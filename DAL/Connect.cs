using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class Connect
    {
        private string strCon = "Server=210.211.125.205;Database=kbvaamfl_shope;Uid=kbvaamfl_Dang;Pwd=Dinhdang@1;Charset=utf8;";

        public Connect() { }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(strCon);
        }

        public DataTable LoadData(string sql, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kết nối CSDL: " + ex.Message);
                }
            }
            return dt;
        }

        public int Execute(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi Execute: " + ex.Message);
                }
            }
        }

        public void ExecuteTransaction(List<MySqlCommand> commands)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var cmd in commands)
                        {
                            cmd.Connection = conn;
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Lỗi Transaction: " + ex.Message);
                    }
                }
            }
        }
    }
}