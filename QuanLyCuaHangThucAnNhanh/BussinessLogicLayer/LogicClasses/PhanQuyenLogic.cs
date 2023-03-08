using DataAccessLayer;
using DataAccessLayer.DALClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class PhanQuyenLogic
    {
        PhanQuyenDAL dal = new PhanQuyenDAL();

        public int KiemTraQuyen(string username, string pass)
        {
            return dal.KiemTraQuyen(username, pass);    
        }

    }
}
