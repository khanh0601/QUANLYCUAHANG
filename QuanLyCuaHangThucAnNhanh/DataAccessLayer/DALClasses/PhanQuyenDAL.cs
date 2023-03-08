using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PhanQuyenDAL
    {
        public int KiemTraQuyen(string username, string pass)
        {
            if (username == "admin007" && pass == "iamadmin007")
            {
                return 1;
            }
            else if (username == "nhanvien002" && pass == "iamstaff002")
            {
                return 2;
            }
            else return -1;
        }

    }
}
