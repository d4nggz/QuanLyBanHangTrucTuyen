using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DAL.QLPTTT_DAL;

namespace BUS
{
    public class QLPTTT_BUS
    {
        QLPTTT_DAL dal = new QLPTTT_DAL();

        public DataTable GetAll() => dal.GetAll();
        public void Add(QLPTTT_DTO p) => dal.Insert(p);
        public void Edit(QLPTTT_DTO p) => dal.Update(p);
        public void Remove(int id) => dal.Delete(id);
    }
}
