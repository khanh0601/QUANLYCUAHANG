using BussinessLogicLayer.LogicClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class DangNhap : Form
    {
        PhanQuyenLogic phanQuyenLogic = new PhanQuyenLogic(); 
        public DangNhap()
        {
            InitializeComponent();
        }

        public bool KiemTraQuyen(string tendangnhap, string matkhau)
        {
            /*QuanLyVeMayBayContext dbs = new QuanLyVeMayBayContext();

            // Code kiểm tra tài có quyền AD hay không
            var nv_search = dbs.NhanViens
                    .FirstOrDefault(p => p.TenDangNhap == tendangnhap && p.MatKhau == matkhau);
            if (nv_search.ChucVu.Contains("Quan tri"))
                return true;//Quyền tài khoản AD
            return false; //Quyền tài khoản nhân viên*/
            return true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (db.checkNhanVien_thongtindangnhap(txtTenDangNhap.Text, txtMatKhau.Text))
                {
                    Menu formMenu = new Menu(this.KiemTraQuyen(txtTenDangNhap.Text, txtMatKhau.Text), txtTenDangNhap.Text);
                    this.Hide();
                    formMenu.Closed += (s, args) => this.Close();
                    formMenu.Show();
                }
                else
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!",
                        "Lỗi đăng nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            if (phanQuyenLogic.KiemTraQuyen(txtTenDangNhap.Text, txtMatKhau.Text) == 1)
            {
                Menu formMenu = new Menu(true, txtTenDangNhap.Text);
                this.Hide();
                this.Closed += (s, args) => formMenu.Close();
                formMenu.Show();
            } else if (phanQuyenLogic.KiemTraQuyen(txtTenDangNhap.Text, txtMatKhau.Text) == 2)
            {
                Menu formMenu = new Menu(false, txtTenDangNhap.Text);
                this.Hide();
                this.Closed += (s, args) => formMenu.Close();
                formMenu.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ!", "Sai tài khoản hoặc mật khẩu",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMatKhau.ResetText();
            txtTenDangNhap.ResetText();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Thoát chương trình",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
                Application.Exit();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
