using System;
using System.Windows.Forms;

namespace MedApp
{
    public partial class LoginForm : Form
    {
        public DbHelper Db { get; private set; }
        public string Role { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = txtUser.Text.Trim();
            var pwd = txtPwd.Text;

            var dbh = new DbHelper(user, pwd);
            if (!dbh.TryConnect(out var error))
            {
                MessageBox.Show($"Ошибка подключения: {error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var role = dbh.GetCurrentRole().ToLower();
            if (role != "admin" && role != "doctor" && role != "patient")
            {
                MessageBox.Show($"Недостаточно прав (роль: {role})", "Доступ запрещён", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Db = dbh;
            Role = role;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
