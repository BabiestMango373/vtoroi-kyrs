namespace laba15
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            movingButton = new Button();
            comboBox = new ComboBox();
            SuspendLayout();
            // 
            // movingButton
            // 
            movingButton.Location = new Point(100, 120);
            movingButton.Name = "movingButton";
            movingButton.Size = new Size(131, 40);
            movingButton.TabIndex = 0;
            movingButton.Text = "кнопка";
            movingButton.UseVisualStyleBackColor = true;
            movingButton.Click += button1_Click;
            // 
            // comboBox
            // 
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] { "не перемещаться", "по прямой", "sin(x)", "cos(x)" });
            comboBox.SelectedIndex = 0;
            comboBox.Location = new Point(12, 12);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(212, 28);
            comboBox.TabIndex = 0;
            // 
            // Form1
            // 
            ClientSize = new Size(982, 753);
            Controls.Add(comboBox);
            Controls.Add(movingButton);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "laba15";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }



        #endregion


    }
}
