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
    public partial class fChucVu : Form
    {
        public fChucVu()
        {
            InitializeComponent();
        }
        public fChucVu(fNhanVien form)
        {
            InitializeComponent();
            form1 = form;
        }
        fNhanVien form1 = new fNhanVien();
        StaffContextDB context = new StaffContextDB();

        //form load
        private void fChucVu_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
        //viết hàm load riêng để tiện cho việc tái sử dụng
        private void LoadDGV()
        {
            try
            {
                List<ChucVu> listChucVu = context.ChucVus.ToList();
                BindGrid(listChucVu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }    
        //data từ sql to datagridview
        private void BindGrid(List<ChucVu> listChucVu)
        {
            dgvChucVu.Rows.Clear();
            foreach (var item in listChucVu)
            {
                int index = dgvChucVu.Rows.Add();
                dgvChucVu.Rows[index].Cells[0].Value = item.MaChucVu;
                dgvChucVu.Rows[index].Cells[1].Value = item.TenChucVu;
                dgvChucVu.Rows[index].Cells[2].Value = item.PhuCap;
            }
        }

        //click datagridview data hiện lên textbox, combobox
        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvChucVu.Rows[ind];
            txtMaCV.Text = selectedRows.Cells[0].Value.ToString();
            txtTenCV.Text = selectedRows.Cells[1].Value.ToString();   
            txtPhuCap.Text = selectedRows.Cells[2].Value.ToString();
        }
        
        //hàm làm mới
        private void refresh()
        {
            txtMaCV.Text = "";
            txtTenCV.Text = "";
            txtPhuCap.Text = "";            
        }
        private int GetSelectedRow(string maChucVu, string tenCV)
        {
            for (int i = 0; i < dgvChucVu.Rows.Count - 1; i++)
            {
                if (dgvChucVu.Rows[i].Cells[0].Value.ToString() == maChucVu || dgvChucVu.Rows[i].Cells[1].Value.ToString() == tenCV)
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
                if (txtMaCV.Text == "" || txtTenCV.Text == "" || txtPhuCap.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin chức vụ muốn thêm!");
                if (int.Parse(txtPhuCap.Text) <=0)
                    throw new Exception("Phụ cấp phải lớn hơn 0!");

                if (GetSelectedRow(txtMaCV.Text, txtTenCV.Text) == -1)
                {
                    ChucVu c = new ChucVu()
                    {
                        MaChucVu = txtMaCV.Text,
                        TenChucVu = txtTenCV.Text,                  
                        PhuCap= decimal.Parse(txtPhuCap.Text)
                    };
                    context.ChucVus.Add(c);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mã và tên chức vụ không được trùng trong 1 phòng ban", "Thông báo", MessageBoxButtons.OK);
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
                if (txtMaCV.Text == "" || txtTenCV.Text == "" || txtPhuCap.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin chức vụ");
                if (int.Parse(txtPhuCap.Text) <= 0)
                    throw new Exception("Phụ cấp phải lớn hơn 0!");          

                ChucVu dbUpdate = context.ChucVus.FirstOrDefault(p => p.MaChucVu == txtMaCV.Text);
                if (dbUpdate != null)
                {
                    //dbUpdate.TenChucVu = txtTenCV.Text;
                    dbUpdate.PhuCap = decimal.Parse(txtPhuCap.Text);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy chức vụ cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //button delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                ChucVu dbDelete = context.ChucVus.FirstOrDefault(p => p.MaChucVu == txtMaCV.Text);
                if (dbDelete != null)
                {
                    DialogResult h = MessageBox.Show("Vui lòng chuyển toàn bộ nhân viên đang có chức vụ này sang chức vụ khác" +
                       "                              \nHoặc xóa toàn bộ thông tin nhân viên đang có chức vụ này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (h == DialogResult.Yes)
                    {
                        context.ChucVus.Remove(dbDelete);
                        List<NhanVien> listNhanVien = context.NhanViens.ToList();
                        List<NhanVien> listDeleteNhanVien = listNhanVien.Where(p => p.MaChucVu == txtMaCV.Text).ToList();
                        foreach (var item in listDeleteNhanVien)
                        {
                            context.NhanViens.Remove(item);
                            List<Luong> listLuong = context.Luongs.ToList();
                            List<Luong> listDeleteLuong = listLuong.Where(p => p.MaNV == item.MaNV).ToList();
                            foreach (var item1 in listDeleteLuong)
                            {
                                context.Luongs.Remove(item1);
                            }
                            //
                            List<BaoHiem> listBaoHiem = context.BaoHiems.ToList();
                            List<BaoHiem> listDeleteBaoHiem = listBaoHiem.Where(p => p.MaNV == item.MaNV).ToList();
                            foreach (var item2 in listDeleteBaoHiem)
                            {
                                context.BaoHiems.Remove(item2);
                            }
                            //
                            List<ChamCong> listCC = context.ChamCongs.ToList();
                            List<ChamCong> listDeleteCC = listCC.Where(p => p.MaNV == item.MaNV).ToList();
                            foreach (var item3 in listDeleteCC)
                            {
                                context.ChamCongs.Remove(item3);
                            }
                            //
                            List<Userr> listUser = context.Userrs.ToList();
                            List<Userr> listDeleteUser = listUser.Where(p => p.MaNV == item.MaNV).ToList();
                            foreach (var item4 in listDeleteUser)
                            {
                                context.Userrs.Remove(item4);
                            }
                            //
                            List<ThuongPhat> listTP = context.ThuongPhats.ToList();
                            List<ThuongPhat> listDeleteTP = listTP.Where(p => p.MaNV == item.MaNV).ToList();
                            foreach (var item5 in listDeleteTP)
                            {
                                context.ThuongPhats.Remove(item5);
                            }
                        }
                        context.SaveChanges();
                        context.SaveChanges();
                        LoadDGV();
                        refresh();
                        MessageBox.Show("Xóa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tim thấy chức vụ cần xóa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       
    }
}
