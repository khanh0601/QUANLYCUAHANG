using BussinessLogicLayer.LogicClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class formNguyenLieu : Form
    {
        // command =  | 0 --> Add | 1 --> Edit |
        // maNguyenLieu: chứa mã nguyên liệu của nguyên liệu được chọn khi thực hiện "Sửa"
        // nguyenLieuLogic: biến chứa instance LogicBussinessLayer của nguyên liệu
        private int command;
        private String maNguyenLieu;
        NguyenLieuLogic nguyenLieuLogic = new NguyenLieuLogic();

        public formNguyenLieu(int command, String maNguyenLieu)
        {
            InitializeComponent();
            this.command = command;
            this.maNguyenLieu = maNguyenLieu;
        }

        //Hàm kiểm tra exception
        public bool checkException(string ex)
        {
            if (ex != "")
            {
                MessageBox.Show(ex);
                return true;
            }
            return false;
        }

        void ClearTextBoxes(Control parent)
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
            NguyenLieu nl = new NguyenLieu();
            string ex = "";

            //Lấy dữ liệu từ các textbox
            nl.MaNL = txtMaNguyenLieu.Text.Trim();
            nl.TenNL = txtTenNguyenLieu.Text;
            nl.SoLuong = int.Parse(txtSoLuong.Text);
            nl.MaNCC = txtMaNhaCungCap.Text;
            nl.DonGia = int.Parse(txtDonGia.Text);
            nl.DaXoa = chkDaXoa.Checked ? true : false;


            switch (command)
            {
                case 0:
                    //Thêm dữ liệu
                    nguyenLieuLogic.insert(nl, ref ex);

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
                    nguyenLieuLogic.update(nl, ref ex);

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

        private void formNguyenLieu_Load(object sender, EventArgs e)
        {
            string ex = "";

            //Load dữ liệu từ nguyên liệu đã chọn 
            if (command == 1)   
            {
                //Tìm nguyên liệu từ mã nguyên liệu trong CSDL
                var nl = nguyenLieuLogic.find(this.maNguyenLieu, ref ex);

                if (checkException(ex))
                {
                    MessageBox.Show(ex);
                    return;
                }

                //Gắn dữ liệu vào các textbox
                txtMaNguyenLieu.Text = nl.MaNL;
                txtTenNguyenLieu.Text = nl.TenNL;
                txtMaNhaCungCap.Text = nl.MaNCC;
                txtDonGia.Text = nl.DonGia.ToString();
                txtSoLuong.Text = nl.SoLuong.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Thoát chương trình",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
                this.Close();
        }
    }
}
