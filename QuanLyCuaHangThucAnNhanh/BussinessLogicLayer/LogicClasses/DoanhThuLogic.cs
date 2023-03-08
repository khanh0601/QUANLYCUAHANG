using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class DoanhThuLogic
    {
        DoanhThuDAL dal = new DoanhThuDAL();
        public List<DoanhThu> DoanhThuList()
        {
            var list = dal.getDoanhThuList();
            return list;
        }
        public List<DoanhThu> timDoanhThu(int choice ,int from , int to , DateTime dateFrom , DateTime dateTo,ref string ex) 
        {
            return dal.timDoanhThu( choice , from , to , dateFrom,dateTo,ref ex);
        }
    }
}
