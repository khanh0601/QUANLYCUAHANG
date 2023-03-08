using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class NhanVienDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();
        public List<NhanVien> getNhanVienList()
        {
            var listNhanVien = db.NhanViens.ToList();
            return listNhanVien;
        }
        public void insert(NhanVien nv, ref string ex)
        {
            try
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public void update(NhanVien nv, ref string ex)
        {
            NhanVien nvOnDatabase = db.NhanViens.SingleOrDefault(n => n.MaNV == nv.MaNV);

           nvOnDatabase.TenNV = nv.TenNV;
          // nvOnDatabase.MaNV = nv.MaNV;
           nvOnDatabase.MatKhau = nv.MatKhau;
           nvOnDatabase.GioiTinh = nv.GioiTinh;
            nvOnDatabase.Luong = nv.Luong ;
            nvOnDatabase.LoaiNV=nv.LoaiNV;
            nvOnDatabase.ChucVu = nv.ChucVu;
            nvOnDatabase.DaXoa = nv.DaXoa;

            try
            {
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public void delete(string maNV , ref string ex)
        {
            NhanVien nl =db.NhanViens.SingleOrDefault(n=>n.MaNV == maNV);
            nl.DaXoa = true;
            try
            {
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public NhanVien find(string nv, ref string ex)
        {
            try
            {
                return db.NhanViens.Where(n => n.MaNV == nv).Select(n => n).FirstOrDefault();
            }
            catch (SqlException e)
            {
                ex = e.Message;
                return null;
            }
        }

        public int ToNumberGT(string str)
        {
            if (str.Contains("Nam"))
                return 1;
            return 0;
        }

        public int ToNumberLoai(string str)
        {
            // Part-time quy định là số 0 , fulltime là số 1
            if (str.Contains("Part"))
                return 0;
            return 1;
        }

        public int ToNumberCV(string str)
        {
            // quy định Nhân viên là 1, quản lý là 2
            if (str.Contains("Nhân viên"))
                return 1;
            return 2;
        }

        public List<NhanVien> timThongThuong(int choice, string str, ref string ex)
        {
            var listNhanVien = new List<NhanVien>();
            int m = ToNumberGT(str);

            switch (choice)
            {
                case 0:
                    // tìm kiếm theo mã nhân viên 
                    listNhanVien = db.NhanViens.Where(n => n.MaNV.Contains(str)).Select(n => n).ToList();
                    break;
                case 1:
                    //tìm kiếm theo tên nhân viên 
                    listNhanVien = db.NhanViens.Where(n => n.TenNV.Contains(str)).Select(n => n).ToList();
                    break;
                case 2:
                    //tìm kiếm theo giới tình 

                    listNhanVien = db.NhanViens.Where(n => n.GioiTinh == m).Select(n => n).ToList();
                    break;
            }
            return listNhanVien;
        }
        public List<NhanVien> timNangCao(int choice, string strFrom, string strTo, ref string ex)
        {
            var list = new List<NhanVien>();
            int loai = ToNumberLoai(strFrom);
            int chucvu = ToNumberCV(strFrom);
            int from = int.Parse(strFrom);
            int to = int.Parse(strTo);
            switch (choice)
            {
                case 0:
                    //tìm kiếm theo lương
                    list = db.NhanViens.Where(n => n.Luong >= from && n.Luong <= to).Select(n => n).ToList();
                    break;
                case 1:
                    //tìm kiếm theo kiểu nhân viên part-time hay là full-time 
                    list = db.NhanViens.Where(n => n.LoaiNV == loai).Select(n => n).ToList();
                    break;
                case 2:
                    //tìm kiếm theo chức vụ
                    list = db.NhanViens.Where(n => n.ChucVu == chucvu).Select(n => n).ToList();
                    break;
            }
            return list;
        }
    }
}
