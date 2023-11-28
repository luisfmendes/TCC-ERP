using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCCAcai.Negócios;
using TestTCC.Bases;
using TestTCC.Negócios;

namespace TCCAcai.Apresentacao
{
    public partial class frmConsultaProd : frmBase
    {
        public frmConsultaProd()
        {
            InitializeComponent();
        }

        private void frmConsultaProd_Load(object sender, EventArgs e)
        {
            try
            {
                grdDadosProdutos.PrimaryGrid.DataSource = new clsProduto().BuscaTodos();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdDadosProdutos_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            try
            {
                if (e.GridCell != null && e.GridCell.GridColumn == coluna_visualizar)
                {
                    GridRow row = (GridRow)grdDadosProdutos.ActiveRow;
                    frmProdutos frm = new frmProdutos(VariaveisGlobal.TipoOperacao.Consultar, row["id_produto"].Value.ToInt32_Null());
                    frm.ShowDialog();
                    btnFiltrar_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                grdDadosProdutos.PrimaryGrid.DataSource = null;
                grdDadosProdutos.PrimaryGrid.DataSource = new clsProduto().BuscaTodosFiltro(txtNomeFiltro.Text, rabTodos.Checked ? 'A' : rabAtivo.Checked ? 'A' : 'I', rabNomeContem.Checked ? 'C' : rabNomeInicia.Checked ? 'I' : 'T');
                DataSet dt = (DataSet)grdDadosProdutos.PrimaryGrid.DataSource;
                if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count <= 0)
                {
                    Mensagem.Informacao("Nenhum registro encontrado!", "Informação");
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void frmConsultaProd_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                btnFiltrar_Click(null, null);
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
