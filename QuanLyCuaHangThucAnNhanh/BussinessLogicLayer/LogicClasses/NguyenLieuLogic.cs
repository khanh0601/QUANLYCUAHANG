using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class NguyenLieuLogic
    {
        NguyenLieuDAL dal = new NguyenLieuDAL();
        public List<NguyenLieu> NguyenLieuList()
        {
            var listNguyenLieu = dal.getNguyenLieuList();
            return listNguyenLieu;
        }
        public List<NguyenLieu> timThongThuong(int choice, string str, ref string ex)
        {
            var list = dal.timThongThuong(choice, str, ref ex);
            return list;
        }

        public List<NguyenLieu> timNangCao(int choice, int soLuongTu, int soLuongDen, ref string ex)
        {
            var list = dal.timNangCao(choice, soLuongTu, soLuongDen, ref ex);
            return list;
        }

        public void insert(NguyenLieu nl, ref string ex)
        {
            dal.insert(nl, ref ex);
        }

        public void update(NguyenLieu nl, ref string ex)
        {
            dal.update(nl, ref ex);
        }

        public void delete(String maNL, ref string ex)
        {
            dal.delete(maNL, ref ex);   
        }


        public NguyenLieu find(string maNl, ref string ex)
        {
            return dal.find(maNl, ref ex);  
        }

    }
}
