using MySql.Data.MySqlClient;
using System;

namespace MedApp
{
    public class DbHelper
    {
        private readonly string _host = "localhost";
        private readonly string _schema = "medical_db";
        private readonly string _user;
        private readonly string _pwd;

        public DbHelper(string user, string pwd)
        {
            _user = user;
            _pwd = pwd;
        }

        private string ConnectionString =>
            $"Server={_host};Database={_schema};Uid={_user};Pwd={_pwd};";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public bool TryConnect(out string error)
        {
            error = null;
            try
            {
                using var conn = GetConnection();
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Считает активную роль текущего пользователя (MySQL 8.0+).
        /// </summary>
        public string GetCurrentRole()
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand(
                @"
        SELECT 
          REPLACE(
            REPLACE(
              SUBSTRING_INDEX(CURRENT_ROLE(), '@', 1),
              '`', ''),
            '''', '')
        ",
                conn
            );
            var raw = cmd.ExecuteScalar() as string;
            return (raw ?? "").ToLower();
        }
    }
}