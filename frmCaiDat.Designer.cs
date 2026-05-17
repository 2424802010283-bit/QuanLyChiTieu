namespace QuanLyChiTieu
{
    partial class frmCaiDat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaiDat));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNganSach = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnXuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thiết lập ngân sách tháng (VNĐ):";
            // 
            // txtNganSach
            // 
            this.txtNganSach.Location = new System.Drawing.Point(278, 70);
            this.txtNganSach.Name = "txtNganSach";
            this.txtNganSach.Size = new System.Drawing.Size(100, 22);
            this.txtNganSach.TabIndex = 1;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(184, 126);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 31);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu cài đặt";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.White;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDong.ForeColor = System.Drawing.Color.Black;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.ImageSize = new System.Drawing.Size(24, 24);
            this.btnDong.Location = new System.Drawing.Point(712, 262);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(89, 64);
            this.btnDong.TabIndex = 11;
            this.btnDong.Text = "Đóng";
            // 
            // btnXuat
            // 
            this.btnXuat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuat.FillColor = System.Drawing.Color.White;
            this.btnXuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuat.ForeColor = System.Drawing.Color.Black;
            this.btnXuat.Image = ((System.Drawing.Image)(resources.GetObject("btnXuat.Image")));
            this.btnXuat.ImageSize = new System.Drawing.Size(24, 24);
            this.btnXuat.Location = new System.Drawing.Point(606, 262);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(89, 61);
            this.btnXuat.TabIndex = 10;
            this.btnXuat.Text = "Xuất";
            // 
            // btnLoc
            // 
            this.btnLoc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoc.FillColor = System.Drawing.Color.White;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoc.ForeColor = System.Drawing.Color.Black;
            this.btnLoc.Image = ((System.Drawing.Image)(resources.GetObject("btnLoc.Image")));
            this.btnLoc.ImageSize = new System.Drawing.Size(24, 24);
            this.btnLoc.Location = new System.Drawing.Point(511, 262);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(89, 61);
            this.btnLoc.TabIndex = 9;
            this.btnLoc.Text = "Lọc";
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.White;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.Black;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.ImageSize = new System.Drawing.Size(24, 24);
            this.btnXoa.Location = new System.Drawing.Point(416, 262);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(89, 61);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Xóa";
            // 
            // btnSua
            // 
            this.btnSua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.White;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSua.Location = new System.Drawing.Point(308, 262);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(89, 61);
            this.btnSua.TabIndex = 7;
            this.btnSua.Text = "Sửa";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.FillColor = System.Drawing.Color.White;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.ImageSize = new System.Drawing.Size(24, 24);
            this.btnThem.Location = new System.Drawing.Point(197, 262);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(89, 61);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm";
            // 
            // frmCaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 588);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtNganSach);
            this.Controls.Add(this.label1);
            this.Name = "frmCaiDat";
            this.Text = "frmThongKe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNganSach;
        private System.Windows.Forms.Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2Button btnXuat;
        private Guna.UI2.WinForms.Guna2Button btnLoc;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnThem;
    }
}