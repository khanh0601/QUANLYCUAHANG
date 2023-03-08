using BussinessLogicLayer.LogicClasses;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class DanhSach : Form
    {
        //Insert, update command
        const byte addCommand = 0;
        const byte updateCommand = 1;

        //Exception
        String ex = "";

        DataSet ds = new DataSet();
        NguyenLieuLogic nguyenLieuLogic = new NguyenLieuLogic();
        NhaCungCapLogic nhaCungCapLogic = new NhaCungCapLogic();
        MaGiamGiaLogic maGiamGiaLogic = new MaGiamGiaLogic();
        NhanVienLogic nhanVienLogic = new NhanVienLogic();
        DonHangLogic donHangLogic = new DonHangLogic();
        SanPhamLogic sanPhamLogic = new SanPhamLogic();
        LichLamViecLogic lichLamViecLogic = new LichLamViecLogic();
        DoanhThuLogic doanhThuLogic = new DoanhThuLogic();


        public bool QuyenCRUD;
        public DanhSach(bool quyenCRUD)
        {
            this.QuyenCRUD = quyenCRUD;
            InitializeComponent();
        }


        public void CapNhatQuyen(bool quyenCRUD)
        {
            if (!quyenCRUD)
            {
                //Disable CRUD: NguyenLieu
                btnThem_NL.Enabled = false;
                btnSua_NL.Enabled = false;
                btnXoa_NL.Enabled = false;

                //Disable CRUD: NhanVien

            }
        }

        private void DataBind()
        {
            /*ds = db.getChuyenBays();
            dgvChuyenBay.DataSource = ds.Tables[0];
            txtTimTatCa_NL.DataBindings.Clear();
            txtTimTatCa_NL.DataBindings.Add("Text", ds.Tables[0], "Mã chuyến bay");*/
        }
        private void DanhSach_Load(object sender, EventArgs e)
        {
            CapNhatQuyen(this.QuyenCRUD);
            setDgvNhaCungCap();
            setDgvNguyenLieu();
            setDgvMaGiamGia();
            setDgvNhanVien();
            setDgvDonHang();
            setdgvSanPham();
            setdgvLichLamViec();
            setDgvDoanhthu();
            setDgvNhanVien();

            btnThem_DoanhThu.Enabled = false;   
            btnSua_DoanhThu.Enabled=false;
            btnXoa_DoanhThu.Enabled = false;

            btnSua_DonHang.Enabled = false;
            btnXoa_DonHang.Enabled = false;


            if (!this.QuyenCRUD)
            {
                this.tabControl.TabPages.Remove(tabNguyenLieu);
                this.tabControl.TabPages.Remove(tabNhanVien);
                this.tabControl.TabPages.Remove(tabNhaCungCap);
                this.tabControl.TabPages.Remove(tabDoanhThu);
                this.tabControl.TabPages.Remove(tabMaGiamGia);
                btnThem_SanPham.Enabled = false;
                btnSua_SanPham.Enabled = false;
                btnXoa_SanPham.Enabled = false;
                btnThem_LichLamViec.Enabled = false;
                btnSua_LichLamViec.Enabled = false;
                btnXoa_LichLamViec.Enabled = false;
                
            }

            DataBind();

        }

        //Kiểm tra exception
        public bool checkException(string ex)
        {
            if (ex != "")
            {
                MessageBox.Show(ex);
                return true;
            }
            return false;
        }

        //Điều chỉnh độ cao từng hàng của một DataGridView
        private void setRowHeight(DataGridView dgv, int h)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Height = h;
            }
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

        void ClearRadioButtons(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                RadioButton rdb = child as RadioButton;
                if (rdb == null)
                    ClearRadioButtons(child);
                else
                    rdb.Checked = false;
            }
        }

        void ShowUnhided(DataGridView dgv, string  nameRow)
        {
            CurrencyManager currencyManager1 = (CurrencyManager) BindingContext [dgv.DataSource];
            currencyManager1.SuspendBinding();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[nameRow].Value.ToString() == "True")
                {
                    row.Visible = false;
             
                }
            }
            currencyManager1.ResumeBinding();
        }



        /* -----------------------------------------------------> NGUYÊN LIỆU ----------------------------------------------------->*/

        //Lấy toàn bộ dữ liệu của table NguyenLieu gắn lên dgv, điều chỉnh dgv
        private void setDgvNguyenLieu()
        {
            //Bind data
            dgvNguyenLieu.AutoGenerateColumns = false;
            nguyenLieuLogic = new NguyenLieuLogic();
            var listNguyenLieu = nguyenLieuLogic.NguyenLieuList();
            dgvNguyenLieu.DataSource = listNguyenLieu;

            //Modify header
            dgvNguyenLieu.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvNguyenLieu.EnableHeadersVisualStyles = false;

            foreach (DataGridViewColumn col in dgvNguyenLieu.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvNguyenLieu.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvNguyenLieu.Columns[0].Width = 150;
            dgvNguyenLieu.Columns[1].Width = 300;
            dgvNguyenLieu.Columns[2].Width = 200;
            dgvNguyenLieu.Columns[3].Width = 150;
            dgvNguyenLieu.Columns[4].Width = 200;

            //Modify each row
            setRowHeight(dgvNguyenLieu, 35);

            //Show unhided rows
            ShowUnhided(dgvNguyenLieu, "DaXoa_NL");
        }

        private void lockChoices_NguyenLieu(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5)
        {
            
            txtMaNguyenLieu.Enabled = !obj1;
            txtTenNguyenLieu.Enabled = !obj2;
            txtMaNhaCungCap_NguyenLieu.Enabled = !obj3;
            txtFrom_NguyenLieu.Enabled = !obj4;
            txtTo_NguyenLieu.Enabled = !obj5;

            txtMaNguyenLieu.BackColor = obj1 ? Color.Silver : Color.White;
            txtTenNguyenLieu.BackColor = obj2 ? Color.Silver : Color.White;
            txtMaNhaCungCap_NguyenLieu.BackColor = obj3 ? Color.Silver : Color.White;
            txtFrom_NguyenLieu.BackColor = obj4 ? Color.Silver : Color.White;
            txtTo_NguyenLieu.BackColor = obj5 ? Color.Silver : Color.White;

        }
        private void txtTimTatCa_NL_Enter(object sender, EventArgs e)
        {
            txtTimTatCa_NL.Clear();
        }

        private void btnTim_NL_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_NL_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(tabControl.SelectedTab);
            ClearRadioButtons(tabControl.SelectedTab);
            lockChoices_NguyenLieu(true, true, true, true, true);
            txtTimTatCa_NL.Text = "Nhập thông tin vào đây";
        }

        private void btnThem_NL_Click(object sender, EventArgs e)
        {
            var frmNguyenLieu = new formNguyenLieu(addCommand, null);
            frmNguyenLieu.Show();
        }

        private void btnSua_NL_Click(object sender, EventArgs e)
        {
            //Mở formNguyenLieu và hiển thị thông tin của nguyên liệu đã chọn lên form

            //Lấy mã nguyên liệu đã chọn
            string maNguyenLieu = "";

            foreach (DataGridViewRow row in dgvNguyenLieu.SelectedRows)
            {
                maNguyenLieu = row.Cells[0].Value.ToString();
            }

            var frmNguyenLieu = new formNguyenLieu(updateCommand, maNguyenLieu) ;
            frmNguyenLieu.Show();
        }

        private void btnXoa_NL_Click(object sender, EventArgs e)
        {
            ex = "";
            //Lấy mã nguyên liệu đã chọn
            string maNguyenLieu = "";

            //Lấy mã nguyên liệu từ dòng đã chọn
            foreach (DataGridViewRow row in dgvNguyenLieu.SelectedRows)
            {
                maNguyenLieu = row.Cells[0].Value.ToString();
            }

            nguyenLieuLogic.delete(maNguyenLieu, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }

            setDgvNguyenLieu();
        }

        private void btnReload_NL_Click(object sender, EventArgs e)
        {
            setDgvNguyenLieu();
        }

        private void btnTimThongThuong_NL_Click(object sender, EventArgs e)
        {
            dgvNguyenLieu.DataBindings.Clear();
            string ex = "";

            var listNguyenLieu = new List<NguyenLieu>();

            //Tìm theo mã nguyên liệu
            if (rbMaNguyenLieu.Checked)
            {
                listNguyenLieu = nguyenLieuLogic.timThongThuong(0, txtMaNguyenLieu.Text, ref ex);
            }
            //Tìm theo tên nguyên liệu
            else if (rbTenNguyenLieu.Checked)
            {
                listNguyenLieu = nguyenLieuLogic.timThongThuong(1, txtTenNguyenLieu.Text, ref ex);
            }
            //Tìm theo mã nhà cung cấp
            else if (rbMaNhaCungCap.Checked)
            {
                listNguyenLieu = nguyenLieuLogic.timThongThuong(2, txtMaNhaCungCap_NguyenLieu.Text, ref ex);
            }

            //Gắn dữ liệu lên dgv 
            dgvNguyenLieu.DataSource = listNguyenLieu;
            setRowHeight(dgvNguyenLieu, 35);
            ShowUnhided(dgvNguyenLieu, "DaXoa_NL");
        }

        private void btnTimNangCao_NL_Click(object sender, EventArgs e)
        {
            dgvNguyenLieu.DataBindings.Clear();
            string ex = "";

            var listNguyenLieu = new List<NguyenLieu>();
            var soLuongTu = int.Parse(txtFrom_NguyenLieu.Text);
            var soLuongDen = int.Parse(txtTo_NguyenLieu.Text);

            //Tìm theo số lượng còn lại
            if (rbSoLuongConLai.Checked)
            {
                listNguyenLieu = nguyenLieuLogic.timNangCao(0, soLuongTu, soLuongDen, ref ex);
            }
            //Tìm theo đơn giá
            else if (rbDonGiaNguyenLieu.Checked)
            {
                listNguyenLieu = nguyenLieuLogic.timNangCao(1, soLuongTu, soLuongDen, ref ex);
            }

            //Gắn dữ liệu lên dgv
            dgvNguyenLieu.DataSource = listNguyenLieu;
            setRowHeight(dgvNguyenLieu, 35);
            ShowUnhided(dgvNguyenLieu, "DaXoa_NL");
        }

        private void rbMaNguyenLieu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NguyenLieu(false, true, true, true, true);
        }

        private void rbTenNguyenLieu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NguyenLieu(true, false, true, true, true);

        }

        private void rbMaNhaCungCap_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NguyenLieu(true, true, false, true, true);

        }

        private void rbSoLuongConLai_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NguyenLieu(true, true, true, false, false);

        }

        private void rbDonGiaNguyenLieu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NguyenLieu(true, true, true, false, false);

        }
        /* -----------------------------------------------------> NGUYÊN LIỆU ----------------------------------------------------->*/


        /* -----------------------------------------------------> NHÂN VIÊN ----------------------------------------------------->*/
        private void lockChoices_NhanVien(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5, bool obj6, bool obj7)
        {
            txtMaNhanVien.Enabled = !obj1;
            txtTenNhanVien.Enabled = !obj2;
            txtFrom_NhanVien.Enabled = !obj6;
            txtTo_NhanVien.Enabled = !obj7;

            txtMaNhanVien.BackColor = obj1 ? Color.Silver : Color.White;
            txtTenNhanVien.BackColor = obj2 ? Color.Silver : Color.White;
            cbGioiTinh.BackColor = obj3 ? Color.Silver : Color.White; ;
            cbLoai.BackColor = obj4 ? Color.Silver : Color.White;
            cbChucVu.BackColor = obj5 ? Color.Silver : Color.White;
            txtFrom_NhanVien.BackColor = obj6 ? Color.Silver : Color.White;
            txtTo_NhanVien.BackColor = obj7 ? Color.Silver : Color.White;

        }

        private void setDgvNhanVien()
        {
            //Bind data
            dgvNhanVien.AutoGenerateColumns = false;
            nhanVienLogic = new NhanVienLogic();
            var listNhanVien = nhanVienLogic.NhanVienList();
            dgvNhanVien.DataSource = listNhanVien;

            //Mapping row value
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                row.Cells["GioiTinhStr"].Value = nhanVienLogic.ToStringGT(row.Cells["GioiTinhInt"].Value.ToString());
                row.Cells["LoaiNVStr"].Value = nhanVienLogic.ToStringLoai(row.Cells["LoaiNVInt"].Value.ToString());
                row.Cells["ChucVuStr"].Value = nhanVienLogic.ToStringChucVu(row.Cells["ChucVuInt"].Value.ToString());
            }

            ShowUnhided(dgvNhanVien, "DaXoa_NV");


            //Modify header
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvNhanVien.EnableHeadersVisualStyles = false;

            foreach (DataGridViewColumn col in dgvNhanVien.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvNhanVien.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvNhanVien.Columns[0].Width = 130;
            dgvNhanVien.Columns[2].Width = 250;
            dgvNhanVien.Columns[3].Width = 150;
            dgvNhanVien.Columns[4].Width = 150;
            dgvNhanVien.Columns[5].Width = 200;

            //Modify each row
            setRowHeight(dgvNhanVien, 35);

            

        }
        private void txtTimTatCa_NhanVien_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_NhanVien_Click(object sender, EventArgs e)
        {
                
        }

        private void btnReset_NhanVien_Click(object sender, EventArgs e)
        {

            ClearTextBoxes(tabControl.SelectedTab);
            ClearRadioButtons(tabControl.SelectedTab);
            lockChoices_NhanVien(true, true, true, true, true,true, true);
            txtTimTatCa_NL.Text = "Nhập thông tin vào đây";
        }

        private void btnThem_NhanVien_Click(object sender, EventArgs e)
        {
            var frmNhanVien = new formNhanVien(addCommand, null); 
            frmNhanVien.Show();

        }

        private void btnSua_NhanVien_Click(object sender, EventArgs e)
        {
            //Mở formNhanVien và hiển thị thông tin của nhân viên đã chọn lên form

            //Lấy mã nhân viên đã chọn
            string maNV = "";

            foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
            {
                maNV = row.Cells[0].Value.ToString();
            }

            var frmNhanVien = new formNhanVien(updateCommand, maNV);
            frmNhanVien.Show();
        }

        private void btnXoa_NhanVien_Click(object sender, EventArgs e)
        {
            ex = "";
            //Lấy mã nhân viên đã chọn
            string maNV = "";

            //Lấy mã Nhân viên từ dòng đã chọn
            foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
            {
                maNV = row.Cells[0].Value.ToString();
            }

            nhanVienLogic.delete(maNV, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }

            setDgvNhanVien();
        }

        private void btnReload_NhanVien_Click(object sender, EventArgs e)
        {
            setDgvNhanVien();
        }

        private void btnTimThongThuong_NhanVien_Click(object sender, EventArgs e)
        {
            dgvNhanVien.DataBindings.Clear();
            string ex = "";

            var listNhanVien = new List<NhanVien>();

            //Tìm theo mã nhân viên
            if (rbMaNhanVien.Checked)
            {
                listNhanVien = nhanVienLogic.timThongThuong(0, txtMaNhanVien.Text, ref ex);
            }
            //Tìm theo tên nhân viên 
            else if (rbTenNhanVien.Checked)
            {
                listNhanVien = nhanVienLogic.timThongThuong(1, txtTenNhanVien.Text, ref ex);
            }
            //Tìm theo giới tính 
            else if (true)
            {
              
                listNhanVien = nhanVienLogic.timThongThuong(2, cbGioiTinh.Text, ref ex);
            }

            //Gắn dữ liệu lên dgv 
            dgvNhanVien.DataSource = listNhanVien;
            setRowHeight(dgvNhaCungCap, 35);
            ShowUnhided(dgvNhanVien, "DaXoa_NV");
        }

        private void btnTimNangCao_NhanVien_Click(object sender, EventArgs e)
        {
            dgvNhanVien.DataBindings.Clear();
            string ex = "";

            var listNhanVien = new List<NhanVien>();

            //Tìm theo lương
            if (rbLuongTheoGio.Checked)
            {
                listNhanVien = nhanVienLogic.timNangCao(0,txtFrom_NhanVien.Text,txtTo_NhanVien.Text , ref ex);
            }
            //Tìm theo kiểu nhân viên full-time hay là part-time 
            else if (rbLoai.Checked)
            {
                listNhanVien = nhanVienLogic.timNangCao(1, cbLoai.Text,cbLoai.Text, ref ex);
            }
            //Tìm theo chức vụ
            else if (rbChucVu.Checked)
            {

                listNhanVien = nhanVienLogic.timNangCao(2, cbChucVu.Text,cbChucVu.Text, ref ex);
            }

            //Gắn dữ liệu lên dgv 
            dgvNhanVien.DataSource = listNhanVien;
            setRowHeight(dgvNhaCungCap, 35);
            ShowUnhided(dgvNhanVien, "DaXoa_NV");
        }

        private void rbMaNhanVien_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhanVien(false, true, true, true, true, true, true);
        }

        private void rdTenNhanVien_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhanVien(true, false, true, true, true, true, true);
        }

        private void rbGioiTinh_CheckedChanged_1(object sender, EventArgs e)
        {
            lockChoices_NhanVien(true, true, false, true, true, true, true);

        }

        private void rbLuongTheoGio_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhanVien(true, true, true, true, true, false, false);
        }

        private void rbLoai_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhanVien(true, true, true, false, true, true, true);
        }

        private void rbChucVu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhanVien(true, true, true, true, false, true, true);
        }
        /* -----------------------------------------------------> NHÂN VIÊN ----------------------------------------------------->*/


        /* -----------------------------------------------------> NHÀ CUNG CẤP ----------------------------------------------------->*/
        private void lockChoices_NhaCungCap(bool obj1, bool obj2, bool obj3)
        {
            txtMaNhaCungCap_NhaCungCap.Enabled = !obj1;
            txtTenNhaCungCap.Enabled = !obj2;
            txtDiaChi.Enabled = !obj3;

            txtMaNhaCungCap_NhaCungCap.BackColor = obj1 ? Color.Silver : Color.White;
            txtTenNhaCungCap.BackColor = obj2 ? Color.Silver : Color.White;
            txtDiaChi.BackColor = obj3 ? Color.Silver : Color.White;

        }

        private void setDgvNhaCungCap()
        {
            //Bind data
            dgvNhaCungCap.AutoGenerateColumns = false;
            nhaCungCapLogic= new NhaCungCapLogic(); 
            var listNhaCungCap = nhaCungCapLogic.NhaCungCapList();
            dgvNhaCungCap.DataSource = listNhaCungCap;

            //Modify header
            dgvNhaCungCap.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvNhaCungCap.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvNhaCungCap.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvNhaCungCap.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvNhaCungCap.Columns[0].Width = 200;
            dgvNhaCungCap.Columns[1].Width = 400;
            dgvNhaCungCap.Columns[2].Width = 400;

            //Modify each row
            setRowHeight(dgvNhaCungCap, 35);
            ShowUnhided(dgvNhaCungCap,"DaXoa_NCC");
        }
            private void txtTimTatCa_NhaCungCap_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_NhaCungCap_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_NhaCungCap_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(tabControl.SelectedTab);
            ClearRadioButtons(tabControl.SelectedTab);
            lockChoices_NhaCungCap(true, true, true);
            txtTimTatCa_NL.Text = "Nhập thông tin vào đây";
        }

        private void btnThem_NhaCungCap_Click(object sender, EventArgs e)
        {
            var frmNhaCungCap = new formNhaCungCap(addCommand, null); 
            frmNhaCungCap.Show();
        }

        private void btnSua_NhaCungCap_Click(object sender, EventArgs e)
        {
            //Mở formNhaCungCap và hiển thị thông tin của nhà cung cấp đã chọn lên form

            //Lấy mã nhà cung cấp đã chọn
            string maNCC = "";

            foreach (DataGridViewRow row in dgvNhaCungCap.SelectedRows)
            {
                maNCC = row.Cells[0].Value.ToString();
            }

            var frmNhaCungCap = new formNhaCungCap(updateCommand, maNCC);
            frmNhaCungCap.Show();
            
        }

        private void btnXoa_NhaCungCap_Click(object sender, EventArgs e)
        {
            ex = "";
            //Lấy mã nhà cung cấp đã chọn
            string maNCC = "";

            //Lấy mã nhà cung cấp  từ dòng đã chọn
            foreach (DataGridViewRow row in dgvNhaCungCap.SelectedRows)
            {
                maNCC = row.Cells[0].Value.ToString();
            }

            nhaCungCapLogic.delete(maNCC, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }

            setDgvNhaCungCap();
        }

        private void btnReload_NhaCungCap_Click(object sender, EventArgs e)
        {
            setDgvNhaCungCap();
        }

        private void btnTimThongThuong_NhaCungCap_Click(object sender, EventArgs e)
        {
            dgvNhaCungCap.DataBindings.Clear();
            string ex = "";

            var listNhaCungCap = new List<NhaCungCap>();

            //Tìm theo mã nguyên liệu
            if (rbNhaCungCap.Checked)
            {
                listNhaCungCap = nhaCungCapLogic.timThongThuong(0, txtMaNhaCungCap_NhaCungCap.Text, ref ex);
            }
            //Tìm theo tên nguyên liệu
            else if (rbTenNhaCungCap.Checked)
            {
                listNhaCungCap = nhaCungCapLogic.timThongThuong(1, txtTenNhaCungCap.Text, ref ex);
            }
            //Tìm theo mã nhà cung cấp
            else if (rbDiaChi.Checked)
            {
                listNhaCungCap = nhaCungCapLogic.timThongThuong(2, txtDiaChi.Text, ref ex);
            }

            //Gắn dữ liệu lên dgv 
            dgvNhaCungCap.DataSource = listNhaCungCap;
            setRowHeight(dgvNhaCungCap, 35);
            ShowUnhided(dgvNhaCungCap, "DaXoa_NCC");
        }

        private void rbNhaCungCap_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhaCungCap(false, true, true);

        }

        private void rbTenNhaCungCap_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_NhaCungCap(true, false, true);
        }


        private void rbDiaChi_CheckedChanged(object sender, EventArgs e)
        {

            lockChoices_NhaCungCap(true, true, false);
        }

        /* -----------------------------------------------------> NHÀ CUNG CẤP ----------------------------------------------------->*/


        /* -----------------------------------------------------> SẢN PHẨM ----------------------------------------------------->*/
        private void lockChoices_SanPham(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5, bool obj6)
        {

            txtMaSanPham.Enabled = !obj1;
            txtTenSanPham.Enabled = !obj2;
            txtFrom_SanPham.Enabled = !obj3;
            txtTo_SanPham.Enabled = !obj4;
            dtpFrom_SanPham.Enabled = !obj5;
            dtpTo_SanPham.Enabled = !obj6;

            txtMaSanPham.BackColor = obj1 ? Color.Silver : Color.White;
            txtTenSanPham.BackColor = obj2 ? Color.Silver : Color.White;
            txtFrom_SanPham.BackColor = obj3 ? Color.Silver : Color.White;
            txtTo_SanPham.BackColor = obj4 ? Color.Silver : Color.White;

        }
        private void setdgvSanPham()
        {
            //Bind data
            dgvSanPham.AutoGenerateColumns = false;
            sanPhamLogic = new SanPhamLogic();
            var listSanPham = sanPhamLogic.SanPhamList();
            dgvSanPham.DataSource = listSanPham;

            //Modify header
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvSanPham.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvSanPham.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvSanPham.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvSanPham.Columns[0].Width = 80;
            dgvSanPham.Columns[1].Width = 120;
            dgvSanPham.Columns[2].Width = 100;
            dgvSanPham.Columns[3].Width = 100;
            dgvSanPham.Columns[4].Width = 80;
            dgvSanPham.Columns[5].Width = 80;
            dgvSanPham.Columns[6].Width = 120;
            dgvSanPham.Columns[7].Width = 70;
            dgvSanPham.Columns[8].Width = 230;


            //Modify each row
            setRowHeight(dgvSanPham, 35);
            ShowUnhided(dgvSanPham, "DaXoa_SP");
        }

        private void txtTimTatCa_SanPham_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_SanPham_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_SanPham_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(tabControl.SelectedTab);
            ClearRadioButtons(tabControl.SelectedTab);
            lockChoices_SanPham(true, true, true,true,true, true);
            txtTimTatCa_NL.Text = "Nhập thông tin vào đây";
        }

        private void btnThem_SanPham_Click(object sender, EventArgs e)
        {

            var frmSanPham = new formSanPham(addCommand,null);
            frmSanPham.Show();
        }

        private void btnSua_SanPham_Click(object sender, EventArgs e)
        {
            string masp = "";

            foreach (DataGridViewRow row in dgvSanPham.SelectedRows)
            {
                masp = row.Cells[0].Value.ToString();
            }

            var frmsp = new formSanPham(updateCommand, masp);
            frmsp.Show();
        }
        private void btnXoa_SanPham_Click(object sender, EventArgs e)
        {
            ex = "";
            //Lấy mã sản phẩm đã chọn
            string maSP = "";

            //Lấy mã sản phẩm từ dòng đã chọn
            foreach (DataGridViewRow row in dgvSanPham.SelectedRows)
            {
                maSP = row.Cells[0].Value.ToString();
            }

            sanPhamLogic.delete(maSP, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }

            setdgvSanPham();
        }

        private void btnReload_SanPham_Click(object sender, EventArgs e)
        {
            setdgvSanPham();
        }

        private void btnTimThongThuong_SanPham_Click(object sender, EventArgs e)
        {
            var list =new List<SanPham>();
            if (rbMaSanPham.Checked)
            {
                list = sanPhamLogic.timThongThuong(0, txtMaSanPham.Text, ref ex);
            }
            else if (rbTenSanPham.Checked)
            {
                list = sanPhamLogic.timThongThuong(1, txtTenSanPham.Text, ref ex);
            }

            dgvSanPham.DataSource = list;
            setRowHeight(dgvSanPham, 35);
            ShowUnhided(dgvSanPham, "DaXoa_SP");
        }

        private void btnTimNangCao_SanPham_Click(object sender, EventArgs e)
        {
            int from = int.Parse(txtFrom_SanPham.Text);
            int to =int.Parse(txtTo_SanPham.Text);
            DateTime dateStart =  dtpFrom_SanPham.Value;
            DateTime dateEnd = dtpTo_SanPham.Value;
            var list = new List<SanPham>(); 
            if (rbChiPhiSanXuat.Checked)
            {
                list = sanPhamLogic.timNangCao(0, from, to, new DateTime() , new DateTime(), ref ex);
            }
            else if (rbDonGiaSanPham.Checked)
            {
                list = sanPhamLogic.timNangCao(1, from, to, new DateTime(), new DateTime(), ref ex);
            }
            else if (rbSoLuongTon_SanPham.Checked)
            {
                list = sanPhamLogic.timNangCao(2, from, to, new DateTime(), new DateTime(), ref ex);
            }  
            else if (rbSoLuongDaBan_SanPham.Checked)
            {
                list = sanPhamLogic.timNangCao(3, from, to, new DateTime(), new DateTime(), ref ex);
            }
            else if (rbNgayCapNhat_SanPham.Checked)
            {
                list = sanPhamLogic.timNangCao(4, 0, 0, dateStart, dateEnd, ref ex);
            }
            dgvSanPham.DataSource = list;
            setRowHeight(dgvSanPham, 35);
            ShowUnhided(dgvSanPham, "DaXoa_SP");
        }

        private void rbMaSanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(false, true, true, true, true, true);
        }

        private void rbTenSanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(true, false, true, true, true, true); ;
        }

        private void rbChiPhiSanXuat_CheckedChanged(object sender, EventArgs e)
        {

            lockChoices_SanPham(true, true, false, false, true, true);
        }

        private void rbDonGiaSanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(true, true, false, false, true, true);
        }

        private void rbSoLuongTon_SanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(true, true, false, false, true, true);
        }

        private void rbSoLuongDaBan_SanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(true, true, false, false, true, true);
        }

        private void rbNgayCapNhat_SanPham_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_SanPham(true, true, true, true, false, false);
        }
        /* -----------------------------------------------------> SẢN PHẨM ----------------------------------------------------->*/


        /* -----------------------------------------------------> LỊCH LÀM VIỆC ----------------------------------------------------->*/
        private void lockChoices_LichLamViec(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5)
        {
            txtMaLichLamViec.Enabled = !obj1;
            txtMaNhanVien_LichLamViec.Enabled = !obj2;
            cbCaLam.Enabled = !obj3;
            dtpFrom_LichLamViec.Enabled = !obj4;
            dtpTo_LichLamViec.Enabled = !obj5;

            txtMaLichLamViec.BackColor = obj1 ? Color.Silver : Color.White;
            txtMaNhanVien_LichLamViec.BackColor = obj2 ? Color.Silver : Color.White;
            cbCaLam.BackColor = obj3 ? Color.Silver : Color.White;

        }
        private void setdgvLichLamViec()
        {
            //Bind data
            dgvLichLamViec.AutoGenerateColumns = false;
            lichLamViecLogic = new LichLamViecLogic ();
            var listSanPham = lichLamViecLogic.LichLamViecList();
            dgvLichLamViec.DataSource = listSanPham;

            //Modify header
            dgvLichLamViec.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvLichLamViec.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvLichLamViec.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvLichLamViec.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvLichLamViec.Columns[0].Width = 400;
            dgvLichLamViec.Columns[1].Width = 200;
            dgvLichLamViec.Columns[2].Width = 400;
           
            



            //Modify each row
            setRowHeight(dgvLichLamViec, 35);
            
        }


        private void txtTimTatCa_LichLamViec_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_LichLamViec_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_LichLamViec_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(tabControl.SelectedTab);
            ClearRadioButtons(tabControl.SelectedTab);
            lockChoices_SanPham(true, true, true, true, true, true);
            txtTimTatCa_NL.Text = "Nhập thông tin vào đây";
        }

        private void btnThem_LichLamViec_Click(object sender, EventArgs e)
        {
            var frmLichLamViec = new formLichLamViec(addCommand,null);
            frmLichLamViec.Show();
        }

        private void btnSua_LichLamViec_Click(object sender, EventArgs e)
        {
            string mallv = "";
            foreach (DataGridViewRow row in dgvLichLamViec.SelectedRows)
            {
                mallv = row.Cells[0].Value.ToString();
            }
            formLichLamViec frmLLV = new formLichLamViec(updateCommand, mallv);
            frmLLV.Show();
        }

        private void btnXoa_LichLamViec_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_LichLamViec_Click(object sender, EventArgs e)
        {
            setdgvLichLamViec();
        }

        private void btnTimThongThuong_LichLamViec_Click(object sender, EventArgs e)
        {
            var list = new List<LichLamViec>() ;
            if (rbMaLichLamViec.Checked)
            {
                list = lichLamViecLogic.timthongThuong(0, txtMaLichLamViec.Text, ref ex);
            }
            else if (rbCaLam.Checked)
            {
                list = lichLamViecLogic.timthongThuong(2, cbCaLam.Text, ref ex);
            }
            dgvLichLamViec.DataSource = list;
            setRowHeight(dgvLichLamViec, 35);
            ShowUnhided(dgvLichLamViec, "DaXoa_LLV");
        }

        private void btnTimNangCao_LichLamViec_Click(object sender, EventArgs e)
        {
            DateTime dateStart = dtpFrom_LichLamViec.Value;
            DateTime dateEnd = dtpTo_LichLamViec.Value;
            var list =new List<LichLamViec>() ;
            if (rbThoiGian.Checked)
            {
                list = lichLamViecLogic.timNangCao(0, dateStart, dateEnd, ref ex);
            }
            dgvLichLamViec.DataSource = list;
            setRowHeight(dgvLichLamViec, 35);
            ShowUnhided(dgvLichLamViec, "DaXoa_LLV");
        }

        private void rbMaLichLamViec_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_LichLamViec(false, true, true, true, true);
        }

        private void rbMaNhanVien_LichLamViec_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_LichLamViec(true, false, true, true, true);
        }

        private void rbCaLam_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_LichLamViec(true, true, false, true, true);
        }

        private void rbThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_LichLamViec(true, true, true, false, false);

        }


        /* -----------------------------------------------------> LỊCH LÀM VIỆC ----------------------------------------------------->*/


        /* -----------------------------------------------------> MÃ GIẢM GIÁ ----------------------------------------------------->*/
        private void lockChoices_MaGiamGia(bool obj1, bool obj2, bool obj3)
        {
            txtMaGiamGia.Enabled = !obj1;
            txtFrom_MaGiamGia.Enabled = !obj2;
            txtTo_MaGiamGia.Enabled = !obj3;

            txtMaGiamGia.BackColor = obj1 ? Color.Silver : Color.White;
            txtFrom_MaGiamGia.BackColor = obj2 ? Color.Silver : Color.White;
            txtTo_MaGiamGia.BackColor = obj3 ? Color.Silver : Color.White;



        }
        private void setDgvMaGiamGia()
        {
            //Bind data
            dgvMaGiamGia.AutoGenerateColumns = false;
            maGiamGiaLogic = new MaGiamGiaLogic();
            var listMaGiamGia = maGiamGiaLogic.MaGiamGiaList();
            dgvMaGiamGia.DataSource = listMaGiamGia;

            //Modify header
            dgvMaGiamGia.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvMaGiamGia.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvMaGiamGia.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvMaGiamGia.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvMaGiamGia.Columns[0].Width = 400;
            dgvMaGiamGia.Columns[1].Width = 400;
            dgvMaGiamGia.Columns[2].Width = 200;

            //Modify each row
            setRowHeight(dgvMaGiamGia, 35);
            ShowUnhided(dgvMaGiamGia,"DaXoa_MGG");
            
        }

        private void txtTimTatCa_MaGiamGia_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_MaGiamGia_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_MaGiamGia_Click(object sender, EventArgs e)
        {
            ClearRadioButtons(tabControl.SelectedTab);
            ClearTextBoxes(tabControl.SelectedTab);
            lockChoices_MaGiamGia(true, true, true);
            txtTimTatCa_MaGiamGia.Text = "Nhập mã giảm giá vào đây ";
        }

        private void btnThem_MaGiamGia_Click(object sender, EventArgs e)
        {
            var frmMaGiamGia = new formMaGiamGia(addCommand, null); 
            frmMaGiamGia.Show();
        }

        private void btnSua_MaGiamGia_Click(object sender, EventArgs e)
        {
            string maGG = "";

            foreach (DataGridViewRow row in dgvMaGiamGia.SelectedRows)
            {
                maGG = row.Cells[0].Value.ToString();
            }

            var frmMaGiamGia = new formMaGiamGia(updateCommand, maGG);
            frmMaGiamGia.Show();
        }

        private void btnXoa_MaGiamGia_Click(object sender, EventArgs e)
        {
            ex = "";
            //Lấy mã mã giảm giá  đã chọn
            string maGG = "";

            //Lấy mã mã giảm giá từ dòng đã chọn
            foreach (DataGridViewRow row in dgvMaGiamGia.SelectedRows)
            {
                maGG = row.Cells[0].Value.ToString();
            }

            maGiamGiaLogic.delete(maGG, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }
            
            setDgvMaGiamGia();
        }

        private void btnReload_MaGiamGia_Click(object sender, EventArgs e)
        {
            setDgvMaGiamGia();
        }

        private void btnTimThongThuong_MaGiamGia_Click(object sender, EventArgs e)
        {
            dgvMaGiamGia.DataBindings.Clear();
            var listMaGiamGia = new List<MaGiamGia>();
            string ex = "";
            if (rbMaGiamGia.Checked)
            {
                //Tìm theo mã giảm giá 
                listMaGiamGia = maGiamGiaLogic.timThongThuong(0, txtMaGiamGia.Text,ref ex);
            }
            
            dgvMaGiamGia.DataSource = listMaGiamGia;
            setRowHeight(dgvMaGiamGia, 35);
            ShowUnhided(dgvMaGiamGia, "DaXoa_MGG");
        }

        private void btnTimNangCao_MaGiamGia_Click(object sender, EventArgs e)
        {
            dgvMaGiamGia.DataBindings.Clear();
            var listMaGiamGia = new List<MaGiamGia>();
            string ex = "";
            int from = int.Parse(txtFrom_MaGiamGia.Text);
            int to = int.Parse(txtTo_MaGiamGia.Text);
            if (rbSoTienGiam.Checked)
            {
                //Tìm theo số tiền giảm 
                listMaGiamGia = maGiamGiaLogic.timNangCao(0, from, to, ref ex);
            }
            if (rbSoLuong_MaGiamGia.Checked)
            {
                //Tìm theo số tiền giảm 
                listMaGiamGia = maGiamGiaLogic.timNangCao(1, from, to, ref ex);
            }
            dgvMaGiamGia.DataSource = listMaGiamGia;
            setRowHeight(dgvMaGiamGia, 35);
            ShowUnhided(dgvMaGiamGia, "DaXoa_MGG");
        }

        private void rbMaGiamGia_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_MaGiamGia(false, true, true);
        }

        private void rbSoTienGiam_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_MaGiamGia(true, false, false);
        }

        private void rbSoLuong_MaGiamGia_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_MaGiamGia(true, false, false);
        }
        /* -----------------------------------------------------> MÃ GIẢM GIÁ ----------------------------------------------------->*/




        /* -----------------------------------------------------> ĐƠN HÀNG  ----------------------------------------------------->*/
        private void lockChoices_DonHang(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5, bool obj6, bool obj7)
        {
            txtMaDonHang_DonHang.Enabled = !obj1;
            txtMaNhanVien_DonHang.Enabled = !obj2;
            txtMaGiamGia_DonHang.Enabled = !obj3;
            txtFrom_DonHang.Enabled = !obj4;
            txtTo_DonHang.Enabled = !obj5;
            dtpFrom_DonHang.Enabled = !obj6;
            dtpTo_DonHang.Enabled = !obj7;

            txtMaDonHang_DonHang.BackColor = obj1 ? Color.Silver : Color.White;
            txtMaNhanVien_DonHang.BackColor = obj2 ? Color.Silver : Color.White;
            txtMaGiamGia_DonHang.BackColor = obj3 ? Color.Silver : Color.White;
            txtFrom_DonHang.BackColor = obj4 ? Color.Silver : Color.White;
            txtTo_DonHang.BackColor = obj5 ? Color.Silver : Color.White;
        }

        private void setDgvDonHang()
        {
            //Bind data
            dgvDonHang.AutoGenerateColumns = false;
            var listDonHang = donHangLogic.DonHangList();
            dgvDonHang.DataSource = listDonHang ;

            //Modify header
            dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvDonHang.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvDonHang.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvDonHang.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvDonHang.Columns[0].Width = 200;
            dgvDonHang.Columns[1].Width = 200;
            dgvDonHang.Columns[2].Width = 200;
            dgvDonHang.Columns[3].Width = 200;
            dgvDonHang.Columns[4].Width = 200;


            //Modify each row
            setRowHeight(dgvDonHang, 50);
        }

        private void txtTimTatCa_DonHang_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_DonHang_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_DonHang_Click(object sender, EventArgs e)
        {
            ClearRadioButtons(tabControl.SelectedTab);
            ClearTextBoxes(tabControl.SelectedTab);
            lockChoices_DonHang(true, true, true, true, true, true, true);
            txtTimTatCa_DonHang.Text = "Nhập thông tin đơn hàng vào đây ";
        }

        private void btnThem_DonHang_Click(object sender, EventArgs e)
        {
            var frmDonHang = new formTaoDonHang();
            frmDonHang.Show();
        }

        
        private void btnSua_DonHang_Click(object sender, EventArgs e)
        //Mở formDonHang và hiển thị thông tin chi tiết đơn hàng đã chọn lên form
        {

            //Lấy mã đơn hàng đã chọn
            string maDonHang = "";

            foreach (DataGridViewRow row in dgvDonHang.SelectedRows)
            {
                maDonHang = row.Cells[0].Value.ToString();
            }

            var frmDonHang = new formDonHang(maDonHang);
            frmDonHang.Show();
        }

        private void btnXoa_DonHang_Click(object sender, EventArgs e)
        {
            /*ex = "";
            //Lấy mã nguyên liệu đã chọn
            string maDonHang = "";

            //Lấy mã nguyên liệu từ dòng đã chọn
            foreach (DataGridViewRow row in dgvDonHang.SelectedRows)
            {
                maDonHang = row.Cells[0].Value.ToString();
            }

            donHangLogic.delete(maDonHang, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Xóa dữ liệu không thành công!");
                return;
            }

            setDgvNguyenLieu();*/
        }

        private void btnReload_DonHang_Click(object sender, EventArgs e)
        {
            setDgvDonHang();
        }

        private void btnTimThongThuong_DonHang_Click(object sender, EventArgs e)
        {
            dgvDonHang.DataBindings.Clear();
            string ex = "";

            var listDonHang = new List<DonHangMua>();

            //Tìm theo mã nguyên liệu
            if (rbMaDonHang_DonHang.Checked)
            {
                listDonHang = donHangLogic.timThongThuong(0, txtMaDonHang_DonHang.Text, ref ex);
            }
            //Tìm theo mã nhân viên
            else if (rbMaNhanVien_DonHang.Checked)
            {
                listDonHang = donHangLogic.timThongThuong(1, txtMaNhanVien_DonHang.Text, ref ex);
            }
            //Tìm theo mã giảm giá
            else if (rbMaGiamGia_DonHang.Checked)
            {
                listDonHang = donHangLogic.timThongThuong(2, txtMaGiamGia_DonHang.Text, ref ex);
            }

            //Gắn dữ liệu lên dgv 
            dgvDonHang.DataSource = listDonHang;
            setRowHeight(dgvDonHang, 50);
        }

        private void btnTimNangCao_DonHang_Click(object sender, EventArgs e)
        {
            dgvDonHang.DataBindings.Clear();
            var listDonHang = new List<DonHangMua>();
            string ex = "";
            int from = txtFrom_DonHang.Text != "" ? int.Parse(txtFrom_DonHang.Text) : 0;
            int to = txtTo_DonHang.Text != "" ? int.Parse(txtTo_DonHang.Text) : 0;
            DateTime start_date = dtpFrom_DonHang.Value;
            DateTime end_date = dtpTo_DonHang.Value;
            if (rbTongGiaTri_DonHang.Checked)
            {
                //Tìm theo số  tiền đơn hàng
                listDonHang = donHangLogic.timNangCao(0, from, to, new DateTime(), new DateTime(), ref ex);
            }
            if (rbNgayTao_DonHang.Checked)
            {
                //Tìm theo số tiền giảm 
                listDonHang = donHangLogic.timNangCao(1, 0, 0, start_date, end_date, ref ex);
            }
            dgvDonHang.DataSource = listDonHang;
            setRowHeight(dgvDonHang, 50);
        }

        private void rbMaDonHang_DonHang_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DonHang(false, true, true, true, true, true, true);
        }

        private void rbMaNhanVien_DonHang_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DonHang(true, false, true, true, true, true, true);
        }

        private void rbMaGiamGia_DonHang_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DonHang(true, true, false, true, true, true, true);
        }

        private void rbTongGiaTri_DonHang_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DonHang(true, true, true, false, false, true, true);
        }

        private void rbNgayTao_DonHang_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DonHang(true, true, true, true, true, false, false);
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatQuyen(this.QuyenCRUD);
            setDgvNhaCungCap();
            setDgvNguyenLieu();
            setDgvMaGiamGia();
            setDgvNhanVien();
            setDgvDonHang();
            setdgvSanPham();
            setdgvLichLamViec();
            setDgvDoanhthu();
            DataBind();
        }
         // ------------------------Doanh Thu---------------------------------------
        private void dgvDoanhThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void setDgvDoanhthu()
        {
            //Bind data
            dgvDoanhThu.AutoGenerateColumns = false;
            var listDoanhThu = doanhThuLogic.DoanhThuList();
            dgvDoanhThu.DataSource = listDoanhThu;

            //Modify header
            dgvDoanhThu.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvDoanhThu.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dgvDoanhThu.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 17F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvDoanhThu.DefaultCellStyle.Font = new Font("Candara", 15);

            //Modify columns
            dgvDoanhThu.Columns[0].Width = 400;
            dgvDoanhThu.Columns[1].Width = 200;
            dgvDoanhThu.Columns[2].Width = 200;
            dgvDoanhThu.Columns[3].Width = 200;
            


            //Modify each row
            setRowHeight(dgvDonHang, 50);
        }
        private void lockChoices_DoanhThu(bool obj1, bool obj2, bool obj3, bool obj4)
        {
            txtFrom_DoanhThu.Enabled = obj1;
            txtTo_DoanhThu.Enabled = obj2;
            dtpThoiGian_DoanhThu.Enabled = obj3;
            dtpTo_DoanhThu.Enabled = obj4;
           ;

            txtFrom_DoanhThu.BackColor = !obj1 ? Color.Silver : Color.White;
            txtTo_DoanhThu.BackColor = !obj2 ? Color.Silver : Color.White;
            dtpThoiGian_DoanhThu.BackColor = !obj3 ? Color.Silver : Color.White;
            dtpTo_DoanhThu.BackColor = !obj4 ? Color.Silver : Color.White;
            
        }

        private void groupBox28_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DoanhThu(true, true, false, false);
        }

        private void btnThem_DoanhThu_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_DoanhThu_Click(object sender, EventArgs e)
        {
            ClearRadioButtons(tabControl.SelectedTab);
            ClearTextBoxes(tabControl.SelectedTab);
            lockChoices_MaGiamGia(true, true, true);
            txtTimTatCa_MaGiamGia.Text = "Nhập mã giảm giá vào đây ";
        }

        private void btnTimThongThuong_DoanhThu_Click(object sender, EventArgs e)
        {
            dgvDoanhThu.DataBindings.Clear();
            var listDoanhThu = new List<DoanhThu>();
            string ex = "";

            int from =txtFrom_DoanhThu.Text!="" ?int.Parse( txtFrom_DoanhThu.Text):0;
            int to = txtTo_DoanhThu.Text != "" ? int.Parse(txtTo_DoanhThu.Text):0 ;
            DateTime start_date = dtpThoiGian_DoanhThu.Value;
            DateTime end_date = dtpTo_DoanhThu.Value;
            if (rbTongDoanhThu.Checked)
            {
                //Tìm tổng doanh thu
                listDoanhThu = doanhThuLogic.timDoanhThu(0, from, to, new DateTime(), new DateTime(), ref ex);
            }
            if (rbTongChi.Checked)
            {
                //Tìm theo số tiền giảm 
                listDoanhThu = doanhThuLogic.timDoanhThu(1, from, to, new DateTime(), new DateTime(), ref ex);
            }
            if (rbNgay_DoanhThu.Checked)
            {
                listDoanhThu = doanhThuLogic.timDoanhThu(2, 0, 0, start_date, end_date, ref ex);
            }
                dgvDoanhThu.DataSource = listDoanhThu;
            setRowHeight(dgvDoanhThu, 50);
        }

        private void txtMaDonHang_DonHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbTongDoanhThu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DoanhThu(true, true, false, false);
        }

        private void rbNgay_DoanhThu_CheckedChanged(object sender, EventArgs e)
        {
            lockChoices_DoanhThu(false, false, true, true);
        }

        private void btnReload_DoanhThu_Click(object sender, EventArgs e)
        {
            setDgvDoanhthu();
        }
    }
}
