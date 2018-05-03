using MySql.Data.MySqlClient;
using StockAnalyzer.Assist;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    class MySqlDBReader
    {
        private static string connStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=stock_ts;";

        public static List<string> querySql(string sql)
        {
            List<string> rs = new List<string>();

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int count = reader.FieldCount;
                    for(int i = 0; i < count; i++)
                    {
                        rs.Add(reader[i].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                Logger.debugOutput(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return rs;
        }
    }
}
