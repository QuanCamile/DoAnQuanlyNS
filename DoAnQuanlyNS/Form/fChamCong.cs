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
    public partial class fChamCong : Form
    {
        public fChamCong()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();      

        //loadform
        private void fChamCong_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }

        //loadform
        private void LoadDGV()
        {
            try
            {
                List<ChamCong> listChamCong = context.ChamCongs.ToList();
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                FillNhanVienCombobox(listNhanVien);
                BindGrid(listChamCong);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //hiển thị data ra combobox
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {
            this.cmbTenNV.DataSource = listNhanVien;
            this.cmbTenNV.DisplayMember = "TenNV";
            this.cmbTenNV.ValueMember = "MaNV";
        }

        //hiển thị data từ sql ra datagridview
        private void BindGrid(List<ChamCong> listChamCong)
        {
            dgvChamCong.Rows.Clear();
            foreach (var item in listChamCong)
            {
                int index = dgvChamCong.Rows.Add();
                dgvChamCong.Rows[index].Cells[0].Value = item.MaChamCong;
                dgvChamCong.Rows[index].Cells[1].Value = item.NhanVien.TenNV;
                dgvChamCong.Rows[index].Cells[2].Value = item.TinhTrang;
                dgvChamCong.Rows[index].Cells[3].Value = item.SoNgayLam;
            }
        }

        //click datagridview to textbox,combobox
        private void dgvChamCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvChamCong.Rows[ind];
            txtMaCC.Text = selectedRows.Cells[0].Value.ToString();
            cmbTenNV.Text = selectedRows.Cells[1].Value.ToString();
            cmbTinhTrang.Text = selectedRows.Cells[2].Value.ToString();
            txtSoNgayLam.Text = selectedRows.Cells[3].Value.ToString();
        }
        
        //thiết lập các giá trị mặc định
        private void refresh()
        {
            txtMaCC.Text = "";
            cmbTenNV.Text = "";
            cmbTinhTrang.Text = "";
            txtSoNgayLam.Text = "";
        }

        //button update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaCC.Text == "" || cmbTenNV.Text == "" || cmbTinhTrang.Text == "" || txtSoNgayLam.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin cần chấm công!");
                if (int.Parse(txtSoNgayLam.Text) <= 0)
                    throw new Exception("Số ngày làm phải lớn hơn 0!");

                ChamCong dbUpdate = context.ChamCongs.FirstOrDefault(p => p.MaChamCong == txtMaCC.Text);
                if (dbUpdate != null)
                {                  
                    dbUpdate.TinhTrang = cmbTinhTrang.Text;
                    dbUpdate.SoNgayLam = int.Parse(txtSoNgayLam.Text);               
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy mã chấm công cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //button xoa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ChamCong dbDelete = context.ChamCongs.FirstOrDefault(p => p.MaChamCong == txtMaCC.Text);
                if (dbDelete != null)
                {
                    DialogResult h = MessageBox.Show("Bạn có thực sự muốn xóa mã chấm công này?", "Thông báo", MessageBoxButtons.YesNo);
                    if (h == DialogResult.Yes)
                    {
                        context.ChamCongs.Remove(dbDelete);
                        List<Luong> listLuong = context.Luongs.ToList();
                        List<Luong> listDeleteLuong = listLuong.Where(p => p.MaChamCong == (txtMaCC.Text)).ToList();
                        foreach (var item in listDeleteLuong)
                        {
                            context.Luongs.Remove(item);
                        }
                        context.SaveChanges();
                        context.SaveChanges();
                        LoadDGV();
                        refresh();
                        MessageBox.Show("Xóa mã chấm công thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tim thấy mã chấm công cần xóa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
      
        private int GetSelectedRow(string maCC, string tenNV)
        {
            for (int i = 0; i < dgvChamCong.Rows.Count - 1; i++)
            {
                if (dgvChamCong.Rows[i].Cells[0].Value.ToString() == maCC || dgvChamCong.Rows[i].Cells[1].Value.ToString() == tenNV)
                {
                    return i;
                }
            }
            return -1;
        }
        //button add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaCC.Text == "" || cmbTenNV.Text == "" || cmbTinhTrang.Text == "" || txtSoNgayLam.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin chấm công!");

                if (GetSelectedRow(txtMaCC.Text, cmbTenNV.Text) == -1)
                {
                    ChamCong c = new ChamCong()
                    {
                        MaChamCong = txtMaCC.Text,
                        MaNV = cmbTenNV.SelectedValue.ToString(),
                        TinhTrang = cmbTinhTrang.Text,
                        SoNgayLam = int.Parse(txtSoNgayLam.Text)
                       
                    };
                    context.ChamCongs.Add(c);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mỗi nhân viên chỉ có 1 bảng chấm công!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            LoadDGV();
        }
    }
}
