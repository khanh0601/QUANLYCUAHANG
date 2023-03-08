using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class ChiTietDonHangDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<ChiTietDonHang> getChiTietDonHangList()
        {
            var listChiTietDonHang = db.ChiTietDonHangs.ToList();
            return listChiTietDonHang;
        }

        public void insert(ChiTietDonHang ctdh, ref string ex)
        {
            try
            {
                db.ChiTietDonHangs.Add(ctdh);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }

        public void update(ChiTietDonHang ctdh, ref string ex)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDH == ctdh.MaDH && n.MaSP == ctdh.MaSP);
            if (chiTietDonHang != null)
            {
                chiTietDonHang.MaDH = ctdh.MaDH;
                chiTietDonHang.MaSP = ctdh.MaSP;
                chiTietDonHang.SoLuong = ctdh.SoLuong;
                chiTietDonHang.TongGiaTri = ctdh.TongGiaTri;
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

        public List<ChiTietDonHang> findByMaDH(string maDH)
        {
            return db.ChiTietDonHangs.Where(n => n.MaDH == maDH).ToList();
        }

        public ChiTietDonHang findOne(string maDH, string maSP)
        {
            return db.ChiTietDonHangs.Where(n => n.MaSP == maSP && n.MaDH == maDH).FirstOrDefault();
        }



    }
}
