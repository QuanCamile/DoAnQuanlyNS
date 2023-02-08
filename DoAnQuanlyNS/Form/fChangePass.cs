using DoAnQuanlyNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQuanlyNS
{
    public partial class fChangePass : Form
    {
        public fChangePass()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();     
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMNV.Text == "" || txtUserName.Text == "" || txtPassWord.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin tài khoản muốn sửa!");
                if (txtPassWord.Text == txtNewPass.Text)
                    throw new Exception("Mật khẩu mới phải khác mật khẩu cũ!");
                Userr dbUpdate = context.Userrs.FirstOrDefault(p => p.UserName == txtUserName.Text && p.PassWord == txtPassWord.Text);
                if (dbUpdate != null)
                {
                    dbUpdate.PassWord = txtNewPass.Text;
                    //dbUpdate.IDPer = int.Parse(txtTypeAcc.Text);
                    context.SaveChanges();
                    LoadUser();
                    Refesh();
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fChangePass_Load(object sender, EventArgs e)
        {
            LoadUser();
        }
        private void LoadUser()
        {
            try
            {
                List<Userr> listUser = context.Userrs.ToList();
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                FillNhanVienCombobox(listNhanVien);
                BindGrid(listUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<Userr> listUser)
        {
            dgvChangePass.Rows.Clear();
            foreach (var item in listUser)
            {
                int index = dgvChangePass.Rows.Add();
                dgvChangePass.Rows[index].Cells[0].Value = item.MaNV;
                dgvChangePass.Rows[index].Cells[1].Value = item.UserName;
                dgvChangePass.Rows[index].Cells[2].Value = item.PassWord;
                dgvChangePass.Rows[index].Cells[3].Value = item.IDPer;
            }
        }
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {
            this.cmbMNV.DataSource = listNhanVien;
            this.cmbMNV.DisplayMember = "MaNV";
            this.cmbMNV.ValueMember = "MaNV";
        }
        private void Refesh()
        {
            cmbMNV.Text = "";
            txtUserName.Text = "";
            txtPassWord.Text = "";
            txtNewPass.Text = "";
            txtTypeAcc.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Refesh();
        }

        private void dgvChangePass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvChangePass.Rows[ind];
            cmbMNV.Text = selectedRows.Cells[0].Value.ToString();
            txtUserName.Text = selectedRows.Cells[1].Value.ToString();
            txtPassWord.Text = selectedRows.Cells[2].Value.ToString();
            txtTypeAcc.Text = selectedRows.Cells[3].Value.ToString();
        }
    }
}
