using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BussinessLogicLayer.LogicClasses
{
    public class SanPhamLogic
    {
        SanPhamDAL dal = new SanPhamDAL();

        public List<SanPham> SanPhamList()
        {
            var list = dal.getSanPhamList();
            return list;
        }

       public SanPham findByName(string name)
        {
            return dal.findByName(name);
        }

        public SanPham findById(string id)
        {
            return dal.findById(id);
        }
        public SanPham find(string id, ref string ex)
        {
            return dal.find(id,ref  ex);
        }
        public List<SanPham> timThongThuong(int choice , string str,ref string ex)
        {
           return  dal.timThongThuong(choice , str , ref ex);
        }
        public List<SanPham> timNangCao (int choice , int from , int to ,DateTime dateStart ,DateTime  dateEnd , ref string ex)
        {
            return dal.timNangCao(choice , from , to , dateStart, dateEnd , ref ex);    
        }
        public void insert(SanPham sp, ref string ex)
        {
            dal.insert(sp, ref ex);
        }
        public void delete (string  msp, ref string ex)
        {
            dal.delete(msp, ref ex);
        }
        public void update (SanPham sp , ref string ex)
        {
            dal.update(sp, ref ex); 
        }

    }
}
