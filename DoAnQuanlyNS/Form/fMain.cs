
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
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }      
                  
        //hàm load, khi load lên mặc định là form nhân viên
        private void fMain_Load(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fNhanVien"))
            {
                fNhanVien frm = new fNhanVien();
                frm.MdiParent = this;
                frm.Name = "fNhanVien";
                frm.Show();
            }
            else
                ActiveChildForm("fNhanVien");
        }             

        //nút đăng xuất
        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fChangePass"))
            {
                fChangePass frm = new fChangePass();
                frm.MdiParent = this;
                frm.Name = "fChangePass";
                frm.Show();
            }
            else
                ActiveChildForm("fChangePass");
        }       
        
        //gọi form inforaccount
        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fInforAccount")) 
            {
                fInforAccount frm = new fInforAccount();
                frm.MdiParent = this;
                frm.Name = "fInforAccount";
                frm.Show();
            }
            else
                ActiveChildForm("fInforAccount");

        }
        
        //mở form Phòng Ban
        private void phòngBanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fPhongBan")) 
            {
                fPhongBan frm = new fPhongBan();
                frm.MdiParent = this;
                frm.Name = "fPhongBan";
                frm.Show();
            }
            else
                ActiveChildForm("fPhongBan");
        }
        
        //mở form Chấm công
        private void chấmCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fChamCong")) 
            {
                fChamCong frm = new fChamCong();
                frm.MdiParent = this;
                frm.Name = "fChamCong";
                frm.Show();
            }
            else
                ActiveChildForm("fChamCong");
        }

        //mở form Chức Vụ
        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fChucVu"))
            {
                fChucVu frm = new fChucVu();
                frm.MdiParent = this;
                frm.Name = "fChucVu";
                frm.Show();
            }
            else
                ActiveChildForm("fChucVu");
            // f = new fChucVu(this);
            //f.ShowDialog();
        }
        
        //mở form Bảng Lương
        private void bảngLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("test"))
            {
                fBangLuong frm = new fBangLuong();
                frm.MdiParent = this;
                frm.Name = "test";
                frm.Show();
            }
            else
                ActiveChildForm("test");
            // f = new fBangLuong();
            //f.ShowDialog();
        }

        //mở form Thưởng Phạt
        private void thưởngPhạtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fThuongPhat"))
            {
                fThuongPhat frm = new fThuongPhat();
                frm.MdiParent = this;
                frm.Name = "fThuongPhat";
                frm.Show();
            }
            else
                ActiveChildForm("fThuongPhat");
            //form1.LoadDGV();
            // f = new fThuongPhat();
            //f.ShowDialog();
        }

        //mở form Bảo Hiểm
        private void bảoHiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fBaoHiem"))
            {
                fBaoHiem frm = new fBaoHiem();
                frm.MdiParent = this;
                frm.Name = "fBaoHiem";
                frm.Show();
            }
            else
                ActiveChildForm("fBaoHiem");
            // f = new fBaoHiem();
            //f.ShowDialog();
        }       

        //gọi form bảng lương
        private void bảngLươngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fReport f = new fReport();
            f.ShowDialog();
        }

        //gọi form nhân viên
        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("fNhanVien"))
            {
                fNhanVien frm = new fNhanVien();
                frm.MdiParent = this;
                frm.Name = "fNhanVien";
                frm.Show();
            }
            else
                ActiveChildForm("fNhanVien");
        }
        
        //hàm kiểm tra form có đang mở không
        private bool CheckExitsForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if(frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if(frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
