using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class LichLamViecDAL
    {
        QUANLYCUAHANGEntities db = new QUANLYCUAHANGEntities();
        

        public List<LichLamViec> getLichLamViecList()
        {
            var list = db.LichLamViecs.ToList();
            return list;
        }
        public void insert( LichLamViec llv, ref string ex)
        {
            try
            {
                db.LichLamViecs.Add(llv);
                db.SaveChanges();
            }
            catch(SqlException e)
            {
                ex = e.Message;
            }
        }
        public LichLamViec find(string mallv, ref string ex)
        {
            try
            {
                return db.LichLamViecs.Where(n => n.MaLLV == mallv).Select(n => n).FirstOrDefault();
            }
            catch(SqlException e)
            {
                ex = e.Message;
                return null;
            }
    
        }
        public void update (LichLamViec llv , ref string ex)
        {
            var llvOnDB = db.LichLamViecs.SingleOrDefault(n => n.MaLLV == llv.MaLLV);
            llvOnDB.CaLV=llv.CaLV;
            llvOnDB.NgayLV = llv.NgayLV;
            try
            {
                db.SaveChanges();
            }
            catch(SqlException e)
            {
                ex = e.Message;
            }
        }
        public void detele(string mallv , ref string ex)
        {
            LichLamViec llv =db.LichLamViecs.SingleOrDefault(n=>n.MaLLV==mallv);    
            
        }
        public List<LichLamViec> timThongThuong(int choice , string str, ref string ex)
        {
            var list =new List<LichLamViec>();
            int m = int.Parse(str);
            switch (choice)
            {
                case 0:
                    //Tìm theo mã làm  việc 
                    list = db.LichLamViecs.Where(n => n.MaLLV.Contains(str)).Select(n => n).ToList();
                    break;
                //case 1:
                //    //Tìm theo mã nhân viên 
                
                //    break;
                case 2:
                    //Tìm theo ca làm 
                    list = db.LichLamViecs.Where(n => n.CaLV == m).Select(n => n).ToList();
                    break;

            }
            return list;
        }
        public List<LichLamViec> timNangCao(int choice , DateTime dateStart, DateTime dateEnd,ref string ex)
        {
            var list = new List<LichLamViec>();
            switch (choice)
            {
                case 0:
                    //Tìm theo ngày 
                    list =db.LichLamViecs.Where(n=>n.NgayLV>=dateStart&& n.NgayLV<=dateEnd).Select(n => n).ToList();
                    break;
            }
            return list;
        }

    }
}
