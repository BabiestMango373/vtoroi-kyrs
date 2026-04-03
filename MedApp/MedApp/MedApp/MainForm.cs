using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace MedApp
{
    public partial class MainForm : Form
    {
        private readonly DbHelper _db;
        private readonly string _role;
        private class QueryDef
        {
            public string SqlTemplate;
            public string ParamLabel; // пустая — если нет параметра
        }

        private Dictionary<string, QueryDef> _queriesMap;

        public MainForm(DbHelper db, string role)
        {
            _db = db;
            _role = role.ToLower();
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ConfigureByRole();
        }

        private void ConfigureByRole()
        {
            tsbPatients.Visible = _role == "admin" || _role == "doctor";
            tsbAdmin.Visible = _role == "admin";
            tsbAppointments.Visible = _role != "patient";
            tsbQueries.Visible = _role == "admin";
            tsbPatientView.Visible = _role == "patient";
            btnAddPatient.Visible = _role != "patient";
            btnDischargePatient.Visible = _role != "patient";
            btnMovePatient.Visible  = _role != "patient";

        }

        private void tsbPatients_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panelPatients.Visible = true;
            LoadPatients();
        }

        private void tsbAppointments_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panelAppointments.Visible = true;
            LoadAppointments();
            LoadAppointmentDictionaries();
        }

        private void tsbQueries_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panelQueries.Visible = true;
            panelQueries.Controls.Clear();

            var qf = new QueriesForm(_db)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            panelQueries.Controls.Add(qf);
            qf.Show();
        }
        private void tsbPatientView_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panelPatients.Visible = true;   
            LoadPatients();                  
        }
        private void HideAllPanels()
        {
            panelPatients.Visible = false;
            panelAppointments.Visible = false;
            panelAdmin.Visible = false;
            panelQueries.Visible = false;
        }

        private void LoadPatients()
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand("SELECT * FROM patient_of_hospital", conn);
            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            dgvPatients.DataSource = table;
        }

        private void LoadAppointments()
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand("SELECT * FROM inf_survey_appointments", conn);
            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            dgvAppointments.DataSource = table;
        }

        private void LoadAppointmentDictionaries()
        {
            using var conn = _db.GetConnection();
            conn.Open();

            // Пациенты
            var dtP = new DataTable();
            using (var da = new MySqlDataAdapter(
                "SELECT id_patient, CONCAT(last_name,' ',first_name,' ',patronymic) AS name " +
                "FROM Patient WHERE status='на лечении'", conn))
            {
                da.Fill(dtP);
            }
            cmbPatient.DataSource = dtP;
            cmbPatient.ValueMember = "id_patient";
            cmbPatient.DisplayMember = "name";

            // Врачи
            var dtD = new DataTable();
            using (var da = new MySqlDataAdapter(
                "SELECT id_doctor, speciality FROM Doctor", conn))
            {
                da.Fill(dtD);
            }
            cmbDoctor.DataSource = dtD;
            cmbDoctor.ValueMember = "id_doctor";
            cmbDoctor.DisplayMember = "speciality";

            // Процедуры
            var dtPr = new DataTable();
            using (var da = new MySqlDataAdapter(
                "SELECT id_proc, name_of_proc FROM Proc", conn))
            {
                da.Fill(dtPr);
            }
            cmbProcedure.DataSource = dtPr;
            cmbProcedure.ValueMember = "id_proc";
            cmbProcedure.DisplayMember = "name_of_proc";

            // Обследования (опционально)
            var dtS = new DataTable();
            dtS.Columns.Add("id_survey", typeof(object));
            dtS.Columns.Add("name_of_survey", typeof(string));
            dtS.Rows.Add(DBNull.Value, "— не указывать —");
            using (var da = new MySqlDataAdapter(
                "SELECT id_survey, name_of_survey FROM Survey", conn))
            {
                da.Fill(dtS);
            }
            cmbSurvey.DataSource = dtS;
            cmbSurvey.ValueMember = "id_survey";
            cmbSurvey.DisplayMember = "name_of_survey";

            // Состояния
            cmbNewState.Items.Clear();
            cmbNewState.Items.AddRange(new object[] {
                "", "стабильное", "средней тяжести", "тяжелое", "критическое"
            });
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("sp_appointment", conn)
                { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("p_patient_id", cmbPatient.SelectedValue);
                cmd.Parameters.AddWithValue("p_doctor_id", cmbDoctor.SelectedValue);
                cmd.Parameters.AddWithValue("p_current_proc", cmbProcedure.SelectedValue);

                var survVal = cmbSurvey.SelectedValue;
                if (survVal == DBNull.Value || survVal == null)
                    cmd.Parameters.AddWithValue("p_current_survey", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("p_current_survey", survVal);

                cmd.Parameters.AddWithValue("p_appointment_time", dtpTime.Value);
                cmd.Parameters.AddWithValue("p_new_state", cmbNewState.Text ?? "");
                cmd.ExecuteNonQuery();

                LoadAppointments();
                MessageBox.Show("Назначение успешно создано", "OK",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления назначения: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbAdmin_Click(object sender, EventArgs e)
        {
            // 1) Скрываем все остальные
            HideAllPanels();

            // 2) Подготавливаем panelAdmin
            panelAdmin.Visible = true;
            panelAdmin.Controls.Clear();

            // 3) Встраиваем AdminForm
            var admin = new AdminForm(_db)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            panelAdmin.Controls.Add(admin);
            admin.Show();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Получаем список диагнозов из БД
                string diagList;
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    using var cmd = new MySqlCommand(
                        "SELECT id_diagnosis, name_of_diagnosis FROM Diagnosis", conn);
                    using var rdr = cmd.ExecuteReader();
                    diagList = "Словарь диагнозов:\r\n";
                    while (rdr.Read())
                    {
                        diagList += $"{rdr.GetInt32(0)} – {rdr.GetString(1)}\r\n";
                    }
                }

                // 2) Запрашиваем у пользователя все данные, встраивая словарь прямо в окно ввода
                var last = Interaction.InputBox("Фамилия:", "Новый пациент");
                var first = Interaction.InputBox("Имя:", "Новый пациент");
                var patron = Interaction.InputBox("Отчество:", "Новый пациент");
                var dobStr = Interaction.InputBox("Дата рождения (yyyy-MM-dd):", "Новый пациент");
                var diagStr = Interaction.InputBox(
                    diagList + "\r\nВведите ID диагноза:", "Новый пациент");
                var state = Interaction.InputBox(
                    "Состояние (стабильное/средней тяжести/тяжелое/критическое):",
                    "Новый пациент");
                var receipt = Interaction.InputBox("Дата поступления (yyyy-MM-dd):", "Новый пациент");

                // 3) Вызываем хранимую процедуру
                using var conn2 = _db.GetConnection();
                conn2.Open();
                using var cmd2 = new MySqlCommand("sp_admit_patient", conn2)
                { CommandType = CommandType.StoredProcedure };
                cmd2.Parameters.AddWithValue("p_last_name", last);
                cmd2.Parameters.AddWithValue("p_first_name", first);
                cmd2.Parameters.AddWithValue("p_patronymic", patron);
                cmd2.Parameters.AddWithValue("p_date_of_birth", DateTime.Parse(dobStr));
                cmd2.Parameters.AddWithValue("p_diagnosis_id", int.Parse(diagStr));
                cmd2.Parameters.AddWithValue("p_state", state);
                cmd2.Parameters.AddWithValue("p_date_of_receipt", DateTime.Parse(receipt));
                cmd2.ExecuteNonQuery();

                LoadPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDischargePatient_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0) return;
            var id = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["id_patient"].Value);
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("sp_discharge_patient", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("p_patient_id", id);
                cmd.ExecuteNonQuery();
                LoadPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выписки: {ex.Message}");
            }
        }

        private void btnMovePatient_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0) return;
            var id = Convert.ToInt32(
                dgvPatients.SelectedRows[0].Cells["id_patient"].Value);

            try
            {
                // 1) Подготавливаем строку-справочник палат
                var roomsInfo =
                    "Словарь палат:\r\n" +
                    "1 – обычная\r\n" +
                    "2 – реанимационная\r\n" +
                    "3 – интенсивной терапии\r\n" +
                    "4 – повышенного наблюдения\r\n";

                // 2) Запрашиваем ID палаты и причину сразу с выводом справочника
                var roomStr = Interaction.InputBox(
                    roomsInfo + "\r\nВведите ID новой палаты:",
                    "Перевести пациента");
                var reason = Interaction.InputBox(
                    "Причина перемещения:",
                    "Перевести пациента");

                // 3) Вызываем хранимую процедуру
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("sp_move_patient", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("p_patient_id", id);
                cmd.Parameters.AddWithValue("p_new_room_id", int.Parse(roomStr));
                cmd.Parameters.AddWithValue("p_reason", reason);
                cmd.ExecuteNonQuery();

                LoadPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка перевода: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
    }
}
