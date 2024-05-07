using Microsoft.Data.SqlClient;
using System;   
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public abstract class DbContext
    {
        private string connectionstring = "Server=mssqlstud.fhict.local;Database=dbi527483_swipey;User Id=dbi527483_swipey;Password=Odul262A;TrustServerCertificate=True";
        public SqlConnection conn { get;set; }
        

        public DbContext()
        {
            this.conn = new SqlConnection(connectionstring);
            this.conn.ConnectionString = connectionstring;

        }

        public int SendTable(string sqlquery, params SqlParameter[] sqlParameters)
        {
            this.conn.ConnectionString = connectionstring;

            this.conn.Open();

            using (conn)
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlquery, conn))
                {
                    sqlCommand.Parameters.AddRange(sqlParameters);

                    return sqlCommand.ExecuteNonQuery();
                }
                }
            }

        public DataTable GetTable(string sqlquery, params SqlParameter[] sqlParameters)
        {
            this.conn.ConnectionString = connectionstring;

            this.conn.Open();

            DataTable dataTable = new DataTable();
            using (conn)
            {
                using (SqlCommand sqlCommand = new SqlCommand(sqlquery, conn))
                {
                    sqlCommand.Parameters.AddRange(sqlParameters);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load(sqlDataReader);
                    }
                }
            }
            this.conn.Close();

            return dataTable;
            
        }

    }
}
