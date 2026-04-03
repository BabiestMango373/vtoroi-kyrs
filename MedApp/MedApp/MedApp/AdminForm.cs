using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace MedApp
{
    public partial class AdminForm : Form
    {
        private readonly DbHelper _db;

        public AdminForm(DbHelper db)
        {
            _db = db;
            InitializeComponent();
            Load += AdminForm_Load;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            tabAdmin.SelectedTab = tpDiagnosis;
            LoadDiagnoses();
        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)       {
           switch (tabAdmin.SelectedTab.Name)
           {
               case "tpDiagnosis":
                   LoadDiagnoses();
                   break;
               case "tpDoctor":
                   LoadDoctors();
                   break;
               case "tpRoom":
                   LoadRooms();                   
                    break;               
                case "tpLogs":                   
                    LoadLogs();
                   break;
           }
       }

#region Diagnosis CRUD
private void LoadDiagnoses()
        {
            using var conn = _db.GetConnection(); conn.Open();
            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT id_diagnosis, name_of_diagnosis FROM Diagnosis", conn);
            da.Fill(dt);
            dgvDiagnosis.DataSource = dt;
        }
        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            var name = Interaction.InputBox("Введите название диагноза:", "Добавить диагноз");
            if (string.IsNullOrWhiteSpace(name)) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "INSERT INTO Diagnosis(name_of_diagnosis) VALUES(@n)", conn);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.ExecuteNonQuery();
            LoadDiagnoses();
        }
        private void btnEditDiagnosis_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosis.SelectedRows.Count == 0) return;
            var id = (int)dgvDiagnosis.SelectedRows[0].Cells[0].Value;
            var old = (string)dgvDiagnosis.SelectedRows[0].Cells[1].Value;
            var name = Interaction.InputBox("Новое название:", "Изменить диагноз", old);
            if (string.IsNullOrWhiteSpace(name) || name == old) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "UPDATE Diagnosis SET name_of_diagnosis=@n WHERE id_diagnosis=@id", conn);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadDiagnoses();
        }
        private void btnDeleteDiagnosis_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosis.SelectedRows.Count == 0) return;
            var id = (int)dgvDiagnosis.SelectedRows[0].Cells[0].Value;
            if (MessageBox.Show("Удалить диагноз?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "DELETE FROM Diagnosis WHERE id_diagnosis=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadDiagnoses();
        }
        #endregion

        #region Doctor CRUD
        private void LoadDoctors()
        {
            using var conn = _db.GetConnection(); conn.Open();
            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT id_doctor, speciality FROM Doctor", conn);
            da.Fill(dt);
            dgvDoctor.DataSource = dt;
        }
        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            var spec = Interaction.InputBox("Введите специальность:", "Добавить врача");
            if (string.IsNullOrWhiteSpace(spec)) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "INSERT INTO Doctor(speciality) VALUES(@s)", conn);
            cmd.Parameters.AddWithValue("@s", spec);
            cmd.ExecuteNonQuery();
            LoadDoctors();
        }
        private void btnEditDoctor_Click(object sender, EventArgs e)
        {
            if (dgvDoctor.SelectedRows.Count == 0) return;
            var id = (int)dgvDoctor.SelectedRows[0].Cells[0].Value;
            var old = (string)dgvDoctor.SelectedRows[0].Cells[1].Value;
            var spec = Interaction.InputBox("Новая специальность:", "Изменить врача", old);
            if (string.IsNullOrWhiteSpace(spec) || spec == old) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "UPDATE Doctor SET speciality=@s WHERE id_doctor=@id", conn);
            cmd.Parameters.AddWithValue("@s", spec);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadDoctors();
        }
        private void btnDeleteDoctor_Click(object sender, EventArgs e)
        {
            if (dgvDoctor.SelectedRows.Count == 0) return;
            var id = (int)dgvDoctor.SelectedRows[0].Cells[0].Value;
            if (MessageBox.Show("Удалить врача?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "DELETE FROM Doctor WHERE id_doctor=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadDoctors();
        }
        #endregion

        #region Room CRUD
        private void LoadRooms()
        {
            using var conn = _db.GetConnection(); conn.Open();
            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT id_room, capacity, current_capacity, type_of_room FROM Room", conn);
            da.Fill(dt);
            dgvRoom.DataSource = dt;
        }
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            var cap = Interaction.InputBox("Введите вместимость:", "Добавить палату");
            var type = Interaction.InputBox(
                "Тип палаты:\r\nобычная\r\nреанимационная\r\nинтенсивной терапии\r\nповышенного наблюдения",
                "Добавить палату");
            if (!int.TryParse(cap, out var c)) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "INSERT INTO Room(capacity,current_capacity,type_of_room) VALUES(@cap,0,@t)", conn);
            cmd.Parameters.AddWithValue("@cap", c);
            cmd.Parameters.AddWithValue("@t", type);
            cmd.ExecuteNonQuery();
            LoadRooms();
        }
        private void btnEditRoom_Click(object sender, EventArgs e)
        {
            if (dgvRoom.SelectedRows.Count == 0) return;
            var id = (int)dgvRoom.SelectedRows[0].Cells[0].Value;
            var oldCap = (int)dgvRoom.SelectedRows[0].Cells[1].Value;
            var oldType = (string)dgvRoom.SelectedRows[0].Cells[3].Value;
            var capStr = Interaction.InputBox("Новая вместимость:", "Изменить палату", oldCap.ToString());
            var type = Interaction.InputBox(
                "Тип палаты:\r\nобычная\r\nреанимационная\r\nинтенсивной терапии\r\nповышенного наблюдения",
                "Изменить палату", oldType);
            if (!int.TryParse(capStr, out var c)) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "UPDATE Room SET capacity=@cap,type_of_room=@t WHERE id_room=@id", conn);
            cmd.Parameters.AddWithValue("@cap", c);
            cmd.Parameters.AddWithValue("@t", type);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadRooms();
        }
        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            if (dgvRoom.SelectedRows.Count == 0) return;
            var id = (int)dgvRoom.SelectedRows[0].Cells[0].Value;
            if (MessageBox.Show("Удалить палату?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            using var conn = _db.GetConnection(); conn.Open();
            using var cmd = new MySqlCommand(
                "DELETE FROM Room WHERE id_room=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            LoadRooms();
        }
        #endregion

        #region Logs View
        private void LoadLogs()
        {
            using var conn = _db.GetConnection(); conn.Open();
            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT * FROM v_all_events", conn);
            da.Fill(dt);
            dgvLogs.DataSource = dt;
        }
        #endregion
    }
}
