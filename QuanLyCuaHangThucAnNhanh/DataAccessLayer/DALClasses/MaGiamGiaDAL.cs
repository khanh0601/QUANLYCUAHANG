using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class MaGiamGiaDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();

        public List<MaGiamGia> getMaGiamGiaList()
        {
            var list = db.MaGiamGias.ToList();
            return list;
        }
        public void insert(MaGiamGia mgg, ref string ex)
        {
            try
            {
                db.MaGiamGias.Add(mgg);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                ex = e.Message;
            }
        }
        public void delete ( string maGG , ref string ex)
        {
            var mgg = db.MaGiamGias.SingleOrDefault(n => n.MaGG == maGG);
            mgg.DaXoa = true;
            
            try
            {
                db.SaveChanges();
            }
            catch(SqlException e )
            {
                ex = e.Message;
            }
        }
        public void update(MaGiamGia mgg, ref string ex)
        {
            MaGiamGia mggOnDB = db.MaGiamGias.SingleOrDefault(n => n.MaGG == mgg.MaGG);
            mggOnDB.MaGG = mgg.MaGG;
            mggOnDB.SoTienGiam = mgg.SoTienGiam;
            mggOnDB.Soluong = mgg.Soluong;
           
            try
            {
                db.SaveChanges();

            }
            catch(SqlException e)
            {
                ex = e.Message;
            }
        }
        public MaGiamGia find(string MaGG, ref string ex)
        {
            try
            {
                return db.MaGiamGias.Where(m => m.MaGG == MaGG).FirstOrDefault();

            }
            catch (Exception e)
            {
                ex = e.Message;
                return null;
            }
        }
        public List<MaGiamGia> timThongThuong(int choice, string str, ref string ex)
        {
            var list = new List<MaGiamGia>();
            switch (choice)
            {
                case 0:
                    //Tìm theo mã giảm giá 
                    list = db.MaGiamGias.Where(n => n.MaGG.Contains(str)).Select(n => n).ToList();
                    break;
            }
            return list;
        }
        public List<MaGiamGia> timNangCao(int choice, int strFrom, int strTo, ref string ex)
        {
            var list = new List<MaGiamGia>();
            switch (choice)
            {
                case 0:
                    //Tìm theo sô tiền giảm 
                    list = db.MaGiamGias.Where(n => n.SoTienGiam >= strFrom && n.SoTienGiam <= strTo).Select(n => n).ToList();
                    break;
                case 1:
                    //Tìm theo số lượng 
                    list = db.MaGiamGias.Where(n => n.Soluong >= strFrom && n.Soluong <= strTo).Select(n => n).ToList();
                    break;
            }

            return list;
        }
    }
}
