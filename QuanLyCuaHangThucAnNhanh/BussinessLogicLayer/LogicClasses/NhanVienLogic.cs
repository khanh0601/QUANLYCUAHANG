using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class NhanVienLogic
    {
        NhanVienDAL dal = new NhanVienDAL();

        public List<NhanVien> NhanVienList()
        {
            var listNhanVien = dal.getNhanVienList();
            return listNhanVien;
        }
        public string ToStringGT(string gt)
        {
            if (gt=="1")
                return "Nam";
            return "Nữ";
        }
        public string ToStringLoai(string loai)
        {
            if (loai == "0")
                return "Part-time";
            return "Full-time";
        }
        public string ToStringChucVu(string cv)
        {
            if (cv == "1")
                return "Quản Lý ";
            return "Nhân Viên";
        }
        public void insert(NhanVien nv, ref string ex)
        {
            dal.insert(nv, ref ex);

        }
        public void update(NhanVien nv, ref string ex)
        {
            dal.update(nv, ref ex);
        }
        public void delete(string  nv , ref string ex)
        {
            dal.delete(nv, ref ex);
        }
        public NhanVien find(string nv, ref string ex)
        {
           return dal.find(nv, ref ex);
        }
        public List<NhanVien> timThongThuong( int choice , string str , ref string ex)
        {
            var list =dal.timThongThuong(choice, str, ref ex);
            return list;
        }
        public List<NhanVien> timNangCao(int choice, string strFrom,string strTo, ref string ex)
        {
            var list = dal.timNangCao(choice, strFrom, strTo, ref ex);
            return list;
        }
    }
}
