using DataAccessLayer.DALClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.LogicClasses
{
    public class ChiTietDonHangLogic
    {
        ChiTietDonHangDAL dal = new ChiTietDonHangDAL();

        public List<ChiTietDonHang> ChiTietDonHangList()
        {
            var list = dal.getChiTietDonHangList();
            return list;
        }

        public void insert(ChiTietDonHang ctdh, ref string ex)
        {
            dal.insert(ctdh, ref ex);
        }

        public void update(ChiTietDonHang ctdh, ref string ex)
        {
            dal.update(ctdh, ref ex);   
        }


        public List<ChiTietDonHang> findByMaDH(string maDH)
        {
            return dal.findByMaDH(maDH);        
        }

        public ChiTietDonHang findOne(string maDH, string maSP)
        {
            return dal.findOne(maDH, maSP); 
        }

    }
}
