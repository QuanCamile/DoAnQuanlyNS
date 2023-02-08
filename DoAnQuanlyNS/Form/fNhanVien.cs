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
    public partial class fNhanVien : Form
    {
        public fNhanVien()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();

        //hàm load
        private void fNhanVien_Load(object sender, EventArgs e)
        {
            
            loadDGV();
        }
        
        //viết hàm load riêng để tiện tái sử dụng
        public void loadDGV()
        {
            try
            {
                List<NhanVien> listNhanVien = context.NhanViens.ToList();
                List<PhongBan> listPhongBan = context.PhongBans.ToList();
                List<ChucVu> listChucVu = context.ChucVus.ToList();
                FillPhongBanCombobox(listPhongBan);
                FillChucVuCombobox(listChucVu);
                BindGrid(listNhanVien);
                dateTimeNgaySinh.CustomFormat = "dd/MM/yyyy";
                dateTimeNgaySinh.CustomFormat = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //gọi data từ sql lên datagridview
        private void BindGrid(List<NhanVien> listNhanVien)
        {
            dgvNhanVien.Rows.Clear();
            foreach (var item in listNhanVien)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = item.MaNV;
                dgvNhanVien.Rows[index].Cells[1].Value = item.TenNV;
                dgvNhanVien.Rows[index].Cells[2].Value = item.PhongBan.TenPB;
                dgvNhanVien.Rows[index].Cells[3].Value = item.ChucVu.TenChucVu;
                dgvNhanVien.Rows[index].Cells[4].Value = item.GioiTinh;
                dgvNhanVien.Rows[index].Cells[5].Value = item.NgaySinh;
                dgvNhanVien.Rows[index].Cells[6].Value = item.DiaChi;
                dgvNhanVien.Rows[index].Cells[7].Value = item.CMND;
                dgvNhanVien.Rows[index].Cells[8].Value = item.SDT;
                dgvNhanVien.Rows[index].Cells[9].Value = item.TrinhDo;
                dgvNhanVien.Rows[index].Cells[10].Value = item.Email;
                dgvNhanVien.Rows[index].Cells[11].Value = item.NgayVaoLam;

                //
                dgvNhanVien.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
        
        //hiển thị data ra combobox
        public void FillPhongBanCombobox(List<PhongBan> listPhongBan)
        {
            this.cmbPhongBan.DataSource = listPhongBan;
            this.cmbPhongBan.DisplayMember = "TenPB";
            this.cmbPhongBan.ValueMember = "MaPB";
        }

        //hiển thị data ra combobox
        public void FillChucVuCombobox(List<ChucVu> listChucVu)
        {
            this.cmbChucVu.DataSource = listChucVu;
            this.cmbChucVu.DisplayMember = "TenChucVu";
            this.cmbChucVu.ValueMember = "MaChucVu";
        }

        //khi click vào datagridview hiển thị data lên textbox, combobox
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvNhanVien.Rows[ind];
            txtMNV.Text = selectedRows.Cells[0].Value.ToString();
            txtTenNV.Text = selectedRows.Cells[1].Value.ToString();
            cmbPhongBan.Text = selectedRows.Cells[2].Value.ToString();
            cmbChucVu.Text = selectedRows.Cells[3].Value.ToString();
            string gt = dgvNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (gt.Trim() == "Nam")
            {
                radioNam.Checked = true;
            }
            else
            {
                radioNu.Checked = true;
            }
            dateTimeNgaySinh.Text = selectedRows.Cells[5].Value.ToString();
            txtDiaChi.Text = selectedRows.Cells[6].Value.ToString();
            txtCMND.Text = selectedRows.Cells[7].Value.ToString();
            txtSDT.Text = selectedRows.Cells[8].Value.ToString();
            txtTrinhDo.Text = selectedRows.Cells[9].Value.ToString();
            if (selectedRows.Cells[10].Value == null)
                txtEmail.Text = "";
            else
                txtEmail.Text = selectedRows.Cells[10].Value.ToString();
            dateTimeNVaoLam.Text = selectedRows.Cells[11].Value.ToString();
        }

        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvNhanVien.Rows.Count - 1; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }
        
        //hàm kiểm tra tên PB và chức vụ
        private int checkPBCV(string tenPB, string tenCV)
        {
            for (int i = 0; i < dgvNhanVien.Rows.Count - 1; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[2].Value.ToString() == tenPB && dgvNhanVien.Rows[i].Cells[3].Value.ToString() == tenCV)
                    return i;
            }
            return -1;
        }

        //button add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMNV.Text == "" || txtTenNV.Text == "" || cmbPhongBan.Text == "" || cmbChucVu.Text == "" || txtDiaChi.Text == "" ||
                        txtCMND.Text == "" || txtSDT.Text == "" || txtTrinhDo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin nhân viên");

                if (GetSelectedRow(txtMNV.Text) == -1)
                {
                    if (checkPBCV(cmbPhongBan.Text, cmbChucVu.Text) == -1)
                    {
                        NhanVien s = new NhanVien()
                        {
                            MaNV = txtMNV.Text,
                            TenNV = txtTenNV.Text,
                            MaPB = cmbPhongBan.SelectedValue.ToString(),
                            MaChucVu = cmbChucVu.SelectedValue.ToString(),
                            NgaySinh = Convert.ToDateTime(dateTimeNgaySinh.Value.ToString()),
                            DiaChi = txtDiaChi.Text,
                            CMND = txtCMND.Text,
                            SDT = txtSDT.Text,
                            TrinhDo = txtTrinhDo.Text,
                            Email = txtEmail.Text,
                            NgayVaoLam = Convert.ToDateTime(dateTimeNVaoLam.Value.ToString())
                        };
                        if (radioNam.Checked == true)
                            s.GioiTinh = "Nam";
                        else
                            s.GioiTinh = "Nữ";
                        context.NhanViens.Add(s);
                        context.SaveChanges();
                        loadDGV();
                        refresh();
                        MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Trong 1 phòng ban chỉ tồn tại 1 chức vụ!", "Thông báo", MessageBoxButtons.OK);
                    }

                }
                else
                {
                    MessageBox.Show("Nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK);
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
                if (txtMNV.Text == "" || txtTenNV.Text == "" || cmbPhongBan.Text == "" || cmbChucVu.Text == "" || txtDiaChi.Text == "" ||
                        txtCMND.Text == "" || txtSDT.Text == "" || txtTrinhDo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin nhân viên");

                NhanVien dbUpdate = context.NhanViens.FirstOrDefault(p => p.MaNV == txtMNV.Text);
                if (dbUpdate != null)
                {
                    dbUpdate.TenNV = txtTenNV.Text;
                    dbUpdate.MaPB = cmbPhongBan.SelectedValue.ToString();
                    dbUpdate.MaChucVu = cmbChucVu.SelectedValue.ToString();
                    dbUpdate.NgaySinh = Convert.ToDateTime(dateTimeNgaySinh.Value.ToString());
                    dbUpdate.DiaChi = txtDiaChi.Text;
                    dbUpdate.CMND = txtCMND.Text;
                    dbUpdate.SDT = txtSDT.Text;
                    dbUpdate.TrinhDo = txtTrinhDo.Text;
                    dbUpdate.Email = txtEmail.Text;
                    dbUpdate.NgayVaoLam = Convert.ToDateTime(dateTimeNVaoLam.Value.ToString());
                    if (radioNam.Checked == true)
                        dbUpdate.GioiTinh = "Nam";
                    else
                        dbUpdate.GioiTinh = "Nữ";
                    context.SaveChanges();
                    loadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //set các giá trị mặc định
        private void refresh()
        {
            txtMNV.Text = "";
            txtTenNV.Text = "";
            cmbPhongBan.Text = "";
            cmbChucVu.Text = "";
            radioNam.Checked = false;
            radioNu.Checked = false;
            txtDiaChi.Text = "";
            txtCMND.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtTrinhDo.Text = "";

        }
        
        //button xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                NhanVien dbDelete = context.NhanViens.FirstOrDefault(p => p.MaNV == txtMNV.Text);
                if (dbDelete != null)
                {
                    DialogResult h = MessageBox.Show("Bạn có thực sự muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.YesNo);
                    if (h == DialogResult.Yes)
                    {
                        context.NhanViens.Remove(dbDelete);
                        List<Luong> listLuong = context.Luongs.ToList();
                        List<Luong> listDeleteLuong = listLuong.Where(p => p.MaNV == txtMNV.Text).ToList();
                        foreach (var item in listDeleteLuong)
                        {
                            context.Luongs.Remove(item);
                        }
                        //
                        List<BaoHiem> listBaoHiem = context.BaoHiems.ToList();
                        List<BaoHiem> listDeleteBaoHiem = listBaoHiem.Where(p => p.MaNV == txtMNV.Text).ToList();
                        foreach (var item in listDeleteBaoHiem)
                        {
                            context.BaoHiems.Remove(item);
                        }
                        //
                        List<ChamCong> listCC = context.ChamCongs.ToList();
                        List<ChamCong> listDeleteCC = listCC.Where(p => p.MaNV == txtMNV.Text).ToList();
                        foreach (var item in listDeleteCC)
                        {
                            context.ChamCongs.Remove(item);
                        }
                        //
                        List<Userr> listUser = context.Userrs.ToList();
                        List<Userr> listDeleteUser = listUser.Where(p => p.MaNV == txtMNV.Text).ToList();
                        foreach (var item in listDeleteUser)
                        {
                            context.Userrs.Remove(item);
                        }
                        //
                        List<ThuongPhat> listTP = context.ThuongPhats.ToList();
                        List<ThuongPhat> listDeleteTP = listTP.Where(p => p.MaNV == txtMNV.Text).ToList();
                        foreach (var item in listDeleteTP)
                        {
                            context.ThuongPhats.Remove(item);
                        }
                        context.SaveChanges();
                        context.SaveChanges();
                        loadDGV();
                        refresh();
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tim thấy nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       
        //click vào panel load lại dữ liệu
        private void panelNhanVien_Click(object sender, EventArgs e)
        {
            loadDGV();
        }

        //button search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string maNV = txtMNV.Text.Trim();
            string tenNV = txtTenNV.Text.Trim();
            string phongBan = cmbPhongBan.Text.Trim();
            string chucVu = cmbChucVu.Text.Trim();
            //radioNam.Checked = false;
            //radioNu.Checked = false;
            //string ngaySinh = dateTimeNgaySinh.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string cmnd = txtCMND.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string trinhDo = txtTrinhDo.Text.Trim();
            List<NhanVien> listNhanVien = context.NhanViens.ToList();
            List<NhanVien> listSearch = listNhanVien.Where(p => p.TrinhDo.ToLower().Contains(trinhDo.ToLower()) && p.MaNV.ToLower().Contains(maNV.ToLower()) &&
                p.TenNV.ToLower().Contains(tenNV.ToLower()) && p.PhongBan.TenPB.ToLower().Contains(phongBan.ToLower()) &&
                p.ChucVu.TenChucVu.ToLower().Contains(chucVu.ToLower()) && p.DiaChi.ToLower().Contains(diaChi.ToLower()) &&
                p.CMND.Contains(cmnd) && p.SDT.Contains(sdt) && p.Email.ToLower().Contains(email.ToLower())).ToList();
            if (listSearch.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Thông báo", MessageBoxButtons.OK);
            }
            else
                BindGrid(listSearch);
        }   
    }
}
