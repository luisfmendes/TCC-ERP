using System;
using System.Text.RegularExpressions;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;
using static TCCAcai.VariaveisGlobal;

namespace TCCAcai.Apresentacao
{
    public partial class frmClientes : frmBase
    {
        TipoOperacao operacao = TipoOperacao.Gravar;
        int? idCliente = null;
        clsCliente cliente;

        public frmClientes(TipoOperacao eOperacao, int? id_cliente = null)
        {
            InitializeComponent();
            operacao = eOperacao;
            idCliente = id_cliente;
        }

        private void txtCEP1_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtCEP.bCEP)
                {
                    txtEndereco.Text = $"{txtCEP.sEndereco}, {txtCEP.sBairro} - Nº xxx, {txtCEP.sEstado}";

                    txtCidade.Text = txtCEP.sCidade;
                    if (txtEndereco.IsNullOrEmpty())
                    {
                        txtEndereco.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            try
            {
                switch (operacao)
                {
                    case TipoOperacao.Gravar:
                        this.ActiveControl = txtNome;
                        btnAlterar.Visible = false;
                        btnExcluir.Visible = false;
                        break;
                    case TipoOperacao.Alterar:
                        PreencheForm();
                        this.ActiveControl = txtNome;
                        btnAlterar.Visible = false;
                        btnExcluir.Visible = false;
                        break;
                    case TipoOperacao.Consultar:
                        PreencheForm();
                        HabilitaDesabilita(false);
                        btnGravar.Visible = false;
                        btnCancelar.Visible = false;
                        btnAlterar.Visible = true;
                        btnExcluir.Visible = true;
                        break;
                }                
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void PreencheForm(bool bMostraMsg = true)
        {
            try
            {
                if (idCliente != null)
                {
                    cliente = new clsCliente();
                    cliente.ID_CLIENTE = idCliente.ToInt32();
                    cliente.BuscaUmRegistro();
                    if (bMostraMsg)
                    {
                        if ((cliente.Retorno != null && cliente.Retorno.Tables.Count > 0 && cliente.Retorno.Tables[0].Rows != null) && (cliente.Retorno.Tables[0].Rows.Count <= 0 || cliente.Retorno.Tables[0].Rows[0]["id_cliente"].IsNullOrEmpty()))
                        {
                            Mensagem.Alerta("Cliente não existente, cancelando operação...", "Erro");
                            Close();
                        }
                    }
                    txtCodigo.Text = cliente.ID_CLIENTE.ToString();
                    txtNome.Text = cliente.NOME_CLIENTE;
                    txtTelefone.Text = cliente.TELEFONE ;
                    txtCEP.Text = cliente.CEP;
                    txtEndereco.Text = cliente.ENDERECO;
                    txtCidade.Text = cliente.CIDADE;
                    chkAtivo.Checked = cliente.ATIVO;
                }
                else
                {
                    Mensagem.Erro("Cliente não informado!", "Erro");
                }

            }
            catch
            {
                throw;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TestaCampos())
                {
                    switch (operacao)
                    {
                        case TipoOperacao.Gravar:
                            cliente = new clsCliente();
                            cliente.NOME_CLIENTE = txtNome.Text;
                            cliente.TELEFONE = Regex.Replace(txtTelefone.Text, "[^0-9a-zA-Z]+", "");
                            cliente.ENDERECO = txtEndereco.Text;
                            cliente.CIDADE = txtCidade.Text;
                            cliente.CEP = Regex.Replace(txtCEP.Text, "[^0-9a-zA-Z]+", "");
                            cliente.ATIVO = chkAtivo.Checked;
                            cliente.Gravar();

                            txtCodigo.Text = cliente.ID_CLIENTE.ToString();
                            HabilitaDesabilita(false);
                            this.ActiveControl = btnCancelar;
                            Mensagem.Informacao("Registro incluido com sucesso", "Sucesso");
                            operacao = TipoOperacao.Consultar;
                            idCliente = cliente.ID_CLIENTE;
                            btnGravar.Visible = false;
                            btnCancelar.Visible = false;
                            btnAlterar.Visible = true;
                            btnExcluir.Visible = true;
                            break;
                        case TipoOperacao.Alterar:
                            cliente = new clsCliente();
                            cliente.ID_CLIENTE = idCliente.ToInt32();
                            cliente.NOME_CLIENTE = txtNome.Text;
                            cliente.TELEFONE = Regex.Replace(txtTelefone.Text, "[^0-9a-zA-Z]+", "");
                            cliente.ENDERECO = txtEndereco.Text;
                            cliente.CIDADE = txtCidade.Text;
                            cliente.CEP = Regex.Replace(txtCEP.Text, "[^0-9a-zA-Z]+", "");
                            cliente.ATIVO = chkAtivo.Checked;
                            cliente.Alterar();

                            HabilitaDesabilita(false);
                            this.ActiveControl = btnCancelar;
                            Mensagem.Informacao("Registro alterado com sucesso", "Sucesso");
                            btnCancelar_Click(null, null);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        bool TestaCampos()
        {
            try
            {
                if (txtNome.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Nome não preenchido", "Atenção");
                    txtNome.Focus();
                    return false;
                }
                if (txtTelefone.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Telefone não preenchido", "Atenção");
                    txtTelefone.Focus();
                    return false;
                }
                if (txtEndereco.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Endereço não preenchido", "Atenção");
                    txtEndereco.Focus();
                    return false;
                }
                if (txtCidade.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Cidade não preenchido", "Atenção");
                    txtNome.Focus();
                    return false;
                }
                if (txtCEP.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo CEP não preenchido", "Atenção");
                    txtNome.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
            return true;
        }

        private void chkAtivo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    chkAtivo.Checked = !chkAtivo.Checked;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        void HabilitaDesabilita(bool bHabilita)
        {
            try
            {
                txtCodigo.Enabled = bHabilita;
                txtNome.Enabled = bHabilita;
                txtTelefone.Enabled = bHabilita;
                txtCEP.Enabled = bHabilita;
                txtEndereco.Enabled = bHabilita;
                txtCidade.Enabled = bHabilita;
                chkAtivo.Enabled = bHabilita;
                btnGravar.Enabled = bHabilita;
                if (bHabilita)
                {
                    chkAtivo.Checked = true;
                }
            }
            catch
            {
                throw;
            }
        }

        void LimpaTudo(bool bLimpaCodigo = true)
        {
            try
            {
                if (bLimpaCodigo)
                {
                    txtCodigo.Text = "";
                }
                txtNome.Text = "";
                txtTelefone.Text = "";
                txtCEP.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                chkAtivo.Checked = false;
            }
            catch
            {
                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (operacao == TipoOperacao.Gravar)
                {
                    LimpaTudo(operacao == TipoOperacao.Gravar);
                    HabilitaDesabilita(true);
                    this.ActiveControl = txtNome;
                }
                else
                {
                    PreencheForm();
                    HabilitaDesabilita(false);
                    btnGravar.Visible = false;
                    btnCancelar.Visible = false;
                    btnAlterar.Visible = true;
                    btnExcluir.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                operacao = TipoOperacao.Alterar;                
                HabilitaDesabilita(true);
                btnGravar.Visible = true;
                btnCancelar.Visible = true;
                this.ActiveControl = txtNome;
                btnAlterar.Visible = false;
                btnExcluir.Visible = false;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCliente != null)
                {
                    operacao = TipoOperacao.Consultar;
                    if(new clsCliente().BuscaClienteVenda(idCliente.ToInt32()))
                    {
                        Mensagem.Informacao("Existe venda vinculada a esse cliente", "Cancelado");
                        return;
                    }
                    if (Mensagem.Confirmacao("Deseja realmente excluir esse cliente?", "Atenção") == System.Windows.Forms.DialogResult.No)
                    {
                        Mensagem.Informacao("Operação cancelada!", "Cancelado");
                        return;
                    }
                    HabilitaDesabilita(false);
                    cliente = new clsCliente();
                    cliente.ID_CLIENTE = idCliente.ToInt32();
                    cliente.Excluir();
                    Mensagem.Informacao("Registro excluido com sucesso", "Excluido");
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    Mensagem.Erro("Cliente não informado!", "Erro");
                }

            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
