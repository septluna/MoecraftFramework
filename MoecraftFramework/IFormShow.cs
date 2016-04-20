using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MoecraftFramework
{
    public class FormShow
    {
        public FormShow(Form newForm)
        {
            sForm.frm = newForm;
        }
        public void Show()
        {
            IFormShow ish = new CShow();
            ish.Show();
        }
    }
    public static class sForm
    {
        public static Form frm;
        public static int i = 0;
    }
    public interface IFormShow
    {
        void Show();
    }
    public class CShow : IFormShow
    {
        public void Show()
        {    
            sForm.frm.ShowDialog();
        }
    }
}
