namespace MedApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdmin;
        private System.Windows.Forms.ToolStripButton tsbPatients;
        private System.Windows.Forms.ToolStripButton tsbAppointments;
        private System.Windows.Forms.ToolStripButton tsbQueries;

        private System.Windows.Forms.Panel panelPatients;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.Button btnAddPatient;
        private System.Windows.Forms.Button btnDischargePatient;
        private System.Windows.Forms.Button btnMovePatient;

        private System.Windows.Forms.Panel panelAppointments;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.Label lblProcedure;
        private System.Windows.Forms.ComboBox cmbProcedure;
        private System.Windows.Forms.Label lblSurvey;
        private System.Windows.Forms.ComboBox cmbSurvey;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label lblNewState;
        private System.Windows.Forms.ComboBox cmbNewState;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.DataGridView dgvAppointments;

        private System.Windows.Forms.Panel panelAdmin;

        private System.Windows.Forms.Panel panelQueries;

        private System.Windows.Forms.ToolStripButton tsbPatientView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            tsbAdmin = new ToolStripButton();
            tsbPatients = new ToolStripButton();
            tsbAppointments = new ToolStripButton();
            tsbQueries = new ToolStripButton();
            panelPatients = new Panel();
            btnAddPatient = new Button();
            btnDischargePatient = new Button();
            btnMovePatient = new Button();
            dgvPatients = new DataGridView();
            panelAppointments = new Panel();
            lblPatient = new Label();
            cmbPatient = new ComboBox();
            lblDoctor = new Label();
            cmbDoctor = new ComboBox();
            lblProcedure = new Label();
            cmbProcedure = new ComboBox();
            lblSurvey = new Label();
            cmbSurvey = new ComboBox();
            lblTime = new Label();
            dtpTime = new DateTimePicker();
            lblNewState = new Label();
            cmbNewState = new ComboBox();
            btnAddAppointment = new Button();
            dgvAppointments = new DataGridView();
            panelAdmin = new Panel();
            panelQueries = new Panel();
            mySqlConnection1 = new MySql.Data.MySqlClient.MySqlConnection();
            tsbPatientView = new ToolStripButton();
            toolStrip1.SuspendLayout();
            panelPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            panelAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdmin, tsbPatients, tsbAppointments, tsbQueries, tsbPatientView });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(895, 34);
            toolStrip1.TabIndex = 0;
            // 
            // tsbAdmin
            // 
            tsbAdmin.Name = "tsbAdmin";
            tsbAdmin.Size = new Size(187, 29);
            tsbAdmin.Text = "Администрирование";
            tsbAdmin.Click += tsbAdmin_Click;
            // 
            // tsbPatients
            // 
            tsbPatients.Name = "tsbPatients";
            tsbPatients.Size = new Size(98, 29);
            tsbPatients.Text = "Пациенты";
            tsbPatients.Click += tsbPatients_Click;
            // 
            // tsbAppointments
            // 
            tsbAppointments.Name = "tsbAppointments";
            tsbAppointments.Size = new Size(113, 29);
            tsbAppointments.Text = "Назначения";
            tsbAppointments.Click += tsbAppointments_Click;
            // 
            // tsbQueries
            // 
            tsbQueries.Name = "tsbQueries";
            tsbQueries.Size = new Size(88, 29);
            tsbQueries.Text = "Запросы";
            tsbQueries.Click += tsbQueries_Click;
            //
            // patientview
            //
            tsbPatientView.Name = "tsbPatientView";
            tsbPatientView.Size = new Size(100, 29);
            tsbPatientView.Text = "Пациенту";
            tsbPatientView.Click += tsbPatientView_Click;
            // 
            // panelPatients
            // 
            panelPatients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPatients.Controls.Add(btnAddPatient);
            panelPatients.Controls.Add(btnDischargePatient);
            panelPatients.Controls.Add(btnMovePatient);
            panelPatients.Controls.Add(dgvPatients);
            panelPatients.Location = new Point(0, 34);
            panelPatients.Name = "panelPatients";
            panelPatients.Size = new Size(895, 631);
            panelPatients.TabIndex = 1;
            panelPatients.Visible = false;
            // 
            // btnAddPatient
            // 
            btnAddPatient.Location = new Point(10, 10);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(100, 40);
            btnAddPatient.TabIndex = 0;
            btnAddPatient.Text = "Добавить";
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // btnDischargePatient
            // 
            btnDischargePatient.Location = new Point(120, 10);
            btnDischargePatient.Name = "btnDischargePatient";
            btnDischargePatient.Size = new Size(99, 40);
            btnDischargePatient.TabIndex = 1;
            btnDischargePatient.Text = "Выписать";
            btnDischargePatient.Click += btnDischargePatient_Click;
            // 
            // btnMovePatient
            // 
            btnMovePatient.Location = new Point(230, 10);
            btnMovePatient.Name = "btnMovePatient";
            btnMovePatient.Size = new Size(106, 40);
            btnMovePatient.TabIndex = 2;
            btnMovePatient.Text = "Перевести";
            btnMovePatient.Click += btnMovePatient_Click;
            // 
            // dgvPatients
            // 
            dgvPatients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPatients.ColumnHeadersHeight = 34;
            dgvPatients.Location = new Point(10, 60);
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersWidth = 62;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(875, 561);
            dgvPatients.TabIndex = 3;
            // 
            // panelAppointments
            // 
            panelAppointments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelAppointments.Controls.Add(lblPatient);
            panelAppointments.Controls.Add(cmbPatient);
            panelAppointments.Controls.Add(lblDoctor);
            panelAppointments.Controls.Add(cmbDoctor);
            panelAppointments.Controls.Add(lblProcedure);
            panelAppointments.Controls.Add(cmbProcedure);
            panelAppointments.Controls.Add(lblSurvey);
            panelAppointments.Controls.Add(cmbSurvey);
            panelAppointments.Controls.Add(lblTime);
            panelAppointments.Controls.Add(dtpTime);
            panelAppointments.Controls.Add(lblNewState);
            panelAppointments.Controls.Add(cmbNewState);
            panelAppointments.Controls.Add(btnAddAppointment);
            panelAppointments.Controls.Add(dgvAppointments);
            panelAppointments.Location = new Point(0, 34);
            panelAppointments.Name = "panelAppointments";
            panelAppointments.Size = new Size(895, 631);
            panelAppointments.TabIndex = 2;
            panelAppointments.Visible = false;
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.Location = new Point(12, 15);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(85, 25);
            lblPatient.TabIndex = 0;
            lblPatient.Text = "Пациент:";
            // 
            // cmbPatient
            // 
            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.Location = new Point(139, 7);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(220, 33);
            cmbPatient.TabIndex = 1;
            // 
            // lblDoctor
            // 
            lblDoctor.AutoSize = true;
            lblDoctor.Location = new Point(374, 10);
            lblDoctor.Name = "lblDoctor";
            lblDoctor.Size = new Size(56, 25);
            lblDoctor.TabIndex = 2;
            lblDoctor.Text = "Врач:";
            // 
            // cmbDoctor
            // 
            cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctor.Location = new Point(538, 7);
            cmbDoctor.Name = "cmbDoctor";
            cmbDoctor.Size = new Size(200, 33);
            cmbDoctor.TabIndex = 3;
            // 
            // lblProcedure
            // 
            lblProcedure.AutoSize = true;
            lblProcedure.Location = new Point(12, 55);
            lblProcedure.Name = "lblProcedure";
            lblProcedure.Size = new Size(110, 25);
            lblProcedure.TabIndex = 4;
            lblProcedure.Text = "Процедура:";
            // 
            // cmbProcedure
            // 
            cmbProcedure.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProcedure.Location = new Point(139, 50);
            cmbProcedure.Name = "cmbProcedure";
            cmbProcedure.Size = new Size(220, 33);
            cmbProcedure.TabIndex = 5;
            // 
            // lblSurvey
            // 
            lblSurvey.AutoSize = true;
            lblSurvey.Location = new Point(374, 53);
            lblSurvey.Name = "lblSurvey";
            lblSurvey.Size = new Size(135, 25);
            lblSurvey.TabIndex = 6;
            lblSurvey.Text = "Обследование:";
            // 
            // cmbSurvey
            // 
            cmbSurvey.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSurvey.Location = new Point(538, 46);
            cmbSurvey.Name = "cmbSurvey";
            cmbSurvey.Size = new Size(200, 33);
            cmbSurvey.TabIndex = 7;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(12, 95);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(125, 25);
            lblTime.TabIndex = 8;
            lblTime.Text = "Дата:";
            // 
            // dtpTime
            // 
            dtpTime.Format = DateTimePickerFormat.Short;
            dtpTime.Location = new Point(139, 90);
            dtpTime.Name = "dtpTime";
            dtpTime.ShowUpDown = false;
            dtpTime.Size = new Size(220, 31);
            dtpTime.TabIndex = 9;
            // 
            // lblNewState
            // 
            lblNewState.AutoSize = true;
            lblNewState.Location = new Point(374, 95);
            lblNewState.Name = "lblNewState";
            lblNewState.Size = new Size(158, 25);
            lblNewState.TabIndex = 10;
            lblNewState.Text = "Новое состояние:";
            // 
            // cmbNewState
            // 
            cmbNewState.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNewState.Items.AddRange(new object[] { "", "стабильное", "средней тяжести", "тяжелое", "критическое" });
            cmbNewState.Location = new Point(538, 87);
            cmbNewState.Name = "cmbNewState";
            cmbNewState.Size = new Size(200, 33);
            cmbNewState.TabIndex = 11;
            // 
            // btnAddAppointment
            // 
            btnAddAppointment.Location = new Point(753, 48);
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Size = new Size(130, 30);
            btnAddAppointment.TabIndex = 12;
            btnAddAppointment.Text = "Создать назначение";
            btnAddAppointment.UseVisualStyleBackColor = true;
            btnAddAppointment.Click += btnAddAppointment_Click;
            // 
            // dgvAppointments
            // 
            dgvAppointments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAppointments.ColumnHeadersHeight = 34;
            dgvAppointments.Location = new Point(12, 140);
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.ReadOnly = true;
            dgvAppointments.RowHeadersWidth = 62;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.Size = new Size(871, 481);
            dgvAppointments.TabIndex = 13;
            // 
            // panelAdmin
            // 
            panelAdmin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelAdmin.Location = new Point(0, 34);
            panelAdmin.Name = "panelAdmin";
            panelAdmin.Size = new Size(800, 566);
            panelAdmin.TabIndex = 3;
            panelAdmin.Visible = false;
            // 
            // panelQueries
            // 
            panelQueries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelQueries.Location = new Point(0, 34);
            panelQueries.Name = "panelQueries";
            panelQueries.Size = new Size(800, 566);
            panelQueries.TabIndex = 4;
            panelQueries.Visible = false;
            // 
            // MainForm
            // 
            ClientSize = new Size(895, 665);
            Controls.Add(toolStrip1);
            Controls.Add(panelPatients);
            Controls.Add(panelAppointments);
            Controls.Add(panelAdmin);
            Controls.Add(panelQueries);
            Name = "MainForm";
            Text = "Главное окно";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panelPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            panelAppointments.ResumeLayout(false);
            panelAppointments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private MySql.Data.MySqlClient.MySqlConnection mySqlConnection1;
    }
}
