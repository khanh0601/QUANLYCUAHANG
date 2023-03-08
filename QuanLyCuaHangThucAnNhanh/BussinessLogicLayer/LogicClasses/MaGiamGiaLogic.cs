using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class MaGiamGiaLogic
    {
        MaGiamGiaDAL dal = new MaGiamGiaDAL();
        public List<MaGiamGia> MaGiamGiaList()
        {
            var list = dal.getMaGiamGiaList();
            return list;
        }
        public void insert (MaGiamGia mgg, ref string ex)
        {
            
                dal.insert(mgg, ref ex);
           
        }
        public MaGiamGia find(string MaGG,ref string ex)
        {
            return  dal.find(MaGG,ref ex);
        }
        public List<MaGiamGia> timThongThuong(int choice , string str, ref string ex)
        {
            var list = dal.timThongThuong(choice, str, ref ex);
            return list;
        }
        public List<MaGiamGia>timNangCao(int choice , int strFrom ,int strTo , ref string ex)
        {
            var list = dal.timNangCao(choice, strFrom, strTo, ref ex);
            return list;
        }
        public void update (MaGiamGia mgg, ref string ex)
        {
            dal.update(mgg, ref ex);

        }
        public void delete(string mgg, ref string ex)
        {
            dal.delete(mgg, ref ex);
        }
    }
}
