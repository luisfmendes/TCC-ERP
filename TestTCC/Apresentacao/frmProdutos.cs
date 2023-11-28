using System;
using System.Windows.Forms;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;
using static TCCAcai.VariaveisGlobal;

namespace TCCAcai.Apresentacao
{
    public partial class frmProdutos : frmBase
    {
        TipoOperacao operacao = TipoOperacao.Gravar;
        int? idProduto = null;
        clsProduto produto;

        public frmProdutos(TipoOperacao eOperacao, int? id_produto = null)
        {
            InitializeComponent();
            operacao = eOperacao;
            idProduto = id_produto;
        }

        private void frmProdutos_Load(object sender, EventArgs e)
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
                if (idProduto != null)
                {
                    produto = new clsProduto();
                    produto.ID_PRODUTO = idProduto.ToInt32();
                    produto.BuscaUmRegistro();
                    if (bMostraMsg)
                    {
                        if ((produto.Retorno != null && produto.Retorno.Tables.Count > 0 && produto.Retorno.Tables[0].Rows != null) && (produto.Retorno.Tables[0].Rows.Count <= 0 || produto.Retorno.Tables[0].Rows[0]["id_produto"].IsNullOrEmpty()))
                        {
                            Mensagem.Alerta("Produto não existente, cancelando operação...", "Erro");
                            Close();
                        }
                    }
                    txtCodigo.Text = produto.ID_PRODUTO.ToString();
                    txtNome.Text = produto.NOME_PRODUTO;
                    txtDescricao.Text = produto.DESCRICAO;
                    txtPrecoVenda.Value = produto.PRECO_VENDA;
                    txtPrecoCompra.Value = produto.PRECO_COMPRA;
                    chkAtivo.Checked = produto.ATIVO;
                }
                else
                {
                    Mensagem.Erro("Produto não informado!", "Erro");
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
                            produto = new clsProduto();
                            produto.NOME_PRODUTO = txtNome.Text;
                            produto.DESCRICAO = txtDescricao.Text;
                            produto.PRECO_VENDA = txtPrecoVenda.Value;
                            produto.PRECO_COMPRA = txtPrecoCompra.Value;
                            produto.ATIVO = chkAtivo.Checked;
                            produto.Gravar();

                            txtCodigo.Text = produto.ID_PRODUTO.ToString();
                            HabilitaDesabilita(false);
                            this.ActiveControl = btnCancelar;
                            Mensagem.Informacao("Registro incluido com sucesso", "Sucesso");
                            operacao = TipoOperacao.Consultar;
                            idProduto = produto.ID_PRODUTO;
                            btnGravar.Visible = false;
                            btnCancelar.Visible = false;
                            btnAlterar.Visible = true;
                            btnExcluir.Visible = true;
                            break;
                        case TipoOperacao.Alterar:
                            produto = new clsProduto();
                            produto.ID_PRODUTO = idProduto.ToInt32();
                            produto.NOME_PRODUTO = txtNome.Text;
                            produto.DESCRICAO = txtDescricao.Text;
                            produto.PRECO_VENDA = txtPrecoVenda.Value;
                            produto.PRECO_COMPRA = txtPrecoCompra.Value;
                            produto.ATIVO = chkAtivo.Checked;
                            produto.Alterar();

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
        void HabilitaDesabilita(bool bHabilita)
        {
            try
            {
                txtCodigo.Enabled = bHabilita;
                txtNome.Enabled = bHabilita;
                txtDescricao.Enabled = bHabilita;
                txtPrecoVenda.Enabled = bHabilita;
                txtPrecoCompra.Enabled = bHabilita;
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
                if (txtDescricao.Text.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Descrição não preenchido", "Atenção");
                    txtDescricao.Focus();
                    return false;
                }
                if (txtPrecoCompra.Value.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Preço de Compra não preenchido", "Atenção");
                    txtPrecoCompra.Focus();
                    return false;
                }
                if (txtPrecoVenda.Value.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Campo Preço de Venda não preenchido", "Atenção");
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

        void LimpaTudo(bool bLimpaCodigo = true)
        {
            try
            {
                if (bLimpaCodigo)
                {
                    txtCodigo.Text = "";
                }
                txtNome.Text = "";
                txtDescricao.Text = "";
                txtPrecoCompra.Value = 0;
                txtPrecoVenda.Value = 0;
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
        private void numericUpDown_SelectAll(object sender, EventArgs e)
        {
            try
            {
                (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
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
                if (idProduto != null)
                {
                    operacao = TipoOperacao.Consultar;
                    if (new clsProduto().BuscaProdutoVenda(idProduto.ToInt32()))
                    {
                        Mensagem.Informacao("Existe venda ou compra vinculada a esse produto", "Cancelado");
                        return;
                    }
                    if (Mensagem.Confirmacao("Deseja realmente excluir esse produto?", "Atenção") == System.Windows.Forms.DialogResult.No)
                    {
                        Mensagem.Informacao("Operação cancelada!", "Cancelado");
                        return;
                    }
                    HabilitaDesabilita(false);
                    produto = new clsProduto();
                    produto.ID_PRODUTO = idProduto.ToInt32();
                    produto.Excluir();
                    Mensagem.Informacao("Registro excluido com sucesso", "Excluido");
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    Mensagem.Erro("Produto não informado!", "Erro");
                }

            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void chkAtivo_CheckedChanged(object sender, EventArgs e)
        {

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

        private void txtNumericUpDown_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
