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
    
    public partial class formLichLamViec : Form
    {
        private int command;
        private string mallv;
        LichLamViecLogic lichLamViecLogic=new LichLamViecLogic();
        public formLichLamViec( int command, string mallv)
        {
            InitializeComponent();
            this.command=command;
            this.mallv = mallv;
            
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
            string ex = "";
            LichLamViec llv = new LichLamViec();
            llv.MaLLV=txtMaLichLamViec.Text;
            llv.CaLV =byte.Parse(cbCa.Text);
            llv.NgayLV =dtpNgayLamViec.Value;
            switch (command)
            {
                case 0:
                    //Thêm dữ liệu 
                    lichLamViecLogic.insert(llv, ref ex);
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
                    lichLamViecLogic.update(llv, ref ex);
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

        private void formLichLamViec_Load(object sender, EventArgs e)
        {
            string ex = "";
            if (command == 1)
            {
                var llv = lichLamViecLogic.find(this.mallv, ref ex);
                if (checkException(ex))
                {
                    MessageBox.Show(ex);
                    return;
                }
                txtMaLichLamViec.Text = llv.MaLLV;
                cbCa.Text = llv.CaLV.ToString();
                dtpNgayLamViec.Value = llv.NgayLV;

            }
        }

        private void dtpNgayLamViec_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
