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
    public partial class fPhongBan : Form
    {
        public fPhongBan()
        {
            InitializeComponent();
        }
        public fPhongBan(fNhanVien form)
        {
            InitializeComponent();
            form1 = form;
        }
        fNhanVien form1 = new fNhanVien();
        StaffContextDB context = new StaffContextDB();

        //Form load
        private void fPhongBan_Load(object sender, EventArgs e)
        {
            LoadDGV();
        }
        
        // viết hàm Load riêng để tiện cho việc tái sử dụng
        private void LoadDGV()
        {
            try
            {
                List<PhongBan> listPhongBan = context.PhongBans.ToList();               
                BindGrid(listPhongBan);            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //kết nối database và form
        private void BindGrid(List<PhongBan> listPhongBan)
        {
            dgvPhongBan.Rows.Clear();
            foreach (var item in listPhongBan)
            {
                int index = dgvPhongBan.Rows.Add();
                dgvPhongBan.Rows[index].Cells[0].Value = item.MaPB;
                dgvPhongBan.Rows[index].Cells[1].Value = item.TenPB;
                dgvPhongBan.Rows[index].Cells[2].Value = item.SoNhanVien;
            }
        }

        //click dgv hiện thị ra textbox
        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = dgvPhongBan.Rows[ind];
            txtMPB.Text = selectedRows.Cells[0].Value.ToString();
            txtTenPB.Text = selectedRows.Cells[1].Value.ToString();
            txtSoNV.Text = selectedRows.Cells[2].Value.ToString();
        }

        //button thêm
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMPB.Text == "" || txtTenPB.Text == "" || txtSoNV.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin phòng ban cần thêm");
                if (int.Parse(txtSoNV.Text) < 0 )
                    throw new Exception("Tổng số nhân viên trong phòng ban không được < 0");           

                if (GetSelectedRow(txtMPB.Text, txtTenPB.Text) == -1)
                {
                    PhongBan p = new PhongBan()
                    {
                        MaPB = txtMPB.Text,
                        TenPB = txtTenPB.Text,
                        SoNhanVien = int.Parse(txtSoNV.Text)                       
                    };                  
                    context.PhongBans.Add(p);
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Phòng ban đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //getselected row
        private int GetSelectedRow(string phongBanID, string tenPB)
        {
            for (int i = 0; i < dgvPhongBan.Rows.Count - 1; i++)
            {
                if (dgvPhongBan.Rows[i].Cells[0].Value.ToString() == phongBanID || dgvPhongBan.Rows[i].Cells[1].Value.ToString() == tenPB)
                {
                    return i;
                }
            }
            return -1;
        }
        
        //hàm làm mới
        private void refresh()
        {
            txtMPB.Text = "";
            txtTenPB.Text = "";
            txtSoNV.Text = "";
        }

        //button update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMPB.Text == "" || txtTenPB.Text == "" || txtSoNV.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin phòng ban cần thêm");
                if (int.Parse(txtSoNV.Text) < 0)
                    throw new Exception("Tổng số nhân viên trong phòng ban không được < 0");

                PhongBan dbUpdate = context.PhongBans.FirstOrDefault(p => p.MaPB == txtMPB.Text);
                if (dbUpdate != null)
                {
                    dbUpdate.TenPB = txtTenPB.Text;
                    dbUpdate.SoNhanVien = int.Parse(txtSoNV.Text);                   
                    context.SaveChanges();
                    LoadDGV();
                    refresh();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tim thấy phòng ban cần sửa!", "Thông báo", MessageBoxButtons.OK);
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
                PhongBan dbDelete = context.PhongBans.FirstOrDefault(p => p.MaPB == txtMPB.Text);
                if (dbDelete != null)
                {
                    DialogResult h = MessageBox.Show("Vui lòng chuyển toàn bộ nhân viên trong phòng ban này sang phòng ban khác" +
                        "                              \nHoặc xóa toàn bộ thông tin nhân viên trong phòng ban này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (h == DialogResult.Yes)
                    {
                        context.PhongBans.Remove(dbDelete);

                        List<NhanVien> listNhanVien = context.NhanViens.ToList();
                        List<NhanVien> listDeleteNhanVien = listNhanVien.Where(p => p.MaPB == txtMPB.Text).ToList();

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
                        MessageBox.Show("Xóa phòng ban thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tim thấy phòng ban cần xóa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }      
       
    }
}
