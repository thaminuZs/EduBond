using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EduBond
{
    internal class DbConnector
    {
        const string connString = "Server=localhost;Database=edubond;Uid=root;Pwd=sql2289";
        private DbConnector() {}

        private static DbConnector _instance;
        public static DbConnector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbConnector();
                }
                return _instance;
            }
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }

        public bool TestConnection()
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
