using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NetShield_Protector
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main main = new Main();
            main.AutoScaleMode = AutoScaleMode.Dpi;
            main.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            Application.Run(main);
        }
    }
}
