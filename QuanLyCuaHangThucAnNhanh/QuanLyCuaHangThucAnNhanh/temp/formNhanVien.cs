using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLogicLayer;
using DataAccessLayer;
namespace QuanLyBanVeMayBay
{
    public partial class CapNhatNhanVien : Form
    {
        public CapNhatNhanVien()
        {
            InitializeComponent();
        }
        private DbNhanVien dbnv = new DbNhanVien();

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            ////Trường hợp mã nhân viên có trong CSDL
            if (dbnv.checkNhanVien(this.txtMaNhanVien.Text.Trim()) == true)
            {
                DialogResult tl = MessageBox.Show("Bạn muốn cập nhật nhân viên lại không?", "Nhân viên đã tồn tại",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tl == DialogResult.Yes)
                {
                    NhanVien nv = new NhanVien();
                    string err = "";
                    nv.MaNhanVien = txtMaNhanVien.Text.Trim();
                    nv.HoNhanVien = txtHoNhanVien.Text;
                    nv.TenLotNhanVien = txtTenLotNhanVien.Text;
                    nv.TenNhanVien = txtTenNhanVien.Text;
                    nv.SoDienThoai = txtSoDienThoai.Text;
                    nv.ChucVu = txtChucVu.Text;
                    nv.DiaChi = txtDiaChi.Text;
                    nv.TenDangNhap = txtTenDangNhap.Text;
                    nv.MatKhau = txtMatKhau.Text;
                    if (!dbnv.updateNhanVien(ref err, nv))
                        MessageBox.Show("Thất bại!!!");
                    else
                    {
                        MessageBox.Show("Thành công!!!");
                    }
                }
            }
            //Trường hợp mã nhân viên không có trong CSDL
            else
            {
                DialogResult tl = MessageBox.Show("Bạn muốn thêm một nhân viên mới không?", "Nhân viên chưa tồn tại",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tl == DialogResult.Yes)
                {
                    NhanVien nv = new NhanVien();
                    string err = "";
                    nv.MaNhanVien = txtMaNhanVien.Text.Trim();
                    nv.HoNhanVien = txtHoNhanVien.Text;
                    nv.TenLotNhanVien = txtTenLotNhanVien.Text;
                    nv.TenNhanVien = txtTenNhanVien.Text;
                    nv.SoDienThoai = txtSoDienThoai.Text;
                    nv.ChucVu = txtChucVu.Text;
                    nv.DiaChi = txtDiaChi.Text;
                    nv.TenDangNhap = txtTenDangNhap.Text;
                    nv.MatKhau = txtMatKhau.Text;
                    if (!dbnv.insertNhanVien(ref err, nv))
                        MessageBox.Show("Thất bại!!!");
                    else
                    {
                        MessageBox.Show("Thành công!!!");
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            this.txtMaNhanVien.Clear();
            this.txtHoNhanVien.Clear();
            this.txtTenLotNhanVien.Clear();
            this.txtTenNhanVien.Clear();
            this.txtSoDienThoai.Clear();
            this.txtChucVu.Clear();
            this.txtDiaChi.Clear();
            this.txtMaNhanVien.Focus();
        }

        private void txtMaNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtHoNhanVien.Focus();
        }

        private void txtHoNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtTenLotNhanVien.Focus();

        }

        private void txtTenLotNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtTenNhanVien.Focus();
        }

        private void txtTenNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtSoDienThoai.Focus();
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtChucVu.Focus();
        }

        private void txtChucVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtDiaChi.Focus();
        }

        private void txtDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtTenDangNhap.Focus();

        }

        private void txtTenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtMatKhau.Focus();

        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.btnLuuLai.Focus();
        }

        private void CapNhatNhanVien_Load(object sender, EventArgs e)
        {

        }
    }
}
