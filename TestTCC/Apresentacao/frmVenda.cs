using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;

namespace TCCAcai.Apresentacao
{
    public partial class frmVenda : frmBase
    {
        clsProduto produto = new clsProduto();
        clsCliente cliente = new clsCliente();
        clsVenda dadosVenda = new clsVenda();
        List<clsVenda> venda = new List<clsVenda>();
        bool bControleCodigo = false;
        bool mouseClicked;
        Point clickedAt;

        public frmVenda()
        {
            InitializeComponent();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            try
            {
                LimparDadosLancamento();
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
                    txtValor.Value = produto.PRECO_VENDA;
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

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
                if (produto.QUANTIDADE < txtQtd.Value.ToInt32())
                {
                    Mensagem.Informacao($"Quantidade insuficiente do produto.\n\nQuantidade disponivel: {produto.QUANTIDADE}.", "Atenção");
                    txtQtd.Focus();
                    return;
                }
                if (txtQtd.Value <= 0)
                {
                    Mensagem.Informacao("Quantidade deve ser maior que 0.", "Atenção");
                    txtQtd.Focus();
                    return;
                }

                dadosVenda = new clsVenda();
                dadosVenda.ID_PRODUTO = txtCodigo.Text.ToInt16();
                dadosVenda.QUANTIDADE = txtQtd.Value.ToInt16();
                if (btnAdicionar.Tag.ToString() == "I")
                {
                    if (venda.Any(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()))
                    {
                        venda.Find(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()).QUANTIDADE += dadosVenda.QUANTIDADE;
                    }
                    else
                    {
                        venda.Add(dadosVenda);
                    }
                }
                else
                {
                    venda.Find(row => row.ID_PRODUTO == txtCodigo.Text.ToInt32()).QUANTIDADE = dadosVenda.QUANTIDADE;
                }

                DataTable dt = new clsVenda().BuscaUmRegistoVazio();
                foreach (clsVenda item in venda)
                {
                    dt.Rows.Add(item.ID_PRODUTO, item.produtoDados.DESCRICAO, item.produtoDados.PRECO_VENDA, item.QUANTIDADE, item.produtoDados.PRECO_VENDA * item.QUANTIDADE);
                }

                grdProdutos.PrimaryGrid.DataSource = dt;
                LimparDadosLancamento();
                txtCodigo.Focus();
                if (btnAdicionar.Tag.ToString() == "A")
                {
                    btnAdicionar.Tag = "I";
                    btnAdicionar.Text = "Adicionar";
                }
                atualizaTotalGeral();
                grdProdutos.Enabled = true;
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

        private void frmVenda_Load(object sender, EventArgs e)
        {
            try
            {
                superTabControl.SelectedTab = tabCliente;
                txtCodigoCliente.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            txtQtd.Focus();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void form_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (mouseClicked)
                {
                    this.Location = new Point(Cursor.Position.X - clickedAt.X, Cursor.Position.Y - clickedAt.Y);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }

                mouseClicked = true;
                clickedAt = e.Location;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void form_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                mouseClicked = false;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    txtValor.Value = row["preco_venda"].Value.ToDecimal();
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
                    venda.RemoveAll(r => r.ID_PRODUTO == row["id_produto"].Value.ToInt32());
                    DataTable dt = new clsVenda().BuscaUmRegistoVazio();
                    foreach (clsVenda item in venda)
                    {
                        dt.Rows.Add(item.ID_PRODUTO, item.produtoDados.DESCRICAO, item.produtoDados.PRECO_VENDA, item.QUANTIDADE, item.produtoDados.PRECO_VENDA * item.QUANTIDADE);
                    }

                    grdProdutos.PrimaryGrid.DataSource = dt;
                    atualizaTotalGeral();
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

        void atualizaTotalGeral()
        {
            try
            {
                decimal total = 0;
                foreach (clsVenda item in venda)
                {
                    total += item.QUANTIDADE * item.produtoDados.PRECO_VENDA;
                }
                txtTotalGeral.Value = total;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void txtDescricao_Leave(object sender, EventArgs e)
        {

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

                new clsVenda().Gravar((DataTable)grdProdutos.PrimaryGrid.DataSource, cliente.ID_CLIENTE, txtTotalGeral.Value);
                Mensagem.Informacao("Venda gravada com sucesso!", "Sucesso");
                superTabControl.SelectedTab = tabCliente;
                txtCodigoCliente.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void txtCodigoCliente_Validated(object sender, EventArgs e)
        {
            try
            {
                cliente = new clsCliente();
                cliente.ID_CLIENTE = txtCodigoCliente.Text.ToInt32();
                cliente.BuscaUmRegistro();
                if (cliente.NOME_CLIENTE.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Cliente não localizado!", "Atenção");
                    txtCodigoCliente.Focus();
                    return;
                }
                txtNomeCliente.Text = cliente.NOME_CLIENTE;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
                cliente = new clsCliente();
            }
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (cliente.ID_CLIENTE.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Cliente não informado!", "Atenção");
                    txtCodigoCliente.Focus();
                    return;
                }
                grdProdutos.PrimaryGrid.DataSource = null;
                LimparDadosLancamento();
                txtTotalGeral.Value = 0;
                venda.Clear();
                superTabControl.SelectedTab = tabVenda;
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnConsultaVenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (cliente.ID_CLIENTE.IsNullOrEmpty())
                {
                    Mensagem.Alerta("Cliente não informado!", "Atenção");
                    txtCodigoCliente.Focus();
                    return;
                }
                grdConsultaVendas.PrimaryGrid.DataSource = new clsVenda().BuscaTodasVendasCliente(cliente.ID_CLIENTE);
                grdConsultaVendas.ArrangeGrid(true);
                superTabControl.SelectedTab = tabConsultaVenda;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            try
            {
                if (venda.Count > 0)
                {
                    if (Mensagem.Confirmacao("Todos os dados serão perdidos, deseja realmente voltar?", "Atenção") != DialogResult.Yes)
                    {
                        return;
                    }
                }
                superTabControl.SelectedTab = tabCliente;
                txtCodigoCliente.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }

        private void grdConsultaVendas_RowActivated(object sender, GridRowActivatedEventArgs e)
        {
            try
            {
                if (!grdConsultaVendas.ActiveRow.IsNullOrEmpty())
                {
                    GridRow row = (GridRow)grdConsultaVendas.ActiveRow;
                    int ven_id = row["id_venda"].Value.ToInt16();
                    grdConsultaProdutos.PrimaryGrid.DataSource = new clsVenda().BuscaProdutosVenda(ven_id);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }

        private void btnVoltarConsulta_Click(object sender, EventArgs e)
        {
            try
            {

                superTabControl.SelectedTab = tabCliente;
                txtCodigoCliente.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdConsultaVendas_CellClick(object sender, GridCellClickEventArgs e)
        {
            try
            {
                if (e.GridCell != null && e.GridCell.GridColumn == colunaExcluirConsulta)
                {
                    if (!grdConsultaVendas.ActiveRow.IsNullOrEmpty())
                    {
                        GridRow row = (GridRow)grdConsultaVendas.ActiveRow;
                        if (!row.IsNullOrEmpty())
                        {
                            if (Mensagem.Confirmacao($"Deseja realmente remover o item {row["id_venda"].Value.ToInt32()}?", "Atenção") != DialogResult.Yes)
                            {
                                return;
                            }

                            new clsVenda().ExcluiVenda(row["id_venda"].Value.ToInt32());

                            grdConsultaVendas.PrimaryGrid.DataSource = new clsVenda().BuscaTodasVendasCliente(cliente.ID_CLIENTE);
                            grdConsultaVendas.ArrangeGrid(true);
                            Mensagem.Informacao("Venda excluida com sucesso!", "Sucesso");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void frmVenda_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    btnGravar_Click(null, null);
                }
                else if (e.KeyCode == Keys.F11)
                {
                    if (superTabControl.SelectedTab == tabVenda)
                    {
                        btnVoltar_Click(null, null);
                    }
                    else if (superTabControl.SelectedTab == tabConsultaVenda)
                    {
                        btnVoltarConsulta_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
