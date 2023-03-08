using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class NhaCungCapLogic
    {
        NhaCungCapDAL dal = new NhaCungCapDAL();
        public List<NhaCungCap> NhaCungCapList()
        {
            var list = dal.getNhaCungCapList();
            return list;
        }
        public List<NhaCungCap> timThongThuong(int choice , string str, ref string ex)
        {
            var list = dal.timThongThuong(choice, str, ref ex);
            return list;
        }
        public void insert(NhaCungCap ncc, ref string ex)
        {
            dal.insert(ncc, ref ex);
        }
        public void update(NhaCungCap ncc , ref string ex)
        {
            dal.update(ncc, ref ex);
        }
        public void delete(string MaNCC , ref string ex)
        {
            dal.delete(MaNCC , ref ex); 
        }
        public NhaCungCap find (string maNCC,ref string ex)
        {
            return dal.find(maNCC, ref ex);
        }
    }
}
