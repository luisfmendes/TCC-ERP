using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Windows.Forms;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;

namespace TCCAcai.Apresentacao
{
    public partial class frmConsultaCompra : frmBase
    {
        public frmConsultaCompra()
        {
            InitializeComponent();
        }

        private void frmConsultaCompra_Load(object sender, EventArgs e)
        {
            try
            {
                grdConsultaCompras.PrimaryGrid.DataSource = new clsCompra().BuscaTodasCompras();
                grdConsultaCompras.ArrangeGrid(true);
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdConsultaCompras_RowActivated(object sender, DevComponents.DotNetBar.SuperGrid.GridRowActivatedEventArgs e)
        {
            try
            {
                if (!grdConsultaCompras.ActiveRow.IsNullOrEmpty())
                {
                    GridRow row = (GridRow)grdConsultaCompras.ActiveRow;
                    int id_compra = row["id_compra"].Value.ToInt16();
                    grdConsultaProdutos.PrimaryGrid.DataSource = new clsCompra().BuscaProdutosCompra(id_compra);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdConsultaCompras_CellClick(object sender, GridCellClickEventArgs e)
        {
            try
            {
                if (e.GridCell != null && e.GridCell.GridColumn == colunaExcluirConsulta)
                {
                    if (!grdConsultaCompras.ActiveRow.IsNullOrEmpty())
                    {
                        GridRow row = (GridRow)grdConsultaCompras.ActiveRow;
                        if (!row.IsNullOrEmpty())
                        {
                            if (Mensagem.Confirmacao($"Deseja realmente remover o item {row["id_compra"].Value.ToInt32()}?", "Atenção") != DialogResult.Yes)
                            {
                                return;
                            }

                            new clsCompra().ExcluiCompra(row["id_compra"].Value.ToInt16());

                            grdConsultaCompras.PrimaryGrid.DataSource = new clsCompra().BuscaTodasCompras();
                            grdConsultaCompras.ArrangeGrid(true);
                            Mensagem.Informacao("Compra excluida com sucesso!", "Sucesso");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }
    }
}
