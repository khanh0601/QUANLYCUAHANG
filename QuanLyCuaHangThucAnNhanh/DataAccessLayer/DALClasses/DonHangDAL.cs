using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class DonHangDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<DonHangMua> getDonHangList()
        {
            var list = db.DonHangMuas.ToList();
            return list;
        }

        public void insert(DonHangMua donHang, ref string ex)
        {
            try
            {
                db.DonHangMuas.Add(donHang);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }

        public void update(DonHangMua dh, ref string ex)
        {
            DonHangMua donHang = db.DonHangMuas.SingleOrDefault(n => n.MaDH == dh.MaDH);
            if (donHang != null)
            {
                donHang.MaDH = dh.MaDH;
                donHang.MaNV = dh.MaNV;
                donHang.MaGG = dh.MaGG;
                donHang.NgayTao = dh.NgayTao;
                donHang.ThanhTien = dh.ThanhTien;
                try
                {
                    db.SaveChanges();
                }
                catch (SqlException e)
                {
                    ex = e.Message;
                }
            }
        }

        //Hàm tìm Đơn hàng dựa theo Mã đơn hàng
        public DonHangMua find(string maDH, ref string ex)
        {
            try
            {
                return db.DonHangMuas.Where(n => n.MaDH== maDH).Select(n => n).FirstOrDefault();
            }
            catch (Exception e)
            {
                ex = e.Message;
                return null;
            }
        }

        public List<DonHangMua> timThongThuong(int choice, string str, ref string ex)
        {
            var list = new List<DonHangMua>();
            switch (choice)
            {
                case 0:
                    //Tìm theo mã đơn hàng
                    list = db.DonHangMuas.Where(n => n.MaDH.Contains(str)).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo mã nhân viên
                    list = db.DonHangMuas.Where(n => n.MaNV.Contains(str)).Select(n => n).ToList();
                    break;
                case 2:
                    //Tìm theo mã giảm giá
                    list = db.DonHangMuas.Where(n => n.MaGG.Contains(str)).Select(n => n).ToList();
                    break;
            }
            return list;
        }

        public List<DonHangMua> timNangCao(int choice, int soLuongTu, int soLuongDen, DateTime start_date, DateTime end_date, ref string ex)
        {
            var list = new List<DonHangMua>();

            switch (choice)
            {
                case 0:
                    //Tìm theo Tổng giá trị
                    list = db.DonHangMuas.Where(n => n.ThanhTien >= soLuongTu && n.ThanhTien < soLuongDen).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo Ngày tạo
                    list = db.DonHangMuas.Where(n => (n.NgayTao >= start_date && n.NgayTao < end_date)).Select(n => n).ToList();
                    break;
            }

            return list;
        }

    }
}
