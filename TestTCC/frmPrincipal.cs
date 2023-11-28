using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TCCAcai;
using TCCAcai.Apresentacao;
using TestTCC.Bases;
using TestTCC.Negócios;

namespace TestTCC
{
    public partial class frmPrincipal : frmBase
    {
        #region  Formulários que vão ser abertos pelo botões

        frmProdutos frmProd;
        frmConsultaClientes frmConsultaCli;
        frmClientes frmCli;
        frmConsultaProd frmConsultProd;
        frmFornecedor frmForn;
        frmConsultaFornecedor frmConsultForn;
        frmVenda formVenda;
        frmCompras formCompras;
        frmConsultaCompra formComprasConsulta;
        #endregion


        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                SuperTabGeral.SelectedItem = tabCadastros;
                string sWallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false).GetValue("WallPaper").ToString();
                if (System.IO.File.Exists(sWallPaper))
                {
                    panel2.BackgroundImage = new Bitmap(sWallPaper).Resize(Width, Height);
                }
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            try
            {
                lblHora.Text = DateTime.Now.ToLongDateString().PrimeiraPalavraMaiuscula() + " " + DateTime.Now.ToString("hh:mm:ss");
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if(Application.OpenForms.OfType<frmClientes>().Count() > 0)
                {
                    frmCli.BringToFront();
                    frmCli.Activate();
                    return;
                }
                frmCli = new frmClientes(VariaveisGlobal.TipoOperacao.Gravar);
                frmCli.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
        
        private void btnVerCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmConsultaClientes>().Count() > 0)
                {
                    frmConsultaCli.BringToFront();
                    frmConsultaCli.Activate();
                    return;
                }
                frmConsultaCli = new frmConsultaClientes();
                frmConsultaCli.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
       
        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmProdutos>().Count() > 0)
                {
                    frmProd.BringToFront();
                    frmProd.Activate();
                    return;
                }
                frmProd = new frmProdutos(VariaveisGlobal.TipoOperacao.Gravar);
                frmProd.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnVerProduto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmConsultaProd>().Count() > 0)
                {
                    frmConsultProd.BringToFront();
                    frmConsultProd.Activate();
                    return;
                }
                frmConsultProd = new frmConsultaProd();
                frmConsultProd.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmFornecedor>().Count() > 0)
                {
                    frmForn.BringToFront();
                    frmForn.Activate();
                    return;
                }
                frmForn = new frmFornecedor(VariaveisGlobal.TipoOperacao.Gravar);
                frmForn.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }

        }

        private void btnVerFornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmConsultaFornecedor>().Count() > 0)
                {
                    frmConsultForn.BringToFront();
                    frmConsultForn.Activate();
                    return;
                }
                frmConsultForn = new frmConsultaFornecedor();
                frmConsultForn.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmVenda>().Count() > 0)
                {
                    formVenda.BringToFront();
                    formVenda.Activate();
                    return;
                }
                formVenda = new frmVenda();
                formVenda.WindowState = FormWindowState.Maximized;
                formVenda.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmCompras>().Count() > 0)
                {
                    formCompras.BringToFront();
                    formCompras.Activate();
                    return;
                }
                formCompras = new frmCompras();
                formCompras.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnConsultaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms.OfType<frmConsultaCompra>().Count() > 0)
                {
                    formComprasConsulta.BringToFront();
                    formComprasConsulta.Activate();
                    return;
                }
                formComprasConsulta = new frmConsultaCompra();
                formComprasConsulta.Show();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
