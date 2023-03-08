using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class NhaCungCapDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<NhaCungCap> getNhaCungCapList()
        {
            var list = db.NhaCungCaps.ToList();
            return list;
        }
        public void insert(NhaCungCap ncc, ref string ex)
        {
            try
            {
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public void update(NhaCungCap ncc, ref string ex)
        {
            NhaCungCap nccOnDatabase = db.NhaCungCaps.Where(n => n.MaNCC == ncc.MaNCC).Select(n => n).First();
            nccOnDatabase.TenNCC = ncc.TenNCC;
            //nccOnDatabase.MaNCC = ncc.MaNCC;
            nccOnDatabase.DiaChi = ncc.DiaChi;
            try
            {
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public void delete(string maNCC, ref string ex)
        {
            var ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == maNCC);
            ncc.DaXoa = true;
            try
            {
                db.SaveChanges();
            }
            catch(SqlException e)
            {
                ex=e.Message;   
            }
        }
        public NhaCungCap find(string MaNCC, ref string ex)
        {
            try
            {
                return db.NhaCungCaps.Where(n => n.MaNCC == MaNCC).Select(n => n).FirstOrDefault();
            }
            catch (Exception e)
            {
                ex = e.Message;
                return null;
            }
        }
        public List<NhaCungCap> timThongThuong(int choice, string str, ref string ex)
        {
            var list = new List<NhaCungCap>();
            switch (choice)
            {

                case 0:
                    // Tìm theo mã nhà cung cấp 
                    list = db.NhaCungCaps.Where(n => n.MaNCC.Contains(str)).Select(n => n).ToList();
                    break;
                case 1:
                    // Tìm theo mã tên nhà cung cấp 
                    list = db.NhaCungCaps.Where(n => n.TenNCC.Contains(str)).Select(n => n).ToList();
                    break;
                case 2:
                    // Tìm theo địa chỉ nhà cung cấp 
                    list = db.NhaCungCaps.Where(n => n.DiaChi.Contains(str)).Select(n => n).ToList();
                    break;

            }
            return list;
        }

    }
}
