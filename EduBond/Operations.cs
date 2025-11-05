using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EduBond
{
    internal class Operations : IOperations
    {
        private readonly DbConnector _dbConnector;
        private Operations()
        {
            _dbConnector = DbConnector.Instance;
        }
        private static Operations _instance;
        public static Operations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Operations();
                }
                return _instance;
            }
        }

        public bool AddStudent(Student student)
        {
            const string insertQuery = "INSERT INTO student_reg (reg_no, password, name, email, st_year) VALUES (@reg_no, @pass, @name, @email, @sty)";
            using (var conn = _dbConnector.GetConnection())
            {
                using (var cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@reg_no", student.RegNo);
                    cmd.Parameters.AddWithValue("@pass", student.Password);
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.Parameters.AddWithValue("@sty", student.YearOfStudy);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
    }
}
