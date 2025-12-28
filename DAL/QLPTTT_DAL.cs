using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DAL.QLPTTT_DAL;

namespace DAL
{
    public class QLPTTT_DAL
    {
            Connect db = new Connect();

        public DataTable GetAllMethods()
        {
            return db.LoadData("SELECT methodID, methodName FROM methods ORDER BY methodID DESC");
        }
        public void InsertMethod(QLPTTT_DTO pttt)
        {
            string sql = "INSERT INTO methods(methodName) VALUES(@Name)";
            MySqlParameter[] para = { new MySqlParameter("@Name", pttt.MethodName) };
            db.Execute(sql, para);
        }

        public void UpdateMethod(QLPTTT_DTO pttt)
        {
            string sql = "UPDATE methods SET methodName = @Name WHERE methodID = @Id";
            MySqlParameter[] para = {
                new MySqlParameter("@Id", pttt.PaymentMethodId),
                new MySqlParameter("@Name", pttt.MethodName)
            };
            db.Execute(sql, para);
        }
        public void DeleteMethod(int id)
        {
            string sql = "DELETE FROM methods WHERE methodID = @Id";
            MySqlParameter[] para = { new MySqlParameter("@Id", id) };
            db.Execute(sql, para);
        }

        public bool CheckNameExist(string name)
        {
            string sql = "SELECT COUNT(*) FROM methods WHERE methodName = @Name";
            MySqlParameter[] para = { new MySqlParameter("@Name", name) };
            DataTable dt = db.LoadData(sql, para);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }
    }
    }

