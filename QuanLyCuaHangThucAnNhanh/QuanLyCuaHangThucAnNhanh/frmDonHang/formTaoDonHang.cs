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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyCuaHangThucAnNhanh
{
    public partial class formTaoDonHang : Form
    {
        SanPhamLogic sanPhamLogic = new SanPhamLogic();
        DonHangLogic donHangLogic = new DonHangLogic();
        ChiTietDonHangLogic chiTietDonHangLogic = new ChiTietDonHangLogic();    
        MaGiamGiaLogic maGiamGiaLogic = new MaGiamGiaLogic();

        SanPham sp = new SanPham();
        MaGiamGia mgg = new MaGiamGia();
        ChiTietDonHang ctdh = new ChiTietDonHang();
        DonHangMua dh = new DonHangMua();       

        bool addedDonHang = false;


        public formTaoDonHang()
        {
            InitializeComponent();
        }

        private void setRowHeight(DataGridView dgv, int h)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Height = h;
            }
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

        private void formTaoDonHang_Load(object sender, EventArgs e)
        {
            foreach(var sp in sanPhamLogic.SanPhamList())
            {
                cbSanPham.Items.Add(sp.TenSP);    
            }

            foreach(var magiamgia in maGiamGiaLogic.MaGiamGiaList())
            {
                cbKhuyenMai.Items.Add(magiamgia.MaGG);   
            }

            txtGiaBan.Text = "0";
            txtSoLuong.Text = "0";
            txtTongGiaTri.Text = "0";
            setDgvCTDH();
        }

        private void updateDgvCTDH()
        {
            //Update dgv data
            var listCTDH = chiTietDonHangLogic.findByMaDH(txtMaDonHang.Text.Trim());
            dgvDanhSachSanPham.DataSource = listCTDH;
            //Modify each row
            setRowHeight(dgvDanhSachSanPham, 30);
        }

        private void setDgvCTDH()
        {
            dgvDanhSachSanPham.AutoGenerateColumns = false;

            //Modify header
            dgvDanhSachSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.LemonChiffon;
            dgvDanhSachSanPham.EnableHeadersVisualStyles = false;

            foreach (DataGridViewColumn col in dgvDanhSachSanPham.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //Modify cell style
            dgvDanhSachSanPham.DefaultCellStyle.Font = new Font("Candara", 14);

            //Modify columns
            dgvDanhSachSanPham.Columns[0].Width = 160;
            dgvDanhSachSanPham.Columns[1].Width = 160;
            dgvDanhSachSanPham.Columns[2].Width = 175;
            dgvDanhSachSanPham.Columns[3].Width = 153;

            //Modify each row
            setRowHeight(dgvDanhSachSanPham, 30);
        }

        private void cbSanPham_TextChanged(object sender, EventArgs e)
        {
            sp = sanPhamLogic.findByName(cbSanPham.Text.Trim());
            if (sp != null)
            {
                txtGiaBan.Text = sp.GiaBan.ToString();
            }

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "")
                txtSoLuong.Text = "0";
            //sp = sanPhamLogic.findByName(cbSanPham.Text.Trim());
            if (sp != null)
            {
                txtTongGiaTri.Text = (int.Parse(txtGiaBan.Text)*int.Parse(txtSoLuong.Text)).ToString();
            }
        }
        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaBan.Text == "")
                txtGiaBan.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ex = "";

            //Tìm sản phẩm
            sp = sanPhamLogic.findByName(cbSanPham.Text);

            //Set dữ liệu cho Chi tiết đơn hàng
            ctdh = new ChiTietDonHang();
            ctdh.MaSP = sp.MaSP;
            ctdh.MaDH = txtMaDonHang.Text;
            ctdh.SoLuong = int.Parse(txtSoLuong.Text);
            ctdh.TongGiaTri = int.Parse(txtTongGiaTri.Text);

            //Nếu chưa thêm Đơn hàng vào csdl, hay đây là Chi tiết đơn hàng đầu tiên thì tạo đơn hàng
            if (!addedDonHang)
            {
                //Đánh dấu đã thêm Chi tiêt đơn hàng đầu tiên
                addedDonHang = true;

                //Set dữ liệu cho đơn hàng
                dh = new DonHangMua();
                dh.MaDH = txtMaDonHang.Text;
                dh.NgayTao = dtgNgay.Value;

                donHangLogic.insert(dh, ref ex);
                if (checkException(ex))
                {
                    MessageBox.Show("Thêm đơn hàng không thành công!");
                    return;
                }

            }

            chiTietDonHangLogic.insert(ctdh, ref ex);
            if (checkException(ex))
            {
                MessageBox.Show("Thêm sản phẩm vào đơn hàng không thành công!");
                return;
            }

            //Cập nhật dgv
            updateDgvCTDH();

            //Xoá các dữ liệu của sản phẩm đã nhập
            txtTongGiaTri.Text = "0";
            txtSoLuong.Text = "0";
            txtGiaBan.Text = "0";
            cbSanPham.ResetText();
        }

        private void btnTinhTongTien_Click(object sender, EventArgs e)
        {
            string ex = "";
            int tongTien = 0;

            var listCTDH = chiTietDonHangLogic.findByMaDH(txtMaDonHang.Text);

            var mgg = maGiamGiaLogic.find(cbKhuyenMai.Text, ref ex);
            if (checkException(ex))
            {
                MessageBox.Show("Áp dụng mã giảm giá không thành công!");
                return;
            }

            foreach(var ctdh in listCTDH)
            {
                tongTien += ctdh.TongGiaTri.Value;
            }

            txtTongTien.Text = (tongTien - mgg.SoTienGiam).ToString() + " vnd";
            txtTongTien.SelectAll(); 
            txtTongTien.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            string ex = "";

            dh = donHangLogic.find(txtMaDonHang.Text, ref ex);
            if (checkException(ex))
            {
                MessageBox.Show("Áp dụng mã giảm giá không thành công!");
                return;
            }

            //Cập nhật Mã giảm giá, Mã nhân viên và Tổng tiền 
            dh.MaGG = cbKhuyenMai.Text.Trim();
            dh.ThanhTien = int.Parse(txtTongTien.Text.Substring(0,txtTongTien.Text.IndexOf("vnd")).Trim());
/*            dh.MaNV = lblNhanVien.Text;
*/
            //Cập nhật đơn hàng trên database
            donHangLogic.update(dh, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Cập nhật đơn hàng không thành công!");
                return;
            }
        }

        //Event chọn sản phẩm
        private void dgvDanhSachSanPham_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            // For any other operation except, StateChanged, do nothing
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            // Calculate amount code goes
            var maDonHang = txtMaDonHang.Text;
            var maSanPham = "";

            //Lấy mã sản phẩm từ dòng đã chọn
            foreach (DataGridViewRow row in dgvDanhSachSanPham.SelectedRows)
            {
                maSanPham = row.Cells[1].Value.ToString();
            }

            //Lấy dữ liệu Chi tiết đơn hàng và Sản phẩm từ CSDL
            ctdh = chiTietDonHangLogic.findOne(maDonHang, maSanPham);
            sp = sanPhamLogic.findById(maSanPham);

            //Trường hợp cập nhật dữ liêu tiếp tục gọi event này thì không xử lí
            if (sp == null) return;

            //Gắn dữ liệu của sản phẩm đã chọn lên txt và cb
            cbSanPham.Text = sp.TenSP.Trim();
            txtGiaBan.Text = sp.GiaBan.ToString();
            txtSoLuong.Text = ctdh.SoLuong.ToString();
            txtTongGiaTri.Text = ctdh.TongGiaTri.ToString();

            ////Không cho người dùng thay đổi sản phẩm
            //cbSanPham.Enabled = false;
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ex = "";

            //Tìm sản phẩm và Chi tiết đơn hàng
            sp = sanPhamLogic.findByName(cbSanPham.Text.Trim());
            ctdh = chiTietDonHangLogic.findOne(txtMaDonHang.Text, sp.MaSP);

            //Cập nhật thông tin của Chi tiết đơn hàng
            ctdh.MaSP = sp.MaSP;
            ctdh.TongGiaTri = int.Parse(txtTongGiaTri.Text);
            ctdh.SoLuong = int.Parse(txtSoLuong.Text);

            //Cập nhật chi tiết đơn hàng trên csdl
            chiTietDonHangLogic.update(ctdh, ref ex);

            if (checkException(ex))
            {
                MessageBox.Show("Cập nhật chi tiết đơn hàng không thành công!");
                return;
            }

            //Cập nhật dgv
            updateDgvCTDH();

            //Xoá các dữ liệu của sản phẩm đã nhập
            txtTongGiaTri.Text = "0";
            txtSoLuong.Text = "0";
            txtGiaBan.Text = "0";
            cbSanPham.ResetText();
        }

    }
}
