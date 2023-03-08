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
    public partial class formDonHang : Form
    {
        public string maDH { get; set; }

        public formDonHang(string maDH)
        {
            InitializeComponent();
            this.maDH = maDH;
        }

        string ex = "";

        ChiTietDonHang ctdh = new ChiTietDonHang();
        DonHangMua dh = new DonHangMua();

        DonHangLogic donHangLogic = new DonHangLogic();
        ChiTietDonHangLogic chiTietDonHangLogic = new ChiTietDonHangLogic();

        private void formDonHang_Load(object sender, EventArgs e)
        {
            ex = "";
            //Tìm đơn hàng theo mã đơn hàng
            dh = donHangLogic.find(this.maDH, ref ex);

            //Gắn thông tin của đơn hàng lên form
            if(dh != null)
            {
                txtMaDonHang.Text = this.maDH;
                dtgNgay.Value = dh.NgayTao.Value;
                txtMaGiamGia.Text = dh.MaGG;
                txtTongTien.Text = dh.ThanhTien.ToString();
                lblNhanVien.Text = dh.MaNV;
            }

            //Tìm các Chi tiết đơn hàng và gắn lên dgv
            setDgvCTDH();
            updateDgvCTDH();
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
    }
}
