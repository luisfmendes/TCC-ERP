using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTCC.Bases;

namespace TestTCC.Negócios
{
    public static class Mensagem
    {
        static frmMensagem frm = null;


        public static void Alerta(string _sMensagem, string _sTitulo)
        {
            try
            {
                frm = new frmMensagem(_sTitulo, _sMensagem, TCCAcai.VariaveisGlobal.TipoMensagem.Alerta);
                frm.ShowDialog();
            }
            catch
            {
                throw;
            }
        }
        public static void Informacao(string _sMensagem, string _sTitulo)
        {
            try
            {
                frm = new frmMensagem(_sTitulo, _sMensagem, TCCAcai.VariaveisGlobal.TipoMensagem.Informacao);
                frm.ShowDialog();
            }
            catch
            {
                throw;
            }
        }
        public static void Erro(string _sMensagem, string _sTitulo)
        {
            try
            {
                frm = new frmMensagem(_sTitulo, _sMensagem, TCCAcai.VariaveisGlobal.TipoMensagem.Erro);
                frm.ShowDialog();
            }
            catch
            {
                throw;
            }
        }
        public static DialogResult Confirmacao(string _sMensagem, string _sTitulo)
        {
            try
            {
                frmMensagemConfirmacao form = new frmMensagemConfirmacao(_sTitulo, _sMensagem);
                return form.ShowDialog();
            }
            catch
            {
                throw;
            }
        }
       
    }
}
