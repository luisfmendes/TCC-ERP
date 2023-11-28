using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TestTCC.Negócios;

namespace TestTCC.Bases
{
    public partial class frmBase : Form
    {
        private bool processtab;
        [DefaultValue(false), Category("Seek"), Description("Seleciona o próximo/anterior controle disponível e faz dele o controle ativo.")] // Categoria
        public bool ProcessTab { get { return processtab; } set { processtab = value; } }

        bool mouseClicked;
        Point clickedAt;
        public frmBase()
        {
            InitializeComponent();
        }
        void fechar_click(object sender, EventArgs e)
        {
            try
            {
                if (this is frmPrincipal)
                {
                    if (Mensagem.Confirmacao("Deseja realmente sair do sistema?", "Atenção") != DialogResult.Yes)
                    {
                        return;
                    }
                }
                this.Close();
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

        #region Processa a tecla Enter/ESC para Avançar e Voltar nos controles
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    if (ProcessTab)
                    {
                        if (e.KeyCode == Keys.Enter) // ENTER Avança para o próximo campo
                        { ProcessTabKey(Tab.Avanca); }
                        else if (e.KeyCode == Keys.Escape) // ESC Volta para o campo anterior
                        {
                            ProcessTabKey(Tab.Volta);
                        }
                    }
                }
                base.OnKeyDown(e);
            }
            catch { throw; }
        }
        /// <summary>
        /// Seleciona o próximo/anterior controle disponível e faz dele o controle ativo.
        /// </summary>
        public void ProcessTabKey(Tab bAvanca)
        {
            try
            {
                if (ActiveControl is DataGridView)
                {
                    if ((ActiveControl as DataGridView).StandardTab)
                    {
                        ProcessTabKey(bAvanca == Tab.Avanca);
                    }
                }
                else if (ActiveControl is Form || (ActiveControl is UserControl))
                {
                    //Quando o ActiveControl é um _Form/UserControl não faz nada, pois dentro dele próprio já tem a função ProcessTabKey
                }
                else
                {
                    ProcessTabKey(bAvanca == Tab.Avanca);
                }
            }
            catch { throw; }
        }
        public enum Tab
        {
            Avanca,
            Volta
        }
        #endregion
    }
}
