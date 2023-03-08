using BussinessLogicLayer.LogicClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*using BussinessLogicLayer;
using DataAccessLayer;*/
namespace QuanLyCuaHangThucAnNhanh
{
    public partial class formNhanVien : Form
    {
        // command=0 khi thực hiện add
        //command =1 khi thực hiện edit
        private int command;
        private string maNhanVien;
        NhanVienLogic nhanVienLogic = new NhanVienLogic();
        public formNhanVien(int command, string maNhanVien)
        {
            InitializeComponent();
            this.command = command;
            this.maNhanVien = maNhanVien;
        }
        public string ToStringGT(int  gt)
        {
            if (gt == 1)
                return "Nam";
            return "Nữ";
        }
        public string ToStringLoai(int loai)
        {
            if (loai == 0)
                return "Part-time";
            return "Full-time";
        }
        public string ToStringChucVu(int cv)
        {
            if (cv == 1)
                return "Quản Lý";
            return "Nhân Viên";
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

        public byte ToNumberLoai(string str)
        {
            // Part-time quy định là số 0 , fulltime là số 1
            if (str.Contains("Part"))
                return 0;
            return 1;
        }

        public byte ToNumberCV(string str)
        {
            // quy định Nhân viên là 1, quản lý là 2
            if (str.Contains("Nhân viên"))
                return 2;
            return 1;
        }
        public byte ToNumberGT(string str)
        {
            if (str.Contains("Nam"))
                return 1;
            return 0;
        }
        /*        private DbNhanVien dbnv = new DbNhanVien();
        */


        private void txtMaNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtTenNhanVien.Focus();
        }

        private void txtTenNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.cbLoai.Focus();
        }

        private void cbLoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtLuong.Focus();
        }
        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.cbChucVu.Focus();
        }
        private void txtChucVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtMatKhau.Focus();
        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.btnLuuLai.Focus();
        }

        private void formNhanVien_Load(object sender, EventArgs e)
        {
            string ex = "";
            if (command == 0)
            {
                txtLuong.Enabled = false;
            }
            if (command == 1)
            {
                //Tìm nguyên liệu từ mã nguyên liệu trong CSDL
                var nv = nhanVienLogic.find(this.maNhanVien, ref ex);
                if (checkException(ex))
                {
                    MessageBox.Show(ex);
                    return;
                }

                //Gắn dữ liệu vào các textbox
                txtTenNhanVien.Text = nv.TenNV;

                txtMaNhanVien.Text = nv.MaNV;
                txtMatKhau.Text = nv.MatKhau;
                cbGioiTinh.Text =ToStringGT(nv.GioiTinh);
                txtLuong.Text = nv.Luong.ToString();
                cbLoai.Text =ToStringLoai(nv.LoaiNV);
                cbChucVu.Text =ToStringChucVu( nv.ChucVu);
               
            }
        }

        private void btnLuuLai_Click_1(object sender, EventArgs e)
        {
            //khởi tạo biến nhân viên và biến kiếm tra exception 
            NhanVien nv = new NhanVien();
            string ex = "";

            //Lấy dữ liệu từ các textbox
            nv.MaNV = txtMaNhanVien.Text.Trim();
            nv.TenNV = txtTenNhanVien.Text;
            nv.LoaiNV = ToNumberLoai(cbLoai.Text);
            //nv.Luong = Int32.Parse(txtLuong.Text);
            
            nv.ChucVu = ToNumberCV(cbChucVu.Text);
            nv.GioiTinh = ToNumberGT(cbGioiTinh.Text);
            nv.MatKhau = txtMatKhau.Text;

            nv.DaXoa = chkDaXoa.Checked ? true : false;


            switch (command)
            {
                case 0:


                    //Thêm dữ liệu
                    nhanVienLogic.insert(nv, ref ex);

                    if (checkException(ex))
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công!");
                        return;
                    }

                    MessageBox.Show("Đã thêm dữ liệu thành công!", "Thêm dữ liệu thành công",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case 1:
                    //update thông tin nhân viên 
                    nhanVienLogic.update(nv, ref ex);
                    if (checkException(ex))
                    {
                        MessageBox.Show("Update thông tin nhân viên không thành công!");
                        return;
                    }
                    MessageBox.Show("Đã sửa dữ liệu thành công!", "Sửa dữ liệu thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            ClearTextBoxes(this);
           cbChucVu.ResetText();    
            cbGioiTinh.ResetText(); 
            cbLoai.ResetText(); 

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
