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
    public partial class fThuongPhat : Form
    {
        public fThuongPhat()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();

        //hàm load
        private void fThuongPhat_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
        
        //viết hàm riêng để tiện cho việc tái sử dụng
        public void LoadDGV()
        {
            try
            {
                List<ThuongPhat> listThuongPhat = context.ThuongPhats.ToList();
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                //List<NhanVien> listNhanVien = context.NhanViens.ToList();
                FillNhanVienCombobox(listNhanVien);
                BindGrid(listThuongPhat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        //hiện data từ sql ra combobox
        public void FillNhanVienCombobox(List<NhanVien> listNhanVien)
        {
            this.cmbTenNV.DataSource = listNhanVien;
            this.cmbTenNV.DisplayMember = "TenNV";
            this.cmbTenNV.ValueMember = "MaNV";
        }
        
        //hiển thị data từ sql ra datagridview
        private void BindGrid(List<ThuongPhat> listThuongPhat)
        {
            dgvThuongPhat.Rows.Clear();
            foreach (var item in listThuongPhat)
            {
                int index = dgvThuongPhat.Rows.Add();
                dgvThuongPhat.Rows[index].Cells[0].Value = item.MaQuyetDinh;
                dgvThuongPhat.Rows[index].Cells[1].Value = item.NhanVien.TenNV;
                dgvThuongPhat.Rows[index].Cells[2].Value = item.SoTienThuong;             
                dgvThuongPhat.Rows[index].Cells[3].Value = item.SoTienPhat;
                dgvThuongPhat.Rows[index].Cells[4].Value = item.LiDo;

            }
        }

        //click datagridview hiển thị data ra textbox, combobox
        private void dgvThuongPhat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvThuongPhat.Rows[ind];
            txtMaQD.Text = selectedRows.Cells[0].Value.ToString();
            cmbTenNV.Text = selectedRows.Cells[1].Value.ToString();
            txtSoTienThuong.Text = selectedRows.Cells[2].Value.ToString();  
            txtSoTienPhat.Text = selectedRows.Cells[3].Value.ToString();
            txtLiDo.Text = selectedRows.Cells[4].Value.ToString();           
        }
        private int GetSelectedRow(string thuongPhatID)
        {
            for (int i = 0; i < dgvThuongPhat.Rows.Count - 1; i++)
            {
                if (dgvThuongPhat.Rows[i].Cells[0].Value.ToString() == thuongPhatID)
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
                if (txtMaQD.Text == "" || cmbTenNV.Text == "" || txtSoTienThuong.Text == "" || txtSoTienPhat.Text == "" || txtLiDo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin thưởng phạt");
                if (int.Parse(txtSoTienThuong.Text) < 0 || int.Parse(txtSoTienPhat.Text) < 0)
                    throw new Exception("Số tiền phải lớn hơn 0!");           

                if (GetSelectedRow(txtMaQD.Text) == -1)
                {
                    ThuongPhat t = new ThuongPhat()
                    {
                        MaQuyetDinh = txtMaQD.Text,
                        MaNV = cmbTenNV.SelectedValue.ToString(),
                        SoTienThuong = decimal.Parse(txtSoTienThuong.Text),
                        SoTienPhat = decimal.Parse(txtSoTienPhat.Text),
                        LiDo = txtLiDo.Text                      
                    };                  
                    context.ThuongPhats.Add(t);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mã quyết định khen thưởng đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //hàm làm mới
        private void refresh()
        {
            txtMaQD.Text = "";
            cmbTenNV.Text = "";
            txtSoTienThuong.Text = "0";
            txtSoTienPhat.Text = "0";
            txtLiDo.Text = "";
        }        

        //button save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaQD.Text == "" || cmbTenNV.Text == "" || txtSoTienThuong.Text == "" || txtSoTienPhat.Text == "" || txtLiDo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên");
                if (int.Parse(txtSoTienThuong.Text) < 0 || int.Parse(txtSoTienPhat.Text) < 0)
                    throw new Exception("Số tiền phải lớn hơn 0!");

                ThuongPhat dbUpdate = context.ThuongPhats.FirstOrDefault(p => p.MaQuyetDinh == txtMaQD.Text);
                if (dbUpdate != null)
                {
                    //dbUpdate.NhanVien.TenNV = cmbTenNV.SelectedValue.ToString();
                    dbUpdate.SoTienThuong = decimal.Parse(txtSoTienThuong.Text);
                    dbUpdate.SoTienPhat = decimal.Parse(txtSoTienPhat.Text);
                    dbUpdate.LiDo = txtLiDo.Text;              
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy mã quyết định khen thưởng cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }    
    }
}
