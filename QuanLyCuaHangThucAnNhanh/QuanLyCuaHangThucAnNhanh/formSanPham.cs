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

namespace QuanLyCuaHangThucAnNhanh
{
	public partial class formSanPham : Form
	{
		private int command;
		private string masp;
		SanPhamLogic sanPhamLogic=new SanPhamLogic();
		private bool checkException( string ex)
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

        public formSanPham(int command , string masp)
		{
			InitializeComponent();
			this.command = command;
			this.masp = masp;
		}

		private void chkDaXoa_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void btnLuuLai_Click(object sender, EventArgs e)
		{
			SanPham sp = new SanPham();
			string ex = "";
			//Lấy dữ liệu từ các textbox
			sp.MaSP=txtMaSanPham.Text;	
			sp.TenSP=txtTenSanPham.Text;
			sp.ChiPhi =int.Parse( txtChiPhi.Text);
			sp.GiaBan = int.Parse(txtGiaBan.Text);
			sp.SoLuongTon=int.Parse(txtSoLuongTon.Text);
			sp.SoLuongDaBan = int.Parse(txtSoLuongBan.Text);
			sp.DonViTinh = cbDonVi.Text;
			sp.MoTa = rtbMoTa.Text;
			switch (command)
			{
				case 0:
					//thêm dữ liệu 
					sanPhamLogic.insert(sp, ref ex);
					if (checkException(ex))
					{
						MessageBox.Show("Thêm dữ liệu không thành công!");
						return;
					}
					MessageBox.Show("Thêm dữ liệu thành công !","sửa dữ liệu thành công",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
				case 1:
					// Sửa dữ liệu 
					sanPhamLogic.update(sp, ref ex);
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

		private void textBox4_TextChanged(object sender, EventArgs e)
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
			rtbMoTa.ResetText();
			cbDonVi.ResetText();
		    TimeUpdate.ResetText();	
        }

		private void formSanPham_Load(object sender, EventArgs e)
		{
			string ex = "";
			//Load dữ liệu từ sản phẩm đã chọn 
			if (command == 1)
			{
				var sp = sanPhamLogic.find(this.masp,ref ex);
                if (checkException(ex))
                {
                    MessageBox.Show(ex);
                    return;
                }
				txtMaSanPham.Text = sp.MaSP;
				txtTenSanPham.Text = sp.TenSP;
				txtChiPhi.Text = sp.ChiPhi.ToString();
				txtGiaBan.Text = sp.GiaBan.ToString();
				txtSoLuongTon.Text=sp.SoLuongTon.ToString();
				txtSoLuongBan.Text = sp.SoLuongDaBan.ToString();
				cbDonVi.Text = sp.DonViTinh;
				TimeUpdate.Text=sp.NgayCapNhat.ToString();
				rtbMoTa.Text = sp.MoTa;

            }
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
