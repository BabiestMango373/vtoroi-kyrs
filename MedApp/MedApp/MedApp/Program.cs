using System;
using System.Windows.Forms;

namespace MedApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK)
                return;

            Application.Run(new MainForm(login.Db, login.Role));
        }
    }
}