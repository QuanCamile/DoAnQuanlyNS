using DoAnQuanlyNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQuanlyNS
{
    public partial class fLogin : Form
    {     
        public fLogin()
        {
            InitializeComponent();
        }

        StaffContextDB context = new StaffContextDB();
        
        //bật tắt hiển thị mật khẩu
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
                txtPass.UseSystemPasswordChar = true; 
        }

        //button login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            fMain f = new fMain();
            //fNhanVien f1 = new fNhanVien();
            Userr dbAccount = context.Userrs.FirstOrDefault(p => p.UserName == txtUserName.Text && p.PassWord == txtPass.Text);
            if (dbAccount == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                Userr dbAccount0 = context.Userrs.FirstOrDefault(p => p.UserName == txtUserName.Text && p.PassWord == txtPass.Text && p.IDPer == 0);
                Userr dbAccount1 = context.Userrs.FirstOrDefault(p => p.UserName == txtUserName.Text && p.PassWord == txtPass.Text && p.IDPer == 1);
                if (dbAccount0 != null)
                {
                    //f1.btnAdd.Enabled = false;
                    f.bảngLươngToolStripMenuItem.Enabled = f.bảngLươngToolStripMenuItem1.Enabled =
                        f.phòngBanToolStripMenuItem.Enabled=f.chấmCôngToolStripMenuItem.Enabled=f.chứcVụToolStripMenuItem.Enabled=
                        f.quảnLýTàiKhoảnToolStripMenuItem.Enabled = f.thưởngPhạtToolStripMenuItem.Enabled = false;
                    f.ShowDialog();
                }
                txtUserName.Text = "";
                txtPass.Text = "";
                if (dbAccount1 != null)
                {
                    f.ShowDialog();
                }
            }
        }

        //button logout
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //đóng form
        private void fLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //nút x đóng form
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
