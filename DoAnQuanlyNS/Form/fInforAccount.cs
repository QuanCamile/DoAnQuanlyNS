using DoAnQuanlyNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQuanlyNS
{
    public partial class fInforAccount : Form
    {
        public fInforAccount()
        {
            InitializeComponent();
        }
        
        StaffContextDB context = new StaffContextDB();        
        
        //button delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Userr dbDelete = context.Userrs.FirstOrDefault(p => p.MaNV == cmbMNV.Text && p.UserName == txtUserName.Text);
            if (dbDelete != null)
            {
                DialogResult h = MessageBox.Show("Bạn có thực sự muốn xóa tài khoản nhân viên này?", "Thông báo", MessageBoxButtons.YesNo);
                if (h == DialogResult.Yes)
                {
                    context.Userrs.Remove(dbDelete);
                    context.SaveChanges();
                    LoadUser();
                    Refesh();
                    MessageBox.Show("Xóa tài khoản viên thành công!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Không tim thấy tài khoản cần xóa!", "Thông báo", MessageBoxButtons.OK);
            }
        }        

        //hàm load
        private void fInforAccount_Load(object sender, EventArgs e)
        {
            LoadUser();
        }
        
        //viết hàm load riêng để tái sử dụng
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
      
        //lấy dữ liệu từ sql lên combobox
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {           
            this.cmbMNV.DataSource = listNhanVien;
            this.cmbMNV.DisplayMember = "MaNV";
            this.cmbMNV.ValueMember = "MaNV";
        }

        //lấy dữ liệu từ sql lên datagridview
        private void BindGrid(List<Userr> listUser)
        {
            dgvAccount.Rows.Clear();
            foreach (var item in listUser)
            {
                int index = dgvAccount.Rows.Add();
                dgvAccount.Rows[index].Cells[0].Value = item.MaNV;
                dgvAccount.Rows[index].Cells[1].Value = item.UserName;
                dgvAccount.Rows[index].Cells[2].Value = item.PassWord;
                dgvAccount.Rows[index].Cells[3].Value = item.IDPer;              
            }
        }

        //click datagridview dữ liệu hiện lên textbox, combobox
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvAccount.Rows[ind];
            cmbMNV.Text = selectedRows.Cells[0].Value.ToString();
            txtUserName.Text = selectedRows.Cells[1].Value.ToString();
            txtPassWord.Text = selectedRows.Cells[2].Value.ToString();
            txtTypeAcc.Text = selectedRows.Cells[3].Value.ToString();
        }

        //hàm tạo mới
        private void Refesh()
        {
            cmbMNV.Text = "";
            txtUserName.Text = "";
            txtPassWord.Text = "";
            //txtNewPass.Text = "";
            txtTypeAcc.Text = "";
        }

        private int GetSelectedRow(string nhanVienID, string userName)
        {
            for (int i = 0; i < dgvAccount.Rows.Count - 1; i++)
            {
                if (dgvAccount.Rows[i].Cells[0].Value.ToString() == nhanVienID || dgvAccount.Rows[i].Cells[1].Value.ToString() == userName)
                {
                    return i;
                }
            }
            return -1;
        }            

        //button add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMNV.Text == "" || txtUserName.Text == "" || txtPassWord.Text == "" || txtTypeAcc.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin tài khoản muốn thêm!");
                if (int.Parse(txtTypeAcc.Text) != 1 && int.Parse(txtTypeAcc.Text) != 0)
                    throw new Exception("Loại tài khoản chỉ nhập 1 hoặc 0");
                int getSelectedRow = GetSelectedRow(cmbMNV.Text, txtUserName.Text);
                if (GetSelectedRow(cmbMNV.Text, txtUserName.Text) == -1)
                {
                    List<Userr> listUser = context.Userrs.ToList();
                    Userr u = new Userr()
                    {
                        MaNV = cmbMNV.SelectedValue.ToString(),
                        UserName = txtUserName.Text,
                        PassWord = txtPassWord.Text,
                        IDPer = int.Parse(txtTypeAcc.Text)
                    };
                    context.Userrs.Add(u);
                    context.SaveChanges();
                    LoadUser();
                    Refesh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {                  
                    MessageBox.Show("Trùng mã NV hoặc trùng UserName!", "Thông báo", MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //button tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNV = cmbMNV.Text.Trim();
            string userName = txtUserName.Text.Trim();
            //string passWord = txtPassWord.Text.Trim();
            //int typeAcc = int.Parse(txtTypeAcc.Text.Trim());
            List<Userr> listUser = context.Userrs.ToList();
            List<Userr> listSearch = listUser.Where(p => p.MaNV == maNV && p.UserName.ToLower().Contains(userName.ToLower())                                              
                                                       ).ToList();
            if (listSearch.Count == 0)
            {
                MessageBox.Show("Không tìm thấy account của nhân viên!", "Thông báo", MessageBoxButtons.OK);
            }
            else
                BindGrid(listSearch);
        }

        //hàm click vào panel load lại dữ liệu
        private void panel1_Click(object sender, EventArgs e)
        {
            LoadUser();
        }
    }
}
