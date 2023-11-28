using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTCC.Negócios;

namespace TCCAcai.Bases
{
    public partial class txtTelefone : TextBoxX
    {
        #region Propriedades Alteradas

        [DefaultValue(10)]
        public override int MaxLength { get { return base.MaxLength; } set { base.MaxLength = value; } }

        [DefaultValue(HorizontalAlignment.Right)]
        public new HorizontalAlignment TextAlign { get { return base.TextAlign; } set { base.TextAlign = value; } }

        #endregion

        ErrorProvider erro = new ErrorProvider();

        public txtTelefone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Controla o conteúdo que poderá ser inserido na opção colar (Botão direito / CTRL+V / SHIFT+INSERT)
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0302 && !Colar()) //WM_PASTE
                return;
            base.WndProc(ref m);
        }

        private void SelecionaInicio()
        {
            if (!DesignMode)
                if (SelectionStart < MaxLength)
                    SelectionStart = Text.Length + 1;
        }

        protected override void OnEnter(EventArgs e)
        {
            if (!DesignMode)
                SelecionaInicio();
            base.OnEnter(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode)
                SelecionaInicio();
            base.OnClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (!DesignMode)
                SelectAll();
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Controla o conteúdo que poderá ser inserido na opção colar (Botão direito / CTRL+V / SHIFT+INSERT)
        /// </summary>
        bool Colar()
        {
            try
            {
                string sTextoPaste = Clipboard.GetText().Trim();
                if (!string.IsNullOrEmpty(sTextoPaste))
                {
                    if (sTextoPaste.Length > MaxLength)
                    {
                        Mensagem.Alerta(string.Format("Impossível colar Telefone.\nValor copiado deve conter no máximo {0} caracteres.", MaxLength), "Atenção");
                        return false;
                    }
                    else
                    {
                        int iCont = 0;
                        int iTam = (sTextoPaste.fSoNumeros().Length > 8) ? 5 : 4;
                        foreach (Char c in sTextoPaste)
                        {
                            if (!char.IsDigit(c))
                                if ((iCont != iTam) || (iCont == iTam) && (c != '-'))
                                {
                                    Mensagem.Alerta("Impossível colar Telefone.\nValor copiado é inválido.", "Alerta");
                                    return false;
                                }
                            iCont++;
                        }
                    }
                }
            }
            catch { throw; }
            return true;
        }

        //Sobescreve o metodo onKeyPress, para evitar que o usuario entre com um valor não numerico
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!DesignMode)
            {
                if ((!Char.IsDigit(e.KeyChar)) && !VariaveisGlobal.KeyCode_EnterEscBackColar.Contains(e.KeyChar))
                    e.KeyChar = '\x0000';
                else
                {
                    if (!VariaveisGlobal.KeyCode_EnterEsc.Contains(e.KeyChar))
                        if (!ReadOnly && SelectionLength == Text.Length) // Se Selecionar todo o texto, apaga e em seguida insere o numero digitado
                            Text = "";
                }
                SelecionaInicio();
            }
            base.OnKeyPress(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!DesignMode)
            {
                if (VariaveisGlobal.KeyCode_Navegacao.Contains(e.KeyCode)) // Não permite usar as teclas da direita, esquerda, cima e baixo
                    e.Handled = true;
                if (e.KeyCode == Keys.Delete)
                    SendKeys.Send("{BACKSPACE}"); //Usa o BackSpace para deletar
            }
            base.OnKeyDown(e);
        }

        //Sobescreve o metodo OnKeyUp Chama o metodo para a mascara dinamica
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!DesignMode)
                AjustaMascaraTelefone();
            base.OnKeyUp(e);
        }

        //Metodo que cria a mascara dinamica para Telefone
        bool bMascaraCel = false;
        private void AjustaMascaraTelefone()
        {
            if (!DesignMode)
            {
                int cont = 0, cursorPos = SelectionStart;
                if (!string.IsNullOrEmpty(Text))
                {
                    if (Text.fSoNumeros().Length > 8)
                    {
                        Text = Text.Replace("-", "");
                        Text = Text.Substring(0, 5) + "-" + Text.Substring(5, Text.Length - 5);
                        if (SelectionStart < MaxLength)
                            SelectionStart = cursorPos + 1;
                        bMascaraCel = true;
                    }
                    else
                    {
                        if (bMascaraCel)
                        {
                            bMascaraCel = false;
                            Text = Text.Replace("-", "");
                        }
                        foreach (Char c in Text)
                        {
                            if (Text.Length >= cont)
                            {
                                if ((cont == 4) && (c != '-'))
                                {
                                    Text = Text.Insert(4, "-");
                                    if (SelectionStart < MaxLength)
                                        SelectionStart = cursorPos + 1;
                                }
                                if ((c == '-') && (cont != 4))
                                {
                                    Text = Text.Remove(cont, 1);
                                    if (SelectionStart < MaxLength)
                                        SelectionStart = cursorPos;
                                }
                            }
                            cont++;
                        }
                    }
                }
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (!DesignMode)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    if (Text.Length >= 8 && Text.IndexOf('-') == -1) // Verifica se o telefone tem mais de 8 digitos e se está com a máscara
                        AjustaMascaraTelefone();

                    if (Text.Length < 9)
                    {
                        Mensagem.Alerta("Número de Telefone inválido, verifique.", "Atenção");
                        erro.SetError(this, "Número de Telefone inválido, verifique.");
                        Focus(); //e.Cancel = true;
                    }
                    else
                    {
                        erro.Dispose();
                    }
                }
                else
                {
                    erro.Dispose();
                }
            }
            base.OnValidating(e);
        }
    }
}
