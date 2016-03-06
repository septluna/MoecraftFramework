using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MoecraftFramework
{
    public interface IShow
    {
        bool Show();
    }
    public class CTransfShow
    {
        public bool aaa()
        {
            IShow ish = new CShow();
            bool bl =ish.Show();
            return bl;
        }
    }
}
