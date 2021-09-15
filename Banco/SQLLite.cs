using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
namespace Banco
{
    class SQLLite
    {

        public int start(string account, decimal debt, decimal paid, string datepay)
        {
      
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn, account,debt,paid, datepay);
            sqlite_conn.Close();
            return 0;
        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
     
            sqlite_conn = new SQLiteConnection("Data Source= BancoData.db; Version = 3; New = True; Compress = True; ");
         
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servicio no disponible");
            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS debts(id INTEGER NOT NULL,account TEXT NOT NULL,debt REAL NOT NULL,paid REAL NOT NULL,datePayment TEXT NOT NULL,PRIMARY KEY(id AUTOINCREMENT))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

        }

        static void InsertData(SQLiteConnection conn, string account, decimal debt, decimal paid, string datepay)
        {
            var query = "INSERT INTO debts (account, debt, paid,datePayment) VALUES (?, ?, ?, ?)";

            using (var sqlite_cmd = new SQLiteCommand(query, conn))
            {
                sqlite_cmd.Parameters.Add(new SQLiteParameter("account", account));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("debt", debt));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("paid", paid));
                sqlite_cmd.Parameters.Add(new SQLiteParameter("datePayment", datepay));

                sqlite_cmd.ExecuteNonQuery();
            }
        }


    }

}
