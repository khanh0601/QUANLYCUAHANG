using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class LichLamViecLogic
    {
        LichLamViecDAL dal = new LichLamViecDAL();
        public List<LichLamViec> LichLamViecList()
        {
            var list = dal.getLichLamViecList();
            return list;
        }
        public List<LichLamViec> timthongThuong(int choice , string str , ref string ex)
        {
            return dal.timThongThuong(choice, str, ref ex);
        }
        public List<LichLamViec> timNangCao(int choice , DateTime dateStart, DateTime dateEnd,ref string ex)
        {
            return dal.timNangCao(choice, dateStart, dateEnd, ref ex);
        }
        public void insert(LichLamViec llv, ref string ex)
        {
            dal.insert(llv, ref ex);    
        }
        public LichLamViec find(string mallv , ref string ex)
        {
            return dal.find(mallv, ref ex);
        }
        public void update(LichLamViec llv, ref string ex)
        {
            dal.update(llv, ref ex);    
        }
    }
}
