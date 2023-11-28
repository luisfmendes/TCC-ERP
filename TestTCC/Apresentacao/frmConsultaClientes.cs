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
    public partial class frmConsultaClientes : frmBase
    {
        public frmConsultaClientes()
        {
            InitializeComponent();
        }

        private void frmConsultaClientes_Load(object sender, EventArgs e)
        {
            try
            {
                grdDadosClientes.PrimaryGrid.DataSource = new clsCliente().BuscaTodos();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void grdDadosClientes_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            try
            {
                if(e.GridCell != null && e.GridCell.GridColumn == coluna_visualizar)
                {
                    GridRow row = (GridRow)grdDadosClientes.ActiveRow;
                    frmClientes frm = new frmClientes(VariaveisGlobal.TipoOperacao.Consultar, row["id_cliente"].Value.ToInt32_Null());
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
                grdDadosClientes.PrimaryGrid.DataSource = null;
                grdDadosClientes.PrimaryGrid.DataSource = new clsCliente().BuscaTodosFiltro(txtNomeFiltro.Text, rabTodos.Checked ? 'A' : rabAtivo.Checked ? 'A' : 'I', rabNomeContem.Checked ? 'C' : rabNomeInicia.Checked ? 'I' : 'T');
                DataSet dt = (DataSet)grdDadosClientes.PrimaryGrid.DataSource;
                if (dt!= null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count <= 0)
                {
                    Mensagem.Informacao("Nenhum registro encontrado!", "Informação");
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void frmConsultaClientes_VisibleChanged(object sender, EventArgs e)
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
