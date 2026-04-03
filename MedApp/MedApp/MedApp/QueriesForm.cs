using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MedApp
{
    public partial class QueriesForm : Form
    {
        private readonly DbHelper _db;

        private class QueryDef
        {
            public string Sql;
            public enum ParamType { None, Diagnosis, Patient, Period, N, CompoundPeriod }
            public ParamType Type;
        }

        private QueryDef[] _defs;
        private string[] _keys;

        public QueriesForm(DbHelper db)
        {
            _db = db;
            InitializeComponent();
        }

        private void QueriesForm_Load(object sender, EventArgs e)
        {
            LoadDictionaries();
            InitializeQueries();
        }

        private void LoadDictionaries()
        {
            using var conn = _db.GetConnection(); conn.Open();
            // Диагнозы
            var dtD = new DataTable();
            new MySqlDataAdapter("SELECT id_diagnosis, name_of_diagnosis FROM Diagnosis", conn)
                .Fill(dtD);
            cmbDiagnosis.DataSource = dtD;
            cmbDiagnosis.ValueMember = "id_diagnosis";
            cmbDiagnosis.DisplayMember = "name_of_diagnosis";

            // Пациенты на лечении
            var dtP = new DataTable();
            new MySqlDataAdapter(
                "SELECT id_patient, CONCAT(last_name,' ',first_name,' ',patronymic) AS name " +
                "FROM Patient WHERE status='на лечении'", conn)
                .Fill(dtP);
            cmbPatient.DataSource = dtP;
            cmbPatient.ValueMember = "id_patient";
            cmbPatient.DisplayMember = "name";
        }

        private void InitializeQueries()
        {
            var map = new[]
            {
                new { Key="Пациенты в стационаре", Def=new QueryDef {
                    Sql  = "SELECT id_patient, last_name, first_name, patronymic, status FROM Patient WHERE status='на лечении';",
                    Type = QueryDef.ParamType.None }},

                new { Key="Пациенты с диагнозом", Def=new QueryDef {
                    Sql  = "SELECT p.id_patient, p.last_name, p.first_name, p.patronymic, p.status, d.name_of_diagnosis AS diagnosis "+
                           "FROM Patient p JOIN Diagnosis d ON d.id_diagnosis=p.diagnosis_of_patient "+
                           "WHERE p.diagnosis_of_patient={param};",
                    Type = QueryDef.ParamType.Diagnosis }},

                new { Key="Перемещения пациента на лечении", Def=new QueryDef {
                    Sql  = "SELECT m.date_of_movement, m.id_current_room, r.type_of_room, m.reason_of_movement " +
                           "FROM Movement m JOIN Room r ON r.id_room=m.id_current_room " +
                           "WHERE m.patient_in_room={param} " +
                           "ORDER BY m.date_of_movement;",
                    Type = QueryDef.ParamType.Patient }},

                new { Key="Процедуры пациента на лечении", Def=new QueryDef {
                    Sql  = "SELECT p.id_patient, p.last_name, p.first_name, p.patronymic, a.date_of_appointment, pr.name_of_proc " +
                        "FROM Appointment a " +
                        "  JOIN Patient p ON p.id_patient=a.patient AND p.status='на лечении' " +
                        "  JOIN Proc pr   ON pr.id_proc=a.current_proc " +
                        "WHERE a.current_proc IS NOT NULL AND a.patient={param} " +
                        "ORDER BY a.date_of_appointment;",
            Type = QueryDef.ParamType.Patient }},

                new { Key="Обследования за период", Def=new QueryDef {
                    Sql  = "SELECT p.id_patient, p.last_name, p.first_name, p.patronymic, a.date_of_appointment, s.name_of_survey "+
                           "FROM Appointment a JOIN Patient p ON p.id_patient=a.patient "+
                           "JOIN Survey s ON s.id_survey=a.current_survey "+
                           "WHERE a.current_survey IS NOT NULL AND a.date_of_appointment BETWEEN '{from}' AND '{to}' "+
                           "ORDER BY a.date_of_appointment;",
                    Type = QueryDef.ParamType.Period }},

                new { Key="Свободные места в стационаре", Def=new QueryDef {
                    Sql  = "SELECT SUM(capacity-current_capacity) AS total_free_beds FROM Room;",
                    Type = QueryDef.ParamType.None }},

                new { Key="Пациенты посетившие больше N палат", Def=new QueryDef {
                    Sql  = "SELECT p.id_patient, p.last_name, p.first_name, p.patronymic, COUNT(DISTINCT m.id_current_room) AS rooms_visited "+
                           "FROM Movement m JOIN Patient p ON p.id_patient=m.patient_in_room "+
                           "GROUP BY m.patient_in_room HAVING rooms_visited>{param};",
                    Type = QueryDef.ParamType.N }},

                new { Key="Пациенты поступившие за последние N дней", Def=new QueryDef {
                    Sql  = "SELECT id_patient, last_name, first_name, patronymic, date_of_receipt " +
                        "FROM Patient " +
                        "WHERE date_of_receipt>=DATE_SUB(CURDATE(),INTERVAL {param} DAY);",
                    Type = QueryDef.ParamType.N }},

                new { Key="Назначения через N дней", Def=new QueryDef {
                    Sql  = "SELECT a.date_of_appointment, p.id_patient, p.last_name, p.first_name, p.patronymic, d.speciality, pr.name_of_proc, sv.name_of_survey "+
                           "FROM Appointment a "+
                           "JOIN Patient p ON p.id_patient=a.patient "+
                           "JOIN Doctor d  ON d.id_doctor=a.from_doctor "+
                           "LEFT JOIN Proc pr   ON pr.id_proc=a.current_proc "+
                           "LEFT JOIN Survey sv ON sv.id_survey=a.current_survey "+
                           "WHERE a.date_of_appointment BETWEEN CURDATE() AND DATE_ADD(CURDATE(),INTERVAL {param} DAY) "+
                           "ORDER BY a.date_of_appointment;",
                    Type = QueryDef.ParamType.N }}
            };

            _keys = map.Select(x => x.Key).ToArray();
            _defs = map.Select(x => x.Def).ToArray();

            cmbQueries.Items.Clear();
            cmbQueries.Items.AddRange(_keys);
            cmbQueries.SelectedIndex = 0; // сразу

            UpdateParamControls();  // покажем нужные контролы
        }

        private void cmbQueries_SelectedIndexChanged(object sender, EventArgs e)
            => UpdateParamControls();

        private void UpdateParamControls()
        {
            var def = _defs[cmbQueries.SelectedIndex];

            // Скрыть все
            cmbDiagnosis.Visible = lblDiagnosis.Visible =
            cmbPatient.Visible = lblPatient.Visible =
            dtpFrom.Visible = lblFrom.Visible =
            dtpTo.Visible = lblTo.Visible =
            nudParam.Visible = lblN.Visible = false;

            switch (def.Type)
            {
                case QueryDef.ParamType.Diagnosis:
                    lblDiagnosis.Visible = cmbDiagnosis.Visible = true;
                    break;
                case QueryDef.ParamType.Patient:
                    lblPatient.Visible = cmbPatient.Visible = true;
                    break;
                case QueryDef.ParamType.Period:
                    lblFrom.Visible = dtpFrom.Visible = true;
                    lblTo.Visible = dtpTo.Visible = true;
                    break;
                case QueryDef.ParamType.N:
                    lblN.Visible = nudParam.Visible = true;
                    break;
                case QueryDef.ParamType.None:
                    // ничего
                    break;
            }
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            var def = _defs[cmbQueries.SelectedIndex];
            var sql = def.Sql;

            switch (def.Type)
            {
                case QueryDef.ParamType.Diagnosis:
                    sql = sql.Replace("{param}", cmbDiagnosis.SelectedValue.ToString());
                    break;
                case QueryDef.ParamType.Patient:
                    sql = sql.Replace("{param}", cmbPatient.SelectedValue.ToString());
                    break;
                case QueryDef.ParamType.Period:
                    sql = sql.Replace("{from}", dtpFrom.Value.ToString("yyyy-MM-dd"))
                             .Replace("{to}", dtpTo.Value.ToString("yyyy-MM-dd"));
                    break;
                case QueryDef.ParamType.N:
                    sql = sql.Replace("{param}", nudParam.Value.ToString());
                    break;
            }

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand(sql, conn);
                using var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dgvQueries.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса:\n{ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
