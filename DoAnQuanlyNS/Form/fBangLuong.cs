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
    public partial class fBangLuong : Form
    {
        public fBangLuong()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();
        private void test_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
        private void LoadDGV()
        {
            try
            {
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                List<Luong> listLuong = context.Luongs.ToList();
                List<ChamCong> listChamCong = context.ChamCongs.ToList();
                List<ThuongPhat> listThuongPhat = context.ThuongPhats.ToList();

                FillNhanVienCombobox(listNhanVien);
                FillChamCongCombobox(listChamCong);
                FillThuongPhatCombobox(listThuongPhat);
                BindGrid(listLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {
            this.cmbTenNV.DataSource = listNhanVien;
            this.cmbTenNV.DisplayMember = "TenNV";
            this.cmbTenNV.ValueMember = "MaNV";
        }
        public void FillChamCongCombobox(List<ChamCong> listChamCong)
        {
            this.cmbMaCC.DataSource = listChamCong;
            this.cmbMaCC.DisplayMember = "MaChamCong";
            this.cmbMaCC.ValueMember = "MaChamCong";
        }
        public void FillThuongPhatCombobox(List<ThuongPhat> listThuongPhat)
        {
            this.cmbMaQD.DataSource = listThuongPhat;
            this.cmbMaQD.DisplayMember = "MaQuyetDinh";
            this.cmbMaQD.ValueMember = "MaQuyetDinh";
        }
        private void BindGrid(List<Luong> listLuong)
        {
            dgvLuong.Rows.Clear();
            foreach (var item in listLuong)
            {
                int index = dgvLuong.Rows.Add();
                dgvLuong.Rows[index].Cells[0].Value = item.MaLuong;
                dgvLuong.Rows[index].Cells[1].Value = item.ChamCong.MaChamCong;
                dgvLuong.Rows[index].Cells[2].Value = item.ThuongPhat.MaQuyetDinh;
                dgvLuong.Rows[index].Cells[3].Value = item.NhanVien.TenNV;
                dgvLuong.Rows[index].Cells[4].Value = item.HeSoLuong;
                dgvLuong.Rows[index].Cells[5].Value = item.ChamCong.SoNgayLam;
                dgvLuong.Rows[index].Cells[6].Value = item.NhanVien.ChucVu.PhuCap;
                dgvLuong.Rows[index].Cells[7].Value = item.LuongCB;
            }
        }

        private void dgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvLuong.Rows[ind];
            txtMaLuong.Text = selectedRows.Cells[0].Value.ToString();
            cmbMaCC.Text = selectedRows.Cells[1].Value.ToString();
            cmbMaQD.Text = selectedRows.Cells[2].Value.ToString();
            cmbTenNV.Text = selectedRows.Cells[3].Value.ToString();
            txtHeSoLuong.Text = selectedRows.Cells[4].Value.ToString();
            
            
            txtLuongCB.Text = selectedRows.Cells[7].Value.ToString();
        }
        private int GetSelectedRow(string maLuong, string maCC, string maQD, string maNV)
        {
            for (int i = 0; i < dgvLuong.Rows.Count - 1; i++)
            {
                if (dgvLuong.Rows[i].Cells[0].Value.ToString() == maLuong || dgvLuong.Rows[i].Cells[1].Value.ToString() == maCC
                        || dgvLuong.Rows[i].Cells[2].Value.ToString() == maQD || dgvLuong.Rows[i].Cells[3].Value.ToString() == maNV)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtMaBH.Text == "" || cmbTenNV.Text == "" || txtLoaiBH.Text == "")
                    //throw new Exception("Vui lòng nhập đầy đủ thông tin bảo hiểm!");

                if (GetSelectedRow(txtMaLuong.Text, cmbMaCC.Text, cmbMaQD.Text, cmbTenNV.Text) == -1)
                {
                    Luong b = new Luong()
                    {
                        MaLuong = txtMaLuong.Text,                      
                        MaNV = cmbTenNV.SelectedValue.ToString(),
                        MaChamCong = cmbMaCC.SelectedValue.ToString(),
                        MaQuyetDinh = cmbMaQD.SelectedValue.ToString(),
                        HeSoLuong = double.Parse(txtHeSoLuong.Text),
                        LuongCB = decimal.Parse(txtLuongCB.Text)
                     
                    };
                    context.Luongs.Add(b);
                    context.SaveChanges();
                    LoadDGV();
                    //refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra các thông tin các thông tin mã không được trùng!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtHeSoLuong.Text == "" || txtLuongCB.Text == "")
                    //throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (double.Parse(txtHeSoLuong.Text) <= 0 || decimal.Parse(txtLuongCB.Text) <= 0)
                    throw new Exception("Hệ số lương hoặc lương CB phải lớn hơn 0!");

                //var dbUpdate = context.Luongs.FirstOrDefault(p => p.MaNV == txtMaNV.Text);
                Luong dbUpdate = context.Luongs.FirstOrDefault(p => p.MaLuong == txtMaLuong.Text);

                if (dbUpdate != null)
                {
                    dbUpdate.HeSoLuong = double.Parse(txtHeSoLuong.Text);
                    dbUpdate.LuongCB = decimal.Parse(txtLuongCB.Text);
                    context.SaveChanges();
                    //context.Entry(dbUpdate).State = System.Data.Entity.EntityState.Deleted;
                    LoadDGV();
                    //refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy thông tin nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
