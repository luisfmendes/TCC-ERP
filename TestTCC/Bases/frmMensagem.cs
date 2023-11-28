using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTCC.Negócios;
using static TCCAcai.VariaveisGlobal;

namespace TestTCC.Bases
{
    public partial class frmMensagem : Form
    {
        bool mouseClicked;
        Point clickedAt;
        public frmMensagem(string sTitulo, string sMensagem, TipoMensagem tipo)
        {
            InitializeComponent();
            try
            {
                this.BackColor = Color.FloralWhite;
                switch (tipo)
                {
                    case TipoMensagem.Alerta:
                        lblTitulo.BackColor = Color.Khaki;
                        break;
                    case TipoMensagem.Erro:
                        lblTitulo.BackColor = Color.Salmon;
                        break;
                    case TipoMensagem.Informacao:
                        lblTitulo.BackColor = Color.SteelBlue;
                        break;
                }
                lblTitulo.Text = sTitulo;
                txtMensagem.Text = sMensagem;
                CenterToParent();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
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

        private void frmMensagem_Load(object sender, EventArgs e)
        {
            try
            {
                btnSair.Focus();
            }
            catch (Exception ex)
            {
                Mensagem.Erro(ex.Message, "Erro");
            }
        }
    }
}
