using System;
using System.Windows.Forms;
using ADProject;
using MySql.Data.MySqlClient;
using NormalAd;


namespace NormalAd
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
