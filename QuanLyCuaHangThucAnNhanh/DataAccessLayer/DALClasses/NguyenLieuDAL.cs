using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class NguyenLieuDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<NguyenLieu> getNguyenLieuList()
        {
            var list = db.NguyenLieux.ToList();
            return list;
        }

        public void insert(NguyenLieu nl, ref string ex)
        {
            try
            {
                db.NguyenLieux.Add(nl);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }

        public void update(NguyenLieu nl, ref string ex)
        {
            NguyenLieu nlOnDatabase = db.NguyenLieux.SingleOrDefault(n => nl.MaNL == n.MaNL);

            nlOnDatabase.TenNL = nl.TenNL;
            nlOnDatabase.MaNCC = nl.MaNCC;
            nlOnDatabase.SoLuong = nl.SoLuong;
            nlOnDatabase.DonGia = nl.DonGia;
            nlOnDatabase.DaXoa = nl.DaXoa;

            try
            {
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }

        public void delete(String maNL, ref string ex)
        {
            var nl = db.NguyenLieux.SingleOrDefault(n => n.MaNL == maNL);

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

        //Hàm tìm nguyên liệu dựa theo mã nguyên liệu
        public NguyenLieu find(string maNl, ref string ex)
        {
            try
            {
                return db.NguyenLieux.Where(n => n.MaNL == maNl).Select(n => n).FirstOrDefault();
            }
            catch (Exception e)
            {
                ex = e.Message;
                return null;
            }
        }

        public List<NguyenLieu> timThongThuong(int choice, string str, ref string ex)
        {
            var list = new List<NguyenLieu>();
            switch (choice)
            {
                case 0:
                    //Tìm theo mã nguyên liệu
                    list = db.NguyenLieux.Where(n => n.MaNL.Contains(str)).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo tên nguyên liệu
                    list = db.NguyenLieux.Where(n => n.TenNL.Contains(str)).Select(n => n).ToList();
                    break;
                case 2:
                    //Tìm theo mã nhà cung cấp
                    list = db.NguyenLieux.Where(n => n.MaNCC.Contains(str)).Select(n => n).ToList();
                    break;
            }
            return list;
        }

        public List<NguyenLieu> timNangCao(int choice, int soLuongTu, int soLuongDen, ref string ex)
        {
            var list = new List<NguyenLieu>();

            switch (choice)
            {
                case 0:
                    //Tìm theo số lượng còn lại
                    list = db.NguyenLieux.Where(n => n.SoLuong >= soLuongTu && n.SoLuong < soLuongDen).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo đơn giá nguyên liệu
                    list = db.NguyenLieux.Where(n => n.DonGia >= soLuongTu && n.DonGia < soLuongDen).Select(n => n).ToList();
                    break;
            }

            return list;
        }


    }
}
