using DTO;
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

            public DataTable GetAll()
            {
                return db.LoadData("SELECT * FROM PaymentMethods_N01");
            }
            public void Insert(QLPTTT_DTO p)
            {
                string sql = $"INSERT INTO PaymentMethods_N01 (MethodName_N01) VALUES (N'{p.MethodName}')";
                db.Execute(sql);
            }

            public void Update(QLPTTT_DTO p)
            {
                string sql = $"UPDATE PaymentMethods_N01 SET MethodName_N01 = N'{p.MethodName}' WHERE MethodID_N01 = {p.PaymentMethodId}";
                db.Execute(sql);
            }
            public void Delete(int id)
            {
                string sql = $"DELETE FROM PaymentMethods_N01 WHERE MethodID_N01 = {id}";
                db.Execute(sql);
            }
        }
    }

