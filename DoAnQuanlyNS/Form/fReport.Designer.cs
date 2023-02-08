
namespace DoAnQuanlyNS
{
    partial class fReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fReport));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cmbPB = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioBangLuong = new System.Windows.Forms.RadioButton();
            this.radioPB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(12, 92);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(948, 346);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Visible = false;
            // 
            // cmbPB
            // 
            this.cmbPB.Enabled = false;
            this.cmbPB.FormattingEnabled = true;
            this.cmbPB.Location = new System.Drawing.Point(317, 49);
            this.cmbPB.Name = "cmbPB";
            this.cmbPB.Size = new System.Drawing.Size(121, 21);
            this.cmbPB.TabIndex = 1;
            this.cmbPB.SelectedIndexChanged += new System.EventHandler(this.cmbPB_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(469, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Xem";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioBangLuong
            // 
            this.radioBangLuong.AutoSize = true;
            this.radioBangLuong.Checked = true;
            this.radioBangLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBangLuong.Location = new System.Drawing.Point(92, 12);
            this.radioBangLuong.Name = "radioBangLuong";
            this.radioBangLuong.Size = new System.Drawing.Size(209, 20);
            this.radioBangLuong.TabIndex = 4;
            this.radioBangLuong.TabStop = true;
            this.radioBangLuong.Text = "Xem tất cả lương của nhân viên";
            this.radioBangLuong.UseVisualStyleBackColor = true;
            this.radioBangLuong.CheckedChanged += new System.EventHandler(this.radioBangLuong_CheckedChanged);
            // 
            // radioPB
            // 
            this.radioPB.AutoSize = true;
            this.radioPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPB.Location = new System.Drawing.Point(92, 50);
            this.radioPB.Name = "radioPB";
            this.radioPB.Size = new System.Drawing.Size(185, 20);
            this.radioPB.TabIndex = 5;
            this.radioPB.Text = "Xem lương theo phòng ban";
            this.radioPB.UseVisualStyleBackColor = true;
            this.radioPB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // fReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(972, 450);
            this.Controls.Add(this.radioPB);
            this.Controls.Add(this.radioBangLuong);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbPB);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fReport";
            this.Load += new System.EventHandler(this.fReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ComboBox cmbPB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioBangLuong;
        private System.Windows.Forms.RadioButton radioPB;
    }
}