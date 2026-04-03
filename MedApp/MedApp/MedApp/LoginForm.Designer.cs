namespace MedApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtUser = new TextBox();
            txtPwd = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // txtUser
            // 
            txtUser.Location = new Point(30, 30);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Пользователь";
            txtUser.Size = new Size(200, 31);
            txtUser.TabIndex = 0;
            // 
            // txtPwd
            // 
            txtPwd.Location = new Point(30, 70);
            txtPwd.Name = "txtPwd";
            txtPwd.PlaceholderText = "Пароль";
            txtPwd.Size = new Size(200, 31);
            txtPwd.TabIndex = 1;
            txtPwd.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(80, 110);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 30);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            ClientSize = new Size(260, 170);
            Controls.Add(txtUser);
            Controls.Add(txtPwd);
            Controls.Add(btnLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
