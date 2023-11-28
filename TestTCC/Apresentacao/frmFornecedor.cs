using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;
using static TCCAcai.VariaveisGlobal;

namespace TCCAcai.Apresentacao
{
    public partial class frmFornecedor : frmBase
    {
        TipoOperacao operacao = TipoOperacao.Gravar;
        int? idFornecedor = null;
        clsFornecedor fornecedor;
        public frmFornecedor(TipoOperacao eOperacao, int? id_fornecedor = null)
        {
            InitializeComponent();
            operacao = eOperacao;
            idFornecedor = id_fornecedor;
        }

        private void txtCEP_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtCEP.bCEP)
                {
                    txtEndereco.Text = $"{txtCEP.sEndereco}, {txtCEP.sBairro} - Nº xxx, {txtCEP.sEstado}";
                    
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

        private void frmFornecedor_Load(object sender, EventArgs e)
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
                if (idFornecedor != null)
                {
                    fornecedor = new clsFornecedor();
                    fornecedor.ID_FORNECEDOR = idFornecedor.ToInt32();
                    fornecedor.BuscaUmRegistro();
                    if (bMostraMsg)
                    {
                        if ((fornecedor.Retorno != null && fornecedor.Retorno.Tables.Count > 0 && fornecedor.Retorno.Tables[0].Rows != null) && (fornecedor.Retorno.Tables[0].Rows.Count <= 0 || fornecedor.Retorno.Tables[0].Rows[0]["id_fornecedor"].IsNullOrEmpty()))
                        {
                            Mensagem.Alerta("Fornecedor não existente, cancelando operação...", "Erro");
                            Close();
                        }
                    }
                    txtCodigo.Text = fornecedor.ID_FORNECEDOR.ToString();
                    txtNome.Text = fornecedor.NOME_CONTATO;
                    txtTelefone.Text = fornecedor.TELEFONE;
                    txtCEP.Text = fornecedor.CEP;
                    txtEndereco.Text = fornecedor.ENDERECO;
                    txtCNPJ.Text = fornecedor.CNPJ;
                    txtRazaoSocial.Text = fornecedor.RAZAO_SOCIAL;
                    txtEmail.Text = fornecedor.EMAIL;
                    chkAtivo.Checked = fornecedor.ATIVO;
                }
                else
                {
                    Mensagem.Erro("Fornecedor não informado!", "Erro");
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
                            fornecedor = new clsFornecedor();
                            fornecedor.NOME_CONTATO = txtNome.Text;
                            fornecedor.TELEFONE = Regex.Replace(txtTelefone.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.ENDERECO = txtEndereco.Text;
                            fornecedor.CNPJ = Regex.Replace(txtCNPJ.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.RAZAO_SOCIAL = txtRazaoSocial.Text;
                            fornecedor.EMAIL = txtEmail.Text;
                            fornecedor.CEP = Regex.Replace(txtCEP.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.ATIVO = chkAtivo.Checked;
                            fornecedor.Gravar();

                            txtCodigo.Text = fornecedor.ID_FORNECEDOR.ToString();
                            HabilitaDesabilita(false);
                            this.ActiveControl = btnCancelar;
                            Mensagem.Informacao("Registro incluido com sucesso", "Sucesso");
                            operacao = TipoOperacao.Consultar;
                            idFornecedor = fornecedor.ID_FORNECEDOR;
                            btnGravar.Visible = false;
                            btnCancelar.Visible = false;
                            btnAlterar.Visible = true;
                            btnExcluir.Visible = true;
                            break;
                        case TipoOperacao.Alterar:
                            fornecedor = new clsFornecedor();
                            fornecedor.ID_FORNECEDOR = idFornecedor.ToInt32();
                            fornecedor.NOME_CONTATO = txtNome.Text;
                            fornecedor.TELEFONE = Regex.Replace(txtTelefone.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.ENDERECO = txtEndereco.Text;
                            fornecedor.CNPJ = Regex.Replace(txtCNPJ.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.RAZAO_SOCIAL = txtRazaoSocial.Text;
                            fornecedor.EMAIL = txtEmail.Text;
                            fornecedor.CEP = Regex.Replace(txtCEP.Text, "[^0-9a-zA-Z]+", "");
                            fornecedor.ATIVO = chkAtivo.Checked;
                            fornecedor.Alterar();

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
                if (txtCNPJ.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo CNPJ não preenchido", "Atenção");
                    txtNome.Focus();
                    return false;
                }
                if (txtEmail.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Email não preenchido", "Atenção");
                    txtNome.Focus();
                    return false;
                }
                if (txtRazaoSocial.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Razão Social não preenchido", "Atenção");
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

        private void chkAtivo_KeyDown(object sender, KeyEventArgs e)
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
                txtCNPJ.Enabled = bHabilita;
                txtEmail.Enabled = bHabilita;
                txtRazaoSocial.Enabled = bHabilita;
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
                txtCNPJ.Text = "";
                txtEmail.Text = "";
                txtRazaoSocial.Text = "";
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
                if (idFornecedor != null)
                {
                    operacao = TipoOperacao.Consultar;
                    if (new clsFornecedor().BuscaFornecedorCompra(idFornecedor.ToInt32()))
                    {
                        Mensagem.Informacao("Existe compra vinculada a esse fornecedor", "Cancelado");
                        return;
                    }
                    if (Mensagem.Confirmacao("Deseja realmente excluir esse fornecedor?", "Atenção") == System.Windows.Forms.DialogResult.No)
                    {
                        Mensagem.Informacao("Operação cancelada!", "Cancelado");
                        return;
                    }
                    HabilitaDesabilita(false);
                    fornecedor = new clsFornecedor();
                    fornecedor.ID_FORNECEDOR = idFornecedor.ToInt32();
                    fornecedor.Excluir();
                    Mensagem.Informacao("Registro excluido com sucesso", "Excluido");
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    Mensagem.Erro("Fornecedor não informado!", "Erro");
                }

            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }        
    }
}
