using System;
using System.Windows.Forms;

namespace MemoBrew
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            Application.Run(new Welcome());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            CloseAllDatabaseConnections();
        }

        public static void CloseAllDatabaseConnections()
        {

        }
    }
}