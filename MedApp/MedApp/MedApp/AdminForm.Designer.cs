namespace MedApp
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tpDiagnosis;
        private System.Windows.Forms.TabPage tpDoctor;
        private System.Windows.Forms.TabPage tpRoom;
        private System.Windows.Forms.TabPage tpLogs;

        private System.Windows.Forms.DataGridView dgvDiagnosis;
        private System.Windows.Forms.Button btnAddDiagnosis;
        private System.Windows.Forms.Button btnEditDiagnosis;
        private System.Windows.Forms.Button btnDeleteDiagnosis;

        private System.Windows.Forms.DataGridView dgvDoctor;
        private System.Windows.Forms.Button btnAddDoctor;
        private System.Windows.Forms.Button btnEditDoctor;
        private System.Windows.Forms.Button btnDeleteDoctor;

        private System.Windows.Forms.DataGridView dgvRoom;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.Button btnEditRoom;
        private System.Windows.Forms.Button btnDeleteRoom;

        private System.Windows.Forms.DataGridView dgvLogs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabAdmin = new TabControl();
            tpDiagnosis = new TabPage();
            btnDeleteDiagnosis = new Button();
            btnEditDiagnosis = new Button();
            btnAddDiagnosis = new Button();
            dgvDiagnosis = new DataGridView();
            tpDoctor = new TabPage();
            btnDeleteDoctor = new Button();
            btnEditDoctor = new Button();
            btnAddDoctor = new Button();
            dgvDoctor = new DataGridView();
            tpRoom = new TabPage();
            btnDeleteRoom = new Button();
            btnEditRoom = new Button();
            btnAddRoom = new Button();
            dgvRoom = new DataGridView();
            tpLogs = new TabPage();
            dgvLogs = new DataGridView();
            tabAdmin.SuspendLayout();
            tpDiagnosis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosis).BeginInit();
            tpDoctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoctor).BeginInit();
            tpRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom).BeginInit();
            tpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // tabAdmin
            // 
            tabAdmin.Controls.Add(tpDiagnosis);
            tabAdmin.Controls.Add(tpDoctor);
            tabAdmin.Controls.Add(tpRoom);
            tabAdmin.Controls.Add(tpLogs);
            tabAdmin.Dock = DockStyle.Fill;
            tabAdmin.Location = new Point(0, 0);
            tabAdmin.Name = "tabAdmin";
            tabAdmin.SelectedIndex = 0;
            tabAdmin.Size = new Size(800, 600);
            tabAdmin.TabIndex = 0;
            tabAdmin.SelectedIndexChanged += tabAdmin_SelectedIndexChanged;
            // 
            // tpDiagnosis
            // 
            tpDiagnosis.Controls.Add(btnDeleteDiagnosis);
            tpDiagnosis.Controls.Add(btnEditDiagnosis);
            tpDiagnosis.Controls.Add(btnAddDiagnosis);
            tpDiagnosis.Controls.Add(dgvDiagnosis);
            tpDiagnosis.Location = new Point(4, 34);
            tpDiagnosis.Name = "tpDiagnosis";
            tpDiagnosis.Padding = new Padding(3);
            tpDiagnosis.Size = new Size(792, 562);
            tpDiagnosis.TabIndex = 0;
            tpDiagnosis.Text = "Диагнозы";
            tpDiagnosis.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDiagnosis
            // 
            btnDeleteDiagnosis.Location = new Point(224, 8);
            btnDeleteDiagnosis.Name = "btnDeleteDiagnosis";
            btnDeleteDiagnosis.Size = new Size(100, 44);
            btnDeleteDiagnosis.TabIndex = 0;
            btnDeleteDiagnosis.Text = "Удалить";
            btnDeleteDiagnosis.Click += btnDeleteDiagnosis_Click;
            // 
            // btnEditDiagnosis
            // 
            btnEditDiagnosis.Location = new Point(116, 8);
            btnEditDiagnosis.Name = "btnEditDiagnosis";
            btnEditDiagnosis.Size = new Size(100, 44);
            btnEditDiagnosis.TabIndex = 1;
            btnEditDiagnosis.Text = "Изменить";
            btnEditDiagnosis.Click += btnEditDiagnosis_Click;
            // 
            // btnAddDiagnosis
            // 
            btnAddDiagnosis.Location = new Point(8, 8);
            btnAddDiagnosis.Name = "btnAddDiagnosis";
            btnAddDiagnosis.Size = new Size(100, 44);
            btnAddDiagnosis.TabIndex = 2;
            btnAddDiagnosis.Text = "Добавить";
            btnAddDiagnosis.Click += btnAddDiagnosis_Click;
            // 
            // dgvDiagnosis
            // 
            dgvDiagnosis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDiagnosis.ColumnHeadersHeight = 34;
            dgvDiagnosis.Location = new Point(8, 58);
            dgvDiagnosis.Name = "dgvDiagnosis";
            dgvDiagnosis.ReadOnly = true;
            dgvDiagnosis.RowHeadersWidth = 62;
            dgvDiagnosis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiagnosis.Size = new Size(776, 496);
            dgvDiagnosis.TabIndex = 3;
            // 
            // tpDoctor
            // 
            tpDoctor.Controls.Add(btnDeleteDoctor);
            tpDoctor.Controls.Add(btnEditDoctor);
            tpDoctor.Controls.Add(btnAddDoctor);
            tpDoctor.Controls.Add(dgvDoctor);
            tpDoctor.Location = new Point(4, 34);
            tpDoctor.Name = "tpDoctor";
            tpDoctor.Padding = new Padding(3);
            tpDoctor.Size = new Size(792, 562);
            tpDoctor.TabIndex = 1;
            tpDoctor.Text = "Врачи";
            tpDoctor.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDoctor
            // 
            btnDeleteDoctor.Location = new Point(224, 8);
            btnDeleteDoctor.Name = "btnDeleteDoctor";
            btnDeleteDoctor.Size = new Size(100, 30);
            btnDeleteDoctor.TabIndex = 0;
            btnDeleteDoctor.Text = "Удалить";
            btnDeleteDoctor.Click += btnDeleteDoctor_Click;
            // 
            // btnEditDoctor
            // 
            btnEditDoctor.Location = new Point(116, 8);
            btnEditDoctor.Name = "btnEditDoctor";
            btnEditDoctor.Size = new Size(100, 30);
            btnEditDoctor.TabIndex = 1;
            btnEditDoctor.Text = "Изменить";
            btnEditDoctor.Click += btnEditDoctor_Click;
            // 
            // btnAddDoctor
            // 
            btnAddDoctor.Location = new Point(8, 8);
            btnAddDoctor.Name = "btnAddDoctor";
            btnAddDoctor.Size = new Size(100, 30);
            btnAddDoctor.TabIndex = 2;
            btnAddDoctor.Text = "Добавить";
            btnAddDoctor.Click += btnAddDoctor_Click;
            // 
            // dgvDoctor
            // 
            dgvDoctor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDoctor.ColumnHeadersHeight = 34;
            dgvDoctor.Location = new Point(8, 58);
            dgvDoctor.Name = "dgvDoctor";
            dgvDoctor.ReadOnly = true;
            dgvDoctor.RowHeadersWidth = 62;
            dgvDoctor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctor.Size = new Size(776, 496);
            dgvDoctor.TabIndex = 3;
            // 
            // tpRoom
            // 
            tpRoom.Controls.Add(btnDeleteRoom);
            tpRoom.Controls.Add(btnEditRoom);
            tpRoom.Controls.Add(btnAddRoom);
            tpRoom.Controls.Add(dgvRoom);
            tpRoom.Location = new Point(4, 34);
            tpRoom.Name = "tpRoom";
            tpRoom.Padding = new Padding(3);
            tpRoom.Size = new Size(792, 562);
            tpRoom.TabIndex = 2;
            tpRoom.Text = "Палаты";
            tpRoom.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRoom
            // 
            btnDeleteRoom.Location = new Point(224, 8);
            btnDeleteRoom.Name = "btnDeleteRoom";
            btnDeleteRoom.Size = new Size(100, 30);
            btnDeleteRoom.TabIndex = 0;
            btnDeleteRoom.Text = "Удалить";
            btnDeleteRoom.Click += btnDeleteRoom_Click;
            // 
            // btnEditRoom
            // 
            btnEditRoom.Location = new Point(116, 8);
            btnEditRoom.Name = "btnEditRoom";
            btnEditRoom.Size = new Size(100, 30);
            btnEditRoom.TabIndex = 1;
            btnEditRoom.Text = "Изменить";
            btnEditRoom.Click += btnEditRoom_Click;
            // 
            // btnAddRoom
            // 
            btnAddRoom.Location = new Point(8, 8);
            btnAddRoom.Name = "btnAddRoom";
            btnAddRoom.Size = new Size(100, 30);
            btnAddRoom.TabIndex = 2;
            btnAddRoom.Text = "Добавить";
            btnAddRoom.Click += btnAddRoom_Click;
            // 
            // dgvRoom
            // 
            dgvRoom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRoom.ColumnHeadersHeight = 34;
            dgvRoom.Location = new Point(8, 58);
            dgvRoom.Name = "dgvRoom";
            dgvRoom.ReadOnly = true;
            dgvRoom.RowHeadersWidth = 62;
            dgvRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoom.Size = new Size(776, 496);
            dgvRoom.TabIndex = 3;
            // 
            // tpLogs
            // 
            tpLogs.Controls.Add(dgvLogs);
            tpLogs.Location = new Point(4, 34);
            tpLogs.Name = "tpLogs";
            tpLogs.Padding = new Padding(3);
            tpLogs.Size = new Size(792, 562);
            tpLogs.TabIndex = 3;
            tpLogs.Text = "Логи";
            tpLogs.UseVisualStyleBackColor = true;
            // 
            // dgvLogs
            // 
            dgvLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLogs.ColumnHeadersHeight = 34;
            dgvLogs.Location = new Point(8, 8);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersWidth = 62;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(776, 546);
            dgvLogs.TabIndex = 0;
            // 
            // AdminForm
            // 
            ClientSize = new Size(800, 600);
            Controls.Add(tabAdmin);
            Name = "AdminForm";
            Text = "Администрирование";
            tabAdmin.ResumeLayout(false);
            tpDiagnosis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosis).EndInit();
            tpDoctor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDoctor).EndInit();
            tpRoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRoom).EndInit();
            tpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
        }
    }
}
