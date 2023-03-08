using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class DonHangLogic
    {
        DonHangDAL dal = new DonHangDAL();

        public List<DonHangMua> DonHangList()
        {
            var list = dal.getDonHangList();
            return list;
        }

        public void insert(DonHangMua donHang, ref string ex)
        {
            dal.insert(donHang, ref ex);
        }

        public void update(DonHangMua dh, ref string ex)
        {
            dal.update(dh, ref ex);
        }


        public DonHangMua find(string maDH, ref string ex)
        {
            return dal.find(maDH, ref ex);
        }

        public List<DonHangMua> timThongThuong(int choice, string str, ref string ex)
        {
            return dal.timThongThuong(choice, str, ref ex); 
        }
        public List<DonHangMua> timNangCao(int choice, int soLuongTu, int soLuongDen, DateTime start_date, DateTime end_date, ref string ex)
        {
            return dal.timNangCao(choice, soLuongTu, soLuongDen, start_date, end_date, ref ex);
        }
    }
}
