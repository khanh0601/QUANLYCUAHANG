using BussinessLogicLayer.LogicClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class formNhaCungCap : Form
    {
        // command =  | 0 --> Add | 1 --> Edit |
        // maNguyenLieu: chứa mã nhà cung cấp của nhà cung cấp  được chọn khi thực hiện "Sửa"
        // nhaCungCapLogic: biến chứa instance LogicBussinessLayer của nhà cung cấp
        private int command;
        private String maNCC;
        NhaCungCapLogic nhaCungCapLogic = new NhaCungCapLogic();
        public formNhaCungCap(int command, string maNCC)
        {
            this.command = command;
            this.maNCC = maNCC;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool checkException(string ex)
        {
            if (ex != "")
            {
                MessageBox.Show(ex);
                return true;
            }
            return false;
        }
        private void ClearTextBoxes(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                TextBox textBox = child as TextBox;
                if (textBox == null)
                    ClearTextBoxes(child);
                else
                    textBox.Text = string.Empty;
            }
        }
            private void btnLuuLai_Click(object sender, EventArgs e)
        {
            //Khởi tạo biến chứa Model và biến kiểm tra exception 
            NhaCungCap ncc = new NhaCungCap();
            string ex = "";
            //lấy dữ liệu từ các textbox
            ncc.MaNCC = txtMaNCC.Text.Trim();
            ncc.TenNCC = txtTenNhaCungCap.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.DaXoa = chkDaXoa.Checked ? true : false;
            switch (command)
            {
                case 0:
                    //Thêm dữ liệu
                    nhaCungCapLogic.insert(ncc, ref ex);

                    if (checkException(ex))
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công!");
                        return;
                    }

                    MessageBox.Show("Đã thêm dữ liệu thành công!", "Sửa dữ liệu thành công",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case 1:
                    //Sửa dữ liệu
                    nhaCungCapLogic.update(ncc, ref ex);

                    if (checkException(ex))
                    {
                        MessageBox.Show("Sửa dữ liệu không thành công!");
                        return;
                    }

                    MessageBox.Show("Đã sửa dữ liệu thành công!", "Sửa dữ liệu thành công",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
            }
        }
        private void formNhaCungCap_Load(object sender, EventArgs e)
        {
            
        }
        private void bntLamMoi_Click(object sender, EventArgs e)
        {



        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Thoát chương trình",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
                this.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this);
        }

        private void formNhaCungCap_Load_1(object sender, EventArgs e)
        {
            string ex = "";

            //Load dữ liệu từ nguyên liệu đã chọn 
            if (command == 1)
            {
                //Tìm nguyên liệu từ mã nguyên liệu trong CSDL
                var ncc = nhaCungCapLogic.find(this.maNCC, ref ex);

                if (checkException(ex))
                {
                    MessageBox.Show(ex);
                    return;
                }

                //Gắn dữ liệu vào các textbox
                txtMaNCC.Text = ncc.MaNCC;
                txtTenNhaCungCap.Text = ncc.TenNCC;
                txtDiaChi.Text = ncc.DiaChi;
            }
        }
    }
}
