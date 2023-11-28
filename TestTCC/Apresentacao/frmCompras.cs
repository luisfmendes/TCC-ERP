using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Linq;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;
using System.Windows.Forms;

namespace TCCAcai.Apresentacao
{
    public partial class frmCompras : frmBase
    {
        public frmCompras()
        {
            InitializeComponent();
        }

        private void frmCompras_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                cmbFornecedor.DataSource = new clsFornecedor().BuscaTodos().Tables[0];
                //cmbFornecedor.DisplayMember = "razao_social";
                cmbFornecedor.Refresh();
                    }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
        clsProduto produto = new clsProduto();
        clsCompra compra = new clsCompra();
        clsCompraProduto compraProduto = new clsCompraProduto();
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescricao.Text.IsNullOrEmpty())
                {
                    Mensagem.Informacao("Produto não informado.", "Atenção");
                    txtCodigo.Focus();
                    return;
                }
                if (txtQtd.Value <= 0)
                {
                    Mensagem.Informacao("Quantidade deve ser maior que 0.", "Atenção");
                    txtQtd.Focus();
                    return;
                }
                
                compraProduto = new clsCompraProduto();

                compraProduto.ID_PRODUTO = txtCodigo.Text.ToInt16();
                compraProduto.QUANTIDADE = txtQtd.Value.ToInt16();
                if (btnAdicionar.Tag.ToString() == "I")
                {
                    if (compra.produtos.Any(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()))
                    {
                        compra.produtos.Find(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()).QUANTIDADE += compraProduto.QUANTIDADE;
                    }
                    else
                    {
                        compra.produtos.Add(compraProduto);
                    }
                }
                else
                {
                    compra.produtos.Find(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()).QUANTIDADE = compraProduto.QUANTIDADE;
                }

                DataTable dt = new clsCompra().BuscaUmRegistoVazio();
                foreach (clsCompraProduto item in compra.produtos)
                {
                    clsProduto prod = new clsProduto();
                    prod.ID_PRODUTO = item.ID_PRODUTO;
                    prod.BuscaUmRegistro();
                    
                    dt.Rows.Add(item.ID_PRODUTO, prod.DESCRICAO, prod.PRECO_COMPRA, item.QUANTIDADE, prod.PRECO_COMPRA * item.QUANTIDADE);
                }

                grdProdutos.PrimaryGrid.DataSource = dt;
                LimparDadosLancamento();
                txtCodigo.Focus();
                if (btnAdicionar.Tag.ToString() == "A")
                {
                    btnAdicionar.Tag = "I";
                    btnAdicionar.Text = "Adicionar";
                }
                grdProdutos.Enabled = true;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
        void LimparDadosLancamento()
        {
            try
            {
                txtCodigo.Text = "";
                txtDescricao.Text = "";
                txtValor.Value = 0;
                txtQtd.Value = 0;
                txtTotal.Value = 0;
            }
            catch
            {
                throw;
            }
        }
        private void txtCodigo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text != null && txtCodigo.Text != "")
                {
                    produto = new clsProduto();
                    produto.ID_PRODUTO = txtCodigo.Text.ToInt32();
                    produto.BuscaUmRegistro(true);
                    if (produto.NOME_PRODUTO.IsNullOrEmpty() && produto.DESCRICAO.IsNullOrEmpty())
                    {
                        Mensagem.Alerta("Produto não existente.", "Atenção");
                        txtCodigo.Text = "";
                        txtCodigo.Focus();
                        return;
                    }
                    txtDescricao.Text = produto.DESCRICAO;
                    txtValor.Value = produto.PRECO_COMPRA;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdProdutos_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            try
            {
                if (e.GridCell != null && e.GridCell.GridColumn == colunaAlterar)
                {
                    GridRow row = (GridRow)grdProdutos.ActiveRow;
                    txtCodigo.Text = row["id_produto"].Value.ToString();
                    txtDescricao.Text = row["nome_produto"].Value.ToString();
                    txtValor.Value = row["preco_compra"].Value.ToDecimal();
                    txtQtd.Value = row["quantidade"].Value.ToInt16();
                    txtTotal.Value = row["total"].Value.ToDecimal();
                    btnAdicionar.Tag = "A";
                    btnAdicionar.Text = "Alterar";
                    txtDescricao.Focus();
                    grdProdutos.Enabled = false;
                }
                else if (e.GridCell != null && e.GridCell.GridColumn == colunaExcluir)
                {

                    GridRow row = (GridRow)grdProdutos.ActiveRow;
                    if (Mensagem.Confirmacao($"Deseja realmente remover o item {row["id_produto"].Value.ToInt32()} - {row["nome_produto"].Value}?", "Atenção") != DialogResult.Yes)
                    {
                        return;
                    }
                    compra.produtos.RemoveAll(r => r.ID_PRODUTO == row["id_produto"].Value.ToInt32());
                    DataTable dt = new clsCompra().BuscaUmRegistoVazio();
                    foreach (clsCompraProduto item in compra.produtos)
                    {
                        clsProduto prod = new clsProduto();
                        prod.ID_PRODUTO = item.ID_PRODUTO;
                        prod.BuscaUmRegistro();

                        dt.Rows.Add(item.ID_PRODUTO, prod.DESCRICAO, prod.PRECO_COMPRA, item.QUANTIDADE, prod.PRECO_COMPRA * item.QUANTIDADE);
                    }

                    grdProdutos.PrimaryGrid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = (DataTable)grdProdutos.PrimaryGrid.DataSource;
                if (dt.IsNullOrEmpty() || dt.Rows.Count <= 0)
                {
                    Mensagem.Alerta("Não existem produtos lançados!", "Atenção");
                    return;
                }
                if (cmbFornecedor.SelectedValue.IsNullOrEmpty())
                {
                    Mensagem.Informacao("Fornecedor não informado.", "Atenção");
                    cmbFornecedor.Focus();
                    return;
                }

                new clsCompra().Gravar((DataTable)grdProdutos.PrimaryGrid.DataSource, cmbFornecedor.SelectedValue.ToInt32());
                Mensagem.Informacao("Compra gravada com sucesso!", "Sucesso");
                grdProdutos.PrimaryGrid.DataSource = null;
                LimparDadosLancamento();
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void txtQtd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotal.Value = txtValor.Value * txtQtd.Value;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void frmCompras_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    btnGravar_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
