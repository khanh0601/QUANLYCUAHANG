using System;
using System.Windows.Forms;
/*using DataAccessLayer;
using BussinessLogicLayer;*/

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class Menu : Form
    {
        public bool QuyenAD; // Quyền của tài khoản, nếu = True là quyền AD, False là quyền của nhân viên
        public string TenDangNhap;
        /*        QuanLyVeMayBayContext dbs = new QuanLyVeMayBayContext();
         *        
        */
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(bool quyenAD,string tendangnhap)
        {
            InitializeComponent();
            this.QuyenAD = quyenAD;
            this.TenDangNhap = tendangnhap;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //Tạo sẵn form Danh Sách 

            DieuChinhQuyen(this.QuyenAD);
            txtNguoiDangDangNhap.Text = this.TenDangNhap;
            txtQuyenHan.Text = this.QuyenAD ? "Admin" : "Staff";
        }

        public void DieuChinhQuyen(bool quyenAD)
        {
            if (!quyenAD) //Nếu không có quyền AD thì ẩn những chức năng này đi
            {
                btnNhanVien.Enabled = false;
                btnNguyenLieu.Enabled = false;
                btnNhaCungCap.Enabled = false;
                btnDoanhThu.Enabled = false;
                btnMaGiamGia.Enabled = false;
            }
        }

        private void OpenDanhSach(string tabName)
        {
            DanhSach frmDanhSach = new DanhSach(this.QuyenAD);
            frmDanhSach.tabControl.SelectTab(tabName);
            frmDanhSach.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn đăng xuất?", "Đăng xuất",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                DangNhap dn = new DangNhap();
                this.Close();
                dn.Show();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn thoát chương trình?", "Thoát",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                Environment.Exit(1);
            }

        }

        private void btnNguyenLieu_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabNguyenLieu");
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabSanPham");

        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabNhaCungCap");

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabNhanVien");
        }

        private void btnLichLamViec_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabLichLamViec");

        }

        private void btnMaGiamGia_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabMaGiamGia");

        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabDonHang");

        }

        private void btnChiTietDonHang_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabChiTietDonHang");

        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            OpenDanhSach("tabDoanhThu");

        }

        private void btnTaoDonHang_Click(object sender, EventArgs e)
        {
            formTaoDonHang frmTaoDonHang = new formTaoDonHang();
            frmTaoDonHang.Show();
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
