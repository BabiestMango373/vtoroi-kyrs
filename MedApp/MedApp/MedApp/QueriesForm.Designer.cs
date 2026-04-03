namespace MedApp
{
    partial class QueriesForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbQueries;
        private System.Windows.Forms.ComboBox cmbDiagnosis;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.NumericUpDown nudParam;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.DataGridView dgvQueries;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblN;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmbQueries = new ComboBox();
            cmbDiagnosis = new ComboBox();
            cmbPatient = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            nudParam = new NumericUpDown();
            btnRunQuery = new Button();
            dgvQueries = new DataGridView();
            lblDiagnosis = new Label();
            lblPatient = new Label();
            lblFrom = new Label();
            lblTo = new Label();
            lblN = new Label();
            ((System.ComponentModel.ISupportInitialize)nudParam).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvQueries).BeginInit();
            SuspendLayout();
            // 
            // cmbQueries
            // 
            cmbQueries.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQueries.Location = new Point(12, 12);
            cmbQueries.Name = "cmbQueries";
            cmbQueries.Size = new Size(614, 33);
            cmbQueries.TabIndex = 0;
            cmbQueries.SelectedIndexChanged += cmbQueries_SelectedIndexChanged;
            // 
            // cmbDiagnosis
            // 
            cmbDiagnosis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDiagnosis.Location = new Point(310, 65);
            cmbDiagnosis.Name = "cmbDiagnosis";
            cmbDiagnosis.Size = new Size(200, 33);
            cmbDiagnosis.TabIndex = 2;
            cmbDiagnosis.SelectedIndexChanged += cmbDiagnosis_SelectedIndexChanged;
            // 
            // cmbPatient
            // 
            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.Location = new Point(103, 65);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(407, 33);
            cmbPatient.TabIndex = 4;
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "yyyy-MM-dd";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(205, 65);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(120, 31);
            dtpFrom.TabIndex = 6;
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "yyyy-MM-dd";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(390, 65);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(120, 31);
            dtpTo.TabIndex = 8;
            // 
            // nudParam
            // 
            nudParam.Location = new Point(451, 67);
            nudParam.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudParam.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudParam.Name = "nudParam";
            nudParam.Size = new Size(60, 31);
            nudParam.TabIndex = 10;
            nudParam.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnRunQuery
            // 
            btnRunQuery.Location = new Point(516, 65);
            btnRunQuery.Name = "btnRunQuery";
            btnRunQuery.Size = new Size(110, 33);
            btnRunQuery.TabIndex = 11;
            btnRunQuery.Text = "Выполнить";
            btnRunQuery.Click += btnRunQuery_Click;
            // 
            // dgvQueries
            // 
            dgvQueries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvQueries.ColumnHeadersHeight = 34;
            dgvQueries.Location = new Point(12, 104);
            dgvQueries.Name = "dgvQueries";
            dgvQueries.RowHeadersWidth = 62;
            dgvQueries.Size = new Size(760, 466);
            dgvQueries.TabIndex = 12;
            // 
            // lblDiagnosis
            // 
            lblDiagnosis.AutoSize = true;
            lblDiagnosis.Location = new Point(220, 69);
            lblDiagnosis.Name = "lblDiagnosis";
            lblDiagnosis.Size = new Size(83, 25);
            lblDiagnosis.TabIndex = 1;
            lblDiagnosis.Text = "Диагноз:";
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.Location = new Point(12, 69);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(85, 25);
            lblPatient.TabIndex = 3;
            lblPatient.Text = "Пациент:";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(163, 68);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(27, 25);
            lblFrom.TabIndex = 5;
            lblFrom.Text = "С:";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(344, 68);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(40, 25);
            lblTo.TabIndex = 7;
            lblTo.Text = "По:";
            // 
            // lblN
            // 
            lblN.AutoSize = true;
            lblN.Location = new Point(416, 69);
            lblN.Name = "lblN";
            lblN.Size = new Size(29, 25);
            lblN.TabIndex = 9;
            lblN.Text = "N:";
            // 
            // QueriesForm
            // 
            ClientSize = new Size(784, 581);
            Controls.Add(cmbQueries);
            Controls.Add(lblDiagnosis);
            Controls.Add(cmbDiagnosis);
            Controls.Add(lblPatient);
            Controls.Add(cmbPatient);
            Controls.Add(lblFrom);
            Controls.Add(dtpFrom);
            Controls.Add(lblTo);
            Controls.Add(dtpTo);
            Controls.Add(lblN);
            Controls.Add(nudParam);
            Controls.Add(btnRunQuery);
            Controls.Add(dgvQueries);
            Name = "QueriesForm";
            Text = "Запросы";
            Load += QueriesForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudParam).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvQueries).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // helper for consistent Y coords
        private int fifty() => 50;
        private int ninety() => 90;
    }
}
