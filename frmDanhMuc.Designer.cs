namespace QuanLyChiTieu
{
    partial class frmDanhMuc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDanhMuc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThemDanhMuc = new System.Windows.Forms.Button();
            this.btnSửaDanhMuc = new System.Windows.Forms.Button();
            this.btnXoaDanhMuc = new System.Windows.Forms.Button();
            this.txtTenDM = new System.Windows.Forms.TextBox();
            this.rdoChi = new System.Windows.Forms.RadioButton();
            this.rdoThu = new System.Windows.Forms.RadioButton();
            this.cboLoaiDM = new System.Windows.Forms.ComboBox();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.dgvListDanhMuc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDanhMuc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDanhMuc
            // 
            this.dgvDanhMuc.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhMuc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhMuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhMuc.EnableHeadersVisualStyles = false;
            this.dgvDanhMuc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.dgvDanhMuc.Location = new System.Drawing.Point(0, 0);
            this.dgvDanhMuc.Name = "dgvDanhMuc";
            this.dgvDanhMuc.RowHeadersWidth = 51;
            this.dgvDanhMuc.RowTemplate.Height = 24;
            this.dgvDanhMuc.Size = new System.Drawing.Size(920, 547);
            this.dgvDanhMuc.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Danh sách danh mục";
            // 
            // btnThemDanhMuc
            // 
            this.btnThemDanhMuc.Location = new System.Drawing.Point(402, 103);
            this.btnThemDanhMuc.Name = "btnThemDanhMuc";
            this.btnThemDanhMuc.Size = new System.Drawing.Size(75, 29);
            this.btnThemDanhMuc.TabIndex = 2;
            this.btnThemDanhMuc.Text = "Thêm";
            this.btnThemDanhMuc.UseVisualStyleBackColor = true;
            // 
            // btnSửaDanhMuc
            // 
            this.btnSửaDanhMuc.Location = new System.Drawing.Point(402, 163);
            this.btnSửaDanhMuc.Name = "btnSửaDanhMuc";
            this.btnSửaDanhMuc.Size = new System.Drawing.Size(75, 29);
            this.btnSửaDanhMuc.TabIndex = 3;
            this.btnSửaDanhMuc.Text = "Sửa";
            this.btnSửaDanhMuc.UseVisualStyleBackColor = true;
            // 
            // btnXoaDanhMuc
            // 
            this.btnXoaDanhMuc.Location = new System.Drawing.Point(402, 229);
            this.btnXoaDanhMuc.Name = "btnXoaDanhMuc";
            this.btnXoaDanhMuc.Size = new System.Drawing.Size(75, 29);
            this.btnXoaDanhMuc.TabIndex = 4;
            this.btnXoaDanhMuc.Text = "Xóa";
            this.btnXoaDanhMuc.UseVisualStyleBackColor = true;
            // 
            // txtTenDM
            // 
            this.txtTenDM.Location = new System.Drawing.Point(126, 110);
            this.txtTenDM.Name = "txtTenDM";
            this.txtTenDM.Size = new System.Drawing.Size(169, 22);
            this.txtTenDM.TabIndex = 5;
            // 
            // rdoChi
            // 
            this.rdoChi.AutoSize = true;
            this.rdoChi.Location = new System.Drawing.Point(79, 215);
            this.rdoChi.Name = "rdoChi";
            this.rdoChi.Size = new System.Drawing.Size(86, 20);
            this.rdoChi.TabIndex = 6;
            this.rdoChi.TabStop = true;
            this.rdoChi.Text = "Khoản chi";
            this.rdoChi.UseVisualStyleBackColor = true;
            // 
            // rdoThu
            // 
            this.rdoThu.AutoSize = true;
            this.rdoThu.Location = new System.Drawing.Point(225, 215);
            this.rdoThu.Name = "rdoThu";
            this.rdoThu.Size = new System.Drawing.Size(86, 20);
            this.rdoThu.TabIndex = 7;
            this.rdoThu.TabStop = true;
            this.rdoThu.Text = "Khoản thu";
            this.rdoThu.UseVisualStyleBackColor = true;
            // 
            // cboLoaiDM
            // 
            this.cboLoaiDM.FormattingEnabled = true;
            this.cboLoaiDM.Location = new System.Drawing.Point(79, 281);
            this.cboLoaiDM.Name = "cboLoaiDM";
            this.cboLoaiDM.Size = new System.Drawing.Size(107, 24);
            this.cboLoaiDM.TabIndex = 8;
            // 
            // pnlInput
            // 
            this.pnlInput.Location = new System.Drawing.Point(79, 345);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(368, 149);
            this.pnlInput.TabIndex = 9;
            // 
            // dgvListDanhMuc
            // 
            this.dgvListDanhMuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDanhMuc.Location = new System.Drawing.Point(468, 345);
            this.dgvListDanhMuc.Name = "dgvListDanhMuc";
            this.dgvListDanhMuc.RowHeadersWidth = 51;
            this.dgvListDanhMuc.RowTemplate.Height = 24;
            this.dgvListDanhMuc.Size = new System.Drawing.Size(405, 149);
            this.dgvListDanhMuc.TabIndex = 10;
            // 
            // frmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 547);
            this.Controls.Add(this.dgvListDanhMuc);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.cboLoaiDM);
            this.Controls.Add(this.rdoThu);
            this.Controls.Add(this.rdoChi);
            this.Controls.Add(this.txtTenDM);
            this.Controls.Add(this.btnXoaDanhMuc);
            this.Controls.Add(this.btnSửaDanhMuc);
            this.Controls.Add(this.btnThemDanhMuc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDanhMuc);
            this.Name = "frmDanhMuc";
            this.Text = "frmDanhMuc";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDanhMuc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhMuc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemDanhMuc;
        private System.Windows.Forms.Button btnSửaDanhMuc;
        private System.Windows.Forms.Button btnXoaDanhMuc;
        private System.Windows.Forms.TextBox txtTenDM;
        private System.Windows.Forms.RadioButton rdoChi;
        private System.Windows.Forms.RadioButton rdoThu;
        private System.Windows.Forms.ComboBox cboLoaiDM;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.DataGridView dgvListDanhMuc;
    }
}