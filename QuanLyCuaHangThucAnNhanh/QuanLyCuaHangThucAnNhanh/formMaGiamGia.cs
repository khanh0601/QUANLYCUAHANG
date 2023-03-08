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
    public partial class formMaGiamGia : Form
    {
        private int command;
        private String maGiamGia;
        MaGiamGiaLogic maGiamGiaLogic = new MaGiamGiaLogic();
        public formMaGiamGia(int command, string maGiamGia)
        {
            this.command = command;
            this.maGiamGia = maGiamGia;
            InitializeComponent();
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
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            //Khởi tạo biến chứa Model và biến kiểm tra exception
            MaGiamGia mgg = new MaGiamGia();
            string ex = "";

            //Lấy dữ liệu từ các textbox
            mgg.MaGG = txtMaGiamGia.Text;
            mgg.SoTienGiam = int.Parse(txtSoTienGiam.Text);
            mgg.Soluong = int.Parse(txtSoLuong.Text);

            switch (command)
            {
                case 0:
                    //Thêm dữ liệu
                    maGiamGiaLogic.insert(mgg, ref ex);

                    if (checkException(ex))
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công!");
                        return;
                    }

                    MessageBox.Show("Đã thêm dữ liệu thành công!", "Sửa dữ liệu thành công",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case 1:
                    // Sửa dữ liệu 
                    maGiamGiaLogic.update(mgg, ref ex);
                    if (!checkException(ex))
                    {
                        MessageBox.Show("Sửa dữ liệu không thành công!");
                    }
                    MessageBox.Show("Đã sửa dữ liệu thành công!", "Sửa dữ liệu thành công", MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    break;


            }
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
    }
}
