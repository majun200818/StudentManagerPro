using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdatePro
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmUpdateStart frmStart = new frmUpdateStart();
            DialogResult result = frmStart.ShowDialog();
            if (result == DialogResult.OK)
            {

                Application.Run(new frmUpdate());
            }
            else
            {
                Application.Exit();
            }

        }
    }
}
