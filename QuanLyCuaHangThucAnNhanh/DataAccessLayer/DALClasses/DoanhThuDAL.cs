using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALClasses
{
    public class DoanhThuDAL
    {
       QUANLYCUAHANGEntities db =new QUANLYCUAHANGEntities();   
        public List<DoanhThu> getDoanhThuList()
        {
            var list =db.DoanhThus.ToList();
            return list;
        }
        public List<DoanhThu> timDoanhThu(int choice,int from , int to , DateTime dateFrom, DateTime dateTo, ref string ex)
        {
            var list = new List<DoanhThu>();
            switch (choice)
            {
                case 0:
                    //Tìm theo tổng doanh thu 
                    list = db.DoanhThus.Where(n=>n.TongThu>=from&& n.TongThu<=to).Select(n=>n).ToList();    
                    break;
                case 1:
                    //Tìm theo tổng chi
                    list = db.DoanhThus.Where(n => n.TongChi >= from && n.TongChi <= to).Select(n => n).ToList();
                    break;
                case 2:
                    //Tìm theo ngày 
                    list= db.DoanhThus.Where(n => n.Ngay >= dateFrom && n.Ngay <= dateTo).Select(n => n).ToList();
                    break;
            }

            return list;
        }
    }
}
