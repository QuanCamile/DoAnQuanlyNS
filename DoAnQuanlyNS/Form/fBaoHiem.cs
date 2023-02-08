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
    public partial class fBaoHiem : Form
    {
        public fBaoHiem()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();
        private void fBaoHiem_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
        //load data
        private void LoadDGV()
        {
            try
            {
                List<BaoHiem> listBaoHiem = context.BaoHiems.ToList();
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                //List<NhanVien> listNhanVien = context.NhanViens.ToList();
                FillNhanVienCombobox(listNhanVien);
                BindGrid(listBaoHiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //đưa data vào combobox
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {
            this.cmbTenNV.DataSource = listNhanVien;
            this.cmbTenNV.DisplayMember = "TenNV";
            this.cmbTenNV.ValueMember = "MaNV";
        }
        //load data sql to datagridview
        private void BindGrid(List<BaoHiem> listBaoHiem)
        {
            dgvBaoHiem.Rows.Clear();
            foreach (var item in listBaoHiem)
            {
                int index = dgvBaoHiem.Rows.Add();
                dgvBaoHiem.Rows[index].Cells[0].Value = item.MaBaoHiem;
                dgvBaoHiem.Rows[index].Cells[1].Value = item.NhanVien.TenNV;
                dgvBaoHiem.Rows[index].Cells[2].Value = item.NgayHetHan;
                dgvBaoHiem.Rows[index].Cells[3].Value = item.LoaiBaoHiem;

            }
        }
        //click datagridview to textbox, combobox
        private void dgvBaoHiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvBaoHiem.Rows[ind];
            txtMaBH.Text = selectedRows.Cells[0].Value.ToString();
            cmbTenNV.Text = selectedRows.Cells[1].Value.ToString();            
            dateTimeHetHan.Text = selectedRows.Cells[2].Value.ToString();
            txtLoaiBH.Text = selectedRows.Cells[3].Value.ToString();
        }
        private int GetSelectedRow(string maBH, string tenNV)
        {
            for (int i = 0; i < dgvBaoHiem.Rows.Count - 1; i++)
            {
                if (dgvBaoHiem.Rows[i].Cells[0].Value.ToString() == maBH || dgvBaoHiem.Rows[i].Cells[1].Value.ToString() == tenNV)
                {
                    return i;
                }
            }
            return -1;
        }
        //set các giá trị mặc định
        private void refresh()
        {
            txtMaBH.Text = "";
            cmbTenNV.Text = "";
            dateTimeHetHan.Value = DateTime.Now;
            txtLoaiBH.Text = "";
        }
        //button add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBH.Text == "" || cmbTenNV.Text == "" || txtLoaiBH.Text =="")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin bảo hiểm!");             

                if (GetSelectedRow(txtMaBH.Text, cmbTenNV.Text) == -1)
                {
                    BaoHiem b = new BaoHiem()
                    {
                        MaBaoHiem = txtMaBH.Text,
                        MaNV = cmbTenNV.SelectedValue.ToString(),
                        NgayHetHan = dateTimeHetHan.Value,
                        LoaiBaoHiem = txtLoaiBH.Text                       
                    };                  
                    context.BaoHiems.Add(b);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mỗi nhân viên chỉ có 1 bảo hiểm!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //button update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBH.Text == "" || cmbTenNV.Text == "" || txtLoaiBH.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin bảo hiểm!");

                BaoHiem dbUpdate = context.BaoHiems.FirstOrDefault(p => p.MaBaoHiem == txtMaBH.Text);
                if (dbUpdate != null)
                {
                    //dbUpdate.NhanVien.TenNV = cmbTenNV.SelectedValue.ToString();
                    dbUpdate.NgayHetHan = dateTimeHetHan.Value;
                    dbUpdate.LoaiBaoHiem = txtLoaiBH.Text;                  
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy mã bảo hiểm cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //button xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                BaoHiem dbDelete = context.BaoHiems.FirstOrDefault(p => p.MaBaoHiem == txtMaBH.Text);
                if (dbDelete != null)
                {
                    DialogResult h = MessageBox.Show("Bạn có thực sự muốn xóa mã bảo hiểm này?", "Thông báo", MessageBoxButtons.YesNo);
                    if (h == DialogResult.Yes)
                    {
                        context.BaoHiems.Remove(dbDelete);
                        //List<NhanVien> listNhanVien = context.NhanViens.ToList();
                        //List<NhanVien> listDeleteNhanVien = listNhanVien.Where(p => p.MaPB == txtMPB.Text).ToList();
                        //foreach (var item in listDeleteNhanVien)
                        //{
                        //    context.NhanViens.Remove(item);
                        //}
                        //context.SaveChanges();
                        context.SaveChanges();
                        LoadDGV();
                        refresh();
                        MessageBox.Show("Xóa mã bảo hiểm thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tim thấy mã bảo hiểm cần xóa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }      
    }
}
