using DoAnQuanlyNS.Models;
using Microsoft.Reporting.WinForms;
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
    public partial class fReport : Form
    {
        public fReport()
        {
            InitializeComponent();
        }
        StaffContextDB context = new StaffContextDB();
        private void fReport_Load(object sender, EventArgs e)
        {
            List<PhongBan> listPhongBan = context.PhongBans.ToList();
            FillPhongBanCombobox(listPhongBan);
            this.reportViewer1.RefreshReport();           
        }
        public void FillPhongBanCombobox(List<PhongBan> listPhongBan)
        {
            this.cmbPB.DataSource = listPhongBan ;
            this.cmbPB.DisplayMember = "TenPB";
            this.cmbPB.ValueMember = "TenPB";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            reportViewer1.Visible = true;
            if (radioBangLuong.Checked == true)
            {
                List<Luong> listLuong = context.Luongs.ToList();
                List<BangLuongReport> listReport = new List<BangLuongReport>();

                foreach (Luong luong in listLuong)
                {
                    BangLuongReport r = new BangLuongReport();
                    r.MNV = luong.MaNV;
                    r.TenNV = luong.NhanVien.TenNV;
                    r.LuongCB = luong.LuongCB;
                    r.HeSoLuong = (float)luong.HeSoLuong;
                    r.PhuCap = luong.NhanVien.ChucVu.PhuCap;
                    r.SoNgayLam = luong.ChamCong.SoNgayLam;
                    r.SoTienThuong = luong.ThuongPhat.SoTienThuong;
                    r.SoTienPhat = luong.ThuongPhat.SoTienPhat;
                    listReport.Add(r);
                }
                //ReportParameter[] param = new ReportParameter[1];
                //param[0] = new ReportParameter("TenPB", phongBan.TenPB);
                //this.reportViewer1.LocalReport.SetParameters(param);
                reportViewer1.LocalReport.ReportPath = "SatffReport.rdlc";
                var src = new ReportDataSource("BangLuongDataSet", listReport);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(src);
                this.reportViewer1.LocalReport.DisplayName = "Bảng Lương";
            }
            else
            {
                if(radioPB.Checked == true)
                {
                    PhongBan phongBan = context.PhongBans.FirstOrDefault(p => p.TenPB == cmbPB.Text);
                    List<Luong> listLuong = context.Luongs.Where(p => p.NhanVien.PhongBan.TenPB == cmbPB.Text).ToList();

                    if (phongBan == null || listLuong.Count() == 0)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên nào trong phòng ban này!");
                        return;
                    }
                    List<BangLuongReport> listReport = new List<BangLuongReport>();
                    foreach (Luong luong in listLuong)
                    {
                        BangLuongReport r = new BangLuongReport();
                        r.MNV = luong.MaNV;
                        r.TenNV = luong.NhanVien.TenNV;
                        r.LuongCB = luong.LuongCB;
                        r.HeSoLuong = (float)luong.HeSoLuong;
                        r.PhuCap = luong.NhanVien.ChucVu.PhuCap;
                        r.SoNgayLam = luong.ChamCong.SoNgayLam;
                        r.SoTienThuong = luong.ThuongPhat.SoTienThuong;
                        r.SoTienPhat = luong.ThuongPhat.SoTienPhat;
                        listReport.Add(r);
                    }
                    ReportParameter[] param = new ReportParameter[1];
                    param[0] = new ReportParameter("TenPB", phongBan.TenPB);
                   
                    reportViewer1.LocalReport.ReportPath = "PBReport.rdlc";
                    this.reportViewer1.LocalReport.SetParameters(param);
                    var src = new ReportDataSource("PBDataSet", listReport);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(src);
                    this.reportViewer1.LocalReport.DisplayName = "Bảng Lương";
                }
                
                

            }
            this.reportViewer1.RefreshReport();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioBangLuong_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBangLuong.Checked == true)
                cmbPB.Enabled = false;
        }

        private void cmbPB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPB.Checked == true)
                cmbPB.Enabled = true;
        }
    }
}
