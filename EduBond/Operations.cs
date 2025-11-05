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
            const string detailsQuery = "INSERT INTO student_reg (reg_no, password, name, email, study_yr) VALUES (@reg_no, @pass, @name, @email, @sty)";
            const string subjectQuery = "INSERT INTO subject_prof_secondyr (reg_no, visualp, cgraphics, dsa, se, webprog) VALUES (@reg_no, @vp, @cg, @ds, @se, @wp)";
            using (var conn = _dbConnector.GetConnection())
            {
                conn.Open();
                using(var transaction  = conn.BeginTransaction())
                {
                    try
                    {
                        using(var cmd1 = new MySqlCommand(detailsQuery, conn, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@reg_no", student.RegNo);
                            cmd1.Parameters.AddWithValue("@pass", student.Password);
                            cmd1.Parameters.AddWithValue("@name", student.Name);
                            cmd1.Parameters.AddWithValue("@email", student.Email);
                            cmd1.Parameters.AddWithValue("@sty", student.YearOfStudy);
                            cmd1.ExecuteNonQuery();
                        }

                        using(var cmd2 = new MySqlCommand(subjectQuery, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@reg_no", student.RegNo);
                            cmd2.Parameters.AddWithValue("@vp", student.SubjectProficiency[0]);
                            cmd2.Parameters.AddWithValue("@cg", student.SubjectProficiency[1]);
                            cmd2.Parameters.AddWithValue("@ds", student.SubjectProficiency[2]);
                            cmd2.Parameters.AddWithValue("@se", student.SubjectProficiency[3]);
                            cmd2.Parameters.AddWithValue("@wp", student.SubjectProficiency[4]);
                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool IsRegistered(string regNo)
        {
            const string regQuery = "SELECT COUNT(*) FROM student_reg WHERE reg_no = @reg_no";
            
            using(var conn = _dbConnector.GetConnection())
            {
                using(var cmd = new MySqlCommand(regQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@reg_no", regNo);

                    try
                    {
                        conn.Open();
                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result > 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

        }

        public bool IsValidPassword(string regNo, string passw)
        {
            const string passCheckQuery = "SELECT password FROM student_reg WHERE reg_no = @reg_no";

            using(var conn = _dbConnector.GetConnection())
            {
                using(var cmd = new MySqlCommand(passCheckQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@reg_no", regNo);

                    try
                    {
                        conn.Open();
                        string result = Convert.ToString(cmd.ExecuteScalar());
                        if (result == passw)
                        {
                            return true;
                        }
                    }
                    catch { }
                }
            }
            return false;
        }

        public List<int> FetchLevels(string regNo)
        {
            var levelList = new List<int>();
            const string fetchQuery = "SELECT * FROM subject_prof_secondyr WHERE reg_no = @reg_no";

            using (var conn = _dbConnector.GetConnection())
            {
                using(var cmd = new MySqlCommand(fetchQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@reg_no", regNo);

                    try
                    {
                        conn.Open();
                        using(var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                for(int i=1; i<reader.FieldCount; i++)
                                {
                                    levelList.Add(Convert.ToInt32(reader[i]));
                                }
                                return levelList;
                            }
                        }
                    }
                    catch { }
                }
            }
            return levelList;
        }
    }
}
