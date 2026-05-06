namespace QuanLyChiTieu
{
    partial class frmTongQuan
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
            this.dgvGiaoDich = new System.Windows.Forms.DataGridView();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prbNganSach = new System.Windows.Forms.ProgressBar();
            this.btnNhapNhanh = new System.Windows.Forms.Button();
            this.txtSmartInput = new System.Windows.Forms.TextBox();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGiaoDich
            // 
            this.dgvGiaoDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiaoDich.Location = new System.Drawing.Point(140, 323);
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.RowHeadersWidth = 51;
            this.dgvGiaoDich.RowTemplate.Height = 24;
            this.dgvGiaoDich.Size = new System.Drawing.Size(943, 256);
            this.dgvGiaoDich.TabIndex = 11;
            // 
            // lblSoTien
            // 
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.ForeColor = System.Drawing.SystemColors.Window;
            this.lblSoTien.Location = new System.Drawing.Point(440, 51);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(110, 16);
            this.lblSoTien.TabIndex = 10;
            this.lblSoTien.Text = "0 / 3,000,000 VNĐ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(111, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ngân sách tháng này:";
            // 
            // prbNganSach
            // 
            this.prbNganSach.Location = new System.Drawing.Point(140, 70);
            this.prbNganSach.Name = "prbNganSach";
            this.prbNganSach.Size = new System.Drawing.Size(386, 47);
            this.prbNganSach.TabIndex = 8;
            // 
            // btnNhapNhanh
            // 
            this.btnNhapNhanh.Location = new System.Drawing.Point(353, 203);
            this.btnNhapNhanh.Name = "btnNhapNhanh";
            this.btnNhapNhanh.Size = new System.Drawing.Size(93, 40);
            this.btnNhapNhanh.TabIndex = 7;
            this.btnNhapNhanh.Text = "Nhập nhanh";
            this.btnNhapNhanh.UseVisualStyleBackColor = true;
            this.btnNhapNhanh.Click += new System.EventHandler(this.btnNhapNhanh_Click_1);
            // 
            // txtSmartInput
            // 
            this.txtSmartInput.Location = new System.Drawing.Point(222, 212);
            this.txtSmartInput.Name = "txtSmartInput";
            this.txtSmartInput.Size = new System.Drawing.Size(100, 22);
            this.txtSmartInput.TabIndex = 6;
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.AutoSize = true;
            this.lblCanhBao.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCanhBao.Location = new System.Drawing.Point(462, 130);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(100, 16);
            this.lblCanhBao.TabIndex = 12;
            this.lblCanhBao.Text = "Đang tính toán...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(111, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nhập chi tiêu";
            // 
            // frmTongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1200, 668);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCanhBao);
            this.Controls.Add(this.dgvGiaoDich);
            this.Controls.Add(this.lblSoTien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prbNganSach);
            this.Controls.Add(this.btnNhapNhanh);
            this.Controls.Add(this.txtSmartInput);
            this.Name = "frmTongQuan";
            this.Text = "frmTongQuan";
            this.Load += new System.EventHandler(this.frmTongQuan_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGiaoDich;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar prbNganSach;
        private System.Windows.Forms.Button btnNhapNhanh;
        private System.Windows.Forms.TextBox txtSmartInput;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.Label label1;
    }
}