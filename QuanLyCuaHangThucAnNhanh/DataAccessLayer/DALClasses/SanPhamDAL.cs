using DataAccessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.DALClasses
{
    public class SanPhamDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<SanPham> getSanPhamList()
        {
            var list = db.SanPhams.ToList();
            return list;
        }

        public SanPham findByName(string name)
        {
            return db.SanPhams.Where(n => n.TenSP == name).FirstOrDefault();
        }

        public SanPham findById(string id)
        {
            return db.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();

        }
        public SanPham find(string id, ref string ex)
        {
            try 
            { return db.SanPhams.Where(n => n.MaSP == id).Select(n=>n).FirstOrDefault(); }
            catch (SqlException e)
            {
                ex = e.Message;
                return null;
            }
        }

        public void update (SanPham sp , ref  string ex)
        {
            var spOnDB = db.SanPhams.SingleOrDefault(n => n.MaSP == sp.MaSP);
            spOnDB.MoTa = sp.MoTa;
            spOnDB.TenSP=sp.TenSP;
            spOnDB.GiaBan = sp.GiaBan;
            spOnDB.ChiPhi = sp.ChiPhi;
            spOnDB.SoLuongTon = sp.SoLuongTon;
            spOnDB.SoLuongDaBan = sp.SoLuongDaBan;
            spOnDB.NgayCapNhat = spOnDB.NgayCapNhat;
            spOnDB.DonViTinh = sp.DonViTinh;
            spOnDB.DaXoa = sp.DaXoa;
            try
            {
                db.SaveChanges();
            }
            catch(SqlException e)
            {
                ex = e.Message;
            }
        }
        public void delete(string masp , ref string ex)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == masp);
            sp.DaXoa = true;
            try
            {
                db.SaveChanges();
            }
            catch (SqlException e) {

                ex = e.Message;
            }


        }
        public void insert(SanPham sp  , ref string ex)
        {
            try
            {
                db.SanPhams.Add(sp);
                db.SaveChanges();
            }
            catch(SqlException e)
            {
                ex = e.Message;
            }
        }
        public List<SanPham> timThongThuong(int choice , string str , ref string ex)
        {
            var list = new List<SanPham>(); 
            switch(choice)
            {
                case 0:
                    //Tìm theo mã sản phẩm 
                    list = db.SanPhams.Where(n => n.MaSP.Contains(str)).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo tên sản phẩm
                    list = db.SanPhams.Where(n => n.TenSP.Contains(str)).Select(n => n).ToList();
                    break;
                
            }
            return list;
        }
        public List<SanPham> timNangCao(int choice , int from , int to , DateTime dateStart , DateTime dateEnd, ref string ex)
        {
            var list = new List<SanPham>();
            switch (choice)
            {
                case 0:
                    // Tìm theo số tiền chi phí 
                    list = db.SanPhams.Where(n => n.ChiPhi >= from && n.ChiPhi <= to).Select(n => n).ToList();
                    break;
                case 1:
                    // Tìm đơn giá sản phẩm 
                    list = db.SanPhams.Where(n => n.GiaBan >= from && n.GiaBan <= to).Select(n => n).ToList();
                    break;
                case 2:
                    //Tìm theo số lượng tồn 
                    list = db.SanPhams.Where(n => n.SoLuongTon >= from && n.SoLuongTon <= to).Select(n => n).ToList();
                    break;
                case 3:
                    //Tìm theo số lượng đã bán 
                    list = db.SanPhams.Where(n => n.SoLuongDaBan >= from && n.SoLuongDaBan <= to).Select(n => n).ToList();
                    break;
                case 4:
                    // Tìm theo ngày cập nhật 
                    list = db.SanPhams.Where(n => n.NgayCapNhat >= dateStart && n.NgayCapNhat <=dateEnd).Select(n => n).ToList();
                    break;
            }
            return list;
        }
    }
}
