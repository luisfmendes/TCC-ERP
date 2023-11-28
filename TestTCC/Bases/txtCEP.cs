using DevComponents.DotNetBar.Controls;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TestTCC.Negócios;
using static TCCAcai.VariaveisGlobal;

namespace TCCAcai.Bases
{
    public partial class txtCEP : TextBoxX
    {
        #region Propriedades Alteradas

        [DefaultValue(9)]
        public override int MaxLength { get { return base.MaxLength; } set { base.MaxLength = value; } }

        [DefaultValue(HorizontalAlignment.Right)]
        public new HorizontalAlignment TextAlign { get { return base.TextAlign; } set { base.TextAlign = value; } }

        #endregion

        #region Propriedades Endereço do CEP
        /// <summary>
        /// Indica que a consulta de CEP foi realizada com sucesso
        /// </summary>
        [Browsable(false)]
        public bool bCEP { get => bcep; }
        private bool bcep;

        [Browsable(false)]
        public string sEndereco { get => endereco; }
        private string endereco;

        [Browsable(false)]
        public string sComplemento { get => complemento; }
        private string complemento;

        [Browsable(false)]
        public string sComplemento2 { get => complemento2; }
        private string complemento2;

        [Browsable(false)]
        public string sBairro { get => bairro; }
        private string bairro;

        [Browsable(false)]
        public string sCodEstado { get => estado.ToInt32().ToString(); }
        [Browsable(false)]
        public string sEstado { get => estado.ToString(); }
        private UF? estado;

        [Browsable(false)]
        public string sCodCidade { get => codcidade; }
        private string codcidade;
        [Browsable(false)]
        public string sCidade { get => cidade; }
        private string cidade;
        #endregion

        ErrorProvider erro = new ErrorProvider();
        bool pesquisaPorEndereco = false;

        public txtCEP()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Controla o conteúdo que poderá ser inserido na opção colar (Botão direito / CTRL+V / SHIFT+INSERT)
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0302 && !Colar()) //WM_PASTE
            {
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Controla o conteúdo que poderá ser inserido na opção colar (Botão direito / CTRL+V / SHIFT+INSERT)
        /// </summary>
        bool Colar()
        {
            string sTextoPaste = Clipboard.GetText();
            if (!string.IsNullOrEmpty(sTextoPaste))
            {
                if (sTextoPaste.Length > MaxLength)
                {
                    Mensagem.Alerta(string.Format("Impossível colar CEP.\nValor copiado deve conter no máximo {0} caracteres.", MaxLength), "Atenção");
                    return false;
                }
                else
                {
                    int iCont = 0;
                    foreach (Char c in sTextoPaste)
                    {
                        if (!char.IsDigit(c))
                        {
                            if ((iCont != 5) || (iCont == 5) && (c != '-'))
                            {
                                Mensagem.Alerta("Impossível colar CEP.\nValor copiado deve conter o formato '00000-000'.", "Atenção");
                                return false;
                            }
                        }

                        iCont++;
                    }
                }
            }
            return true;
        }

        private void SelecionaInicio()
        {
            if (!DesignMode)
            {
                SelectionStart = Text.Length + 1;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode)
            {
                SelecionaInicio();
            }

            base.OnClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                SelectAll();
            }

            base.OnMouseDoubleClick(e);
        }

        //Sobescreve o metodo onKeyPress, para evitar que o usuario entre com um valor não numerico
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.KeyChar.ToString() == "+")
                {

                }
                else
                {

                    if ((!Char.IsDigit(e.KeyChar)) && !VariaveisGlobal.KeyCode_EnterEscBackColar.Contains(e.KeyChar))
                    {
                        e.KeyChar = '\x0000';
                    }
                    else
                    {
                        if (!VariaveisGlobal.KeyCode_EnterEsc.Contains(e.KeyChar))
                        {
                            if (!ReadOnly && SelectionLength == Text.Length) // Se Selecionar todo o texto, apaga e em seguida insere o numero digitado
                            {
                                Text = "";
                            }
                        }
                    }

                    SelecionaInicio();
                }
            }
            base.OnKeyPress(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!DesignMode)
            {
                if (VariaveisGlobal.KeyCode_Navegacao.Contains(e.KeyCode)) // Não permite usar as teclas da direita, esquerda, cima e baixo
                {
                    e.Handled = true;
                }

                if (e.KeyCode == Keys.Delete)
                {
                    SendKeys.Send("{BACKSPACE}"); //Usa o BackSpace para deletar
                }

            }
        }

        //Sobescreve o metodo OnKeyUp (Chama o metodo para a mascara dinamica)
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (!DesignMode)
            {
                AjustaMascaraCEP();
            }
        }

        //Metodo que cria a mascara dinamica para CEP
        private void AjustaMascaraCEP()
        {
            if (!DesignMode)
            {
                int cont = 0;
                int cursorPos = SelectionStart;

                foreach (Char c in Text)
                {
                    if ((cont == 5) && (c != '-') && (Text.Length >= cont))
                    {
                        Text = Text.Insert(5, "-");
                        SelectionStart = cursorPos + 1;
                    }
                    if ((c == '-') && (cont != 5) && (Text.Length >= cont))
                    {
                        Text = Text.Remove(cont, 1);
                        SelectionStart = cursorPos;
                    }
                    cont++;
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!DesignMode)
            {
                SelecionaInicio();
                Tag = Text;
            }
            base.OnEnter(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (!DesignMode)
            {
                if (pesquisaPorEndereco)
                {
                    //bValidacao_txtChild = true;
                    erro.Dispose();
                    pesquisaPorEndereco = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Text))
                    {
                        if (Text.Length == 8 && Text.IndexOf('-') == -1) // Verifica se o cep tem 8 digitos e se está com a máscara
                        {
                            AjustaMascaraCEP();
                        }

                        if (Text.Length != 9)
                        {
                            LimpaCEP();
                            //bValidacao_txtChild = false;
                            Mensagem.Alerta("CEP deve ter 8 digitos, verifique.", "Atenção");
                            erro.SetError(this, "CEP deve ter 8 digitos.");
                            Focus();
                            SelectAll();
                            //e.Cancel = true;
                        }
                        else
                        {
                            //bValidacao_txtChild = true;
                            erro.Dispose();
                            BuscaCEP();
                        }
                    }
                    else
                    {
                        //bValidacao_txtChild = true;
                        erro.Dispose();
                        LimpaCEP();
                    }
                }
            }
            base.OnValidating(e);
        }

        private bool NullOrEmpty()
        {
            return (string.IsNullOrEmpty(Text));
        }

        private bool CompararTag(object sender)
        {
            try
            {
                if (((Control)sender).Tag != null)
                {
                    if (sender is TextBox)
                    {
                        return ((sender as TextBox).Tag.ToString().Trim() != (sender as TextBox).Text.Trim());
                    }
                    else if (sender is TextBoxX)
                    {
                        return ((sender as TextBoxX).Tag.ToString().Trim() != (sender as TextBoxX).Text.Trim());
                    }
                    else if (sender is ComboBox)
                    {
                        string sValue = ((sender as ComboBox).SelectedValue.IsNullOrEmpty()) ? "" : (sender as ComboBox).SelectedValue.ToString();
                        return ((sender as ComboBox).Tag.ToString() != sValue);
                    }
                    else if (sender is NumericUpDown)
                    {
                        return ((sender as NumericUpDown).Tag.ToString() != (sender as NumericUpDown).Value.ToString());
                    }
                    else if (sender is DateTimePicker)
                    {
                        return ((sender as DateTimePicker).Tag.ToString() != (sender as DateTimePicker).Value.ToString());
                    }
                    else if (sender is ListBox)
                    {
                        return ((sender as ListBox).Tag.ToString() != (sender as ListBox).SelectedValue.ToString());
                    }
                    else if (sender is MaskedTextBox)
                    {
                        return ((sender as MaskedTextBox).Tag.ToString() != (sender as MaskedTextBox).Text.ToString());
                    }
                }
                else
                {
                    return true;
                }
            }
            catch { throw; }
            return false;
        }

        void BuscaCEP()
        {
            try
            {
                if (VariaveisGlobal.bCEP_auto)
                {
                    if (NullOrEmpty())
                    {
                        LimpaCEP();
                    }
                    else if (CompararTag(this))
                    {
                        //
                        // CONSULTA CEP VIA API VIACEP
                        //
                        string url = string.Format("https://viacep.com.br/ws/{0}/json/", Text.fSoNumeros());
                        System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
                        request.Method = "GET";
                        using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
                        {
                            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                throw new ApplicationException("Falha na consulta do CEP: " + response.StatusCode);
                            }

                            using (System.IO.Stream responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
                                    {
                                        DadosBuscaCEP oCEP = new DadosBuscaCEP();
                                        JsonConvert.PopulateObject(reader.ReadToEnd(), oCEP);

                                        endereco = oCEP?.LOGRADOURO?.RemoveAcentos().ToUpper();
                                        bairro = oCEP?.BAIRRO?.RemoveAcentos().ToUpper();
                                        estado = oCEP?.UF?.ToUpper().ToEnum<VariaveisGlobal.UF>();
                                        //complemento = "";
                                        complemento2 = oCEP?.COMPLEMENTO?.RemoveAcentos().ToUpper();
                                        cidade = oCEP?.LOCALIDADE?.RemoveAcentos().ToUpper();

                                        DataRow[] drCid = VariaveisGlobal.dados_cidade.Select(string.Format("desc_uf='{0}' and mun_descricao='{1}'", estado, cidade));
                                        codcidade = (drCid?.Length > 0) ? drCid[0]["mun_codigo"].ToString() : null;
                                        bcep = true;
                                    }
                                }
                            }
                        }                        
                    }
                    else
                    {
                        bcep = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LimpaCEP();
                string sMsg = (!ex.Message.IsNullOrEmpty()) ? string.Format("Falha na consulta de CEP: {0}.", ex.Message.PrimeiraPalavraMaiuscula()) : "Falha durante a pesquisa de CEP via WebService.";
                //bValidacao_txtChild = false;
                Mensagem.Alerta(sMsg, "Pesquisa CEP");
                erro.SetError(this, sMsg);
                Focus();
                SelectAll();
            }
        }

        void LimpaCEP()
        {
            endereco = complemento = complemento2 = bairro = cidade = codcidade = null;
            estado = null;
            bcep = false;
        }
    }
    class DadosBuscaCEP
    {
        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("logradouro")]
        public string LOGRADOURO { get; set; }

        [JsonProperty("complemento")]
        public string COMPLEMENTO { get; set; }

        [JsonProperty("bairro")]
        public string BAIRRO { get; set; }

        [JsonProperty("localidade")]
        public string LOCALIDADE { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        [JsonProperty("unidade")]
        public string UNIDADE { get; set; }

        [JsonProperty("ibge")]
        public string IBGE { get; set; }

        [JsonProperty("gia")]
        public string GIA { get; set; }

    }
}
