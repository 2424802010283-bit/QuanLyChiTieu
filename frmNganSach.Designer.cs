namespace QuanLyChiTieu
{
    partial class frmNganSach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSubTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.progressBudget = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblTienDaDung = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.cboLoai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtNganSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpThang = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblLoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSoTien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThang = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvNganSach = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNganSach)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.lblSubTitle);
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1044, 100);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = false;
            this.lblSubTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.Location = new System.Drawing.Point(22, 58);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(279, 46);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Quản lý ngân sách hàng tháng";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(22, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 67);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ngân sách";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.guna2Panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1044, 520);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.progressBudget);
            this.guna2Panel2.Controls.Add(this.lblTienDaDung);
            this.guna2Panel2.Controls.Add(this.btnLuu);
            this.guna2Panel2.Controls.Add(this.cboLoai);
            this.guna2Panel2.Controls.Add(this.txtNganSach);
            this.guna2Panel2.Controls.Add(this.dtpThang);
            this.guna2Panel2.Controls.Add(this.lblLoai);
            this.guna2Panel2.Controls.Add(this.lblSoTien);
            this.guna2Panel2.Controls.Add(this.lblThang);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.Location = new System.Drawing.Point(3, 3);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(307, 514);
            this.guna2Panel2.TabIndex = 0;
            // 
            // progressBudget
            // 
            this.progressBudget.BorderRadius = 8;
            this.progressBudget.Location = new System.Drawing.Point(28, 376);
            this.progressBudget.Name = "progressBudget";
            this.progressBudget.Size = new System.Drawing.Size(245, 25);
            this.progressBudget.TabIndex = 8;
            this.progressBudget.Text = "guna2ProgressBar1";
            this.progressBudget.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progressBudget.Value = 60;
            // 
            // lblTienDaDung
            // 
            this.lblTienDaDung.AutoSize = false;
            this.lblTienDaDung.BackColor = System.Drawing.Color.Transparent;
            this.lblTienDaDung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienDaDung.Location = new System.Drawing.Point(28, 340);
            this.lblTienDaDung.Name = "lblTienDaDung";
            this.lblTienDaDung.Size = new System.Drawing.Size(261, 46);
            this.lblTienDaDung.TabIndex = 7;
            this.lblTienDaDung.Text = "Đã dùng 3tr / 5tr ngân sách";
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 20;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(58, 257);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(180, 45);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu ngân sách";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cboLoai
            // 
            this.cboLoai.BackColor = System.Drawing.Color.Transparent;
            this.cboLoai.BorderRadius = 8;
            this.cboLoai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoai.ItemHeight = 30;
            this.cboLoai.Items.AddRange(new object[] {
            "Cá nhân",
            "Ăn uống",
            "Học tập",
            "Giải trí"});
            this.cboLoai.Location = new System.Drawing.Point(28, 190);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(245, 36);
            this.cboLoai.TabIndex = 5;
            // 
            // txtNganSach
            // 
            this.txtNganSach.BorderRadius = 8;
            this.txtNganSach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNganSach.DefaultText = "";
            this.txtNganSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNganSach.Location = new System.Drawing.Point(28, 113);
            this.txtNganSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNganSach.Name = "txtNganSach";
            this.txtNganSach.PlaceholderText = "Nhập số tiền...";
            this.txtNganSach.SelectedText = "";
            this.txtNganSach.Size = new System.Drawing.Size(245, 40);
            this.txtNganSach.TabIndex = 4;
            // 
            // dtpThang
            // 
            this.dtpThang.BorderRadius = 8;
            this.dtpThang.Checked = true;
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(28, 37);
            this.dtpThang.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpThang.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(245, 40);
            this.dtpThang.TabIndex = 3;
            this.dtpThang.Value = new System.DateTime(2026, 5, 11, 0, 0, 0, 0);
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = false;
            this.lblLoai.BackColor = System.Drawing.Color.Transparent;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoai.Location = new System.Drawing.Point(28, 162);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(181, 46);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại ngân sách";
            // 
            // lblSoTien
            // 
            this.lblSoTien.AutoSize = false;
            this.lblSoTien.BackColor = System.Drawing.Color.Transparent;
            this.lblSoTien.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoTien.Location = new System.Drawing.Point(28, 85);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(152, 46);
            this.lblSoTien.TabIndex = 1;
            this.lblSoTien.Text = "Ngân sách";
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = false;
            this.lblThang.BackColor = System.Drawing.Color.Transparent;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblThang.Location = new System.Drawing.Point(28, 9);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(123, 46);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Tháng";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.dgvNganSach);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel3.Location = new System.Drawing.Point(316, 3);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(725, 514);
            this.guna2Panel3.TabIndex = 1;
            // 
            // dgvNganSach
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvNganSach.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNganSach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNganSach.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNganSach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNganSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNganSach.GridColor = System.Drawing.Color.LightGray;
            this.dgvNganSach.Location = new System.Drawing.Point(0, 0);
            this.dgvNganSach.Name = "dgvNganSach";
            this.dgvNganSach.RowHeadersVisible = false;
            this.dgvNganSach.RowHeadersWidth = 51;
            this.dgvNganSach.RowTemplate.Height = 24;
            this.dgvNganSach.Size = new System.Drawing.Size(725, 514);
            this.dgvNganSach.TabIndex = 0;
            this.dgvNganSach.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvNganSach.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvNganSach.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvNganSach.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvNganSach.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvNganSach.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvNganSach.ThemeStyle.GridColor = System.Drawing.Color.LightGray;
            this.dgvNganSach.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.Navy;
            this.dgvNganSach.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvNganSach.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNganSach.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvNganSach.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNganSach.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvNganSach.ThemeStyle.ReadOnly = false;
            this.dgvNganSach.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvNganSach.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNganSach.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNganSach.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvNganSach.ThemeStyle.RowsStyle.Height = 24;
            this.dgvNganSach.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dgvNganSach.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // frmNganSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 620);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "frmNganSach";
            this.Text = "frmNganSach";
            this.Load += new System.EventHandler(this.frmNganSach_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNganSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThang;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoTien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLoai;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpThang;
        private Guna.UI2.WinForms.Guna2TextBox txtNganSach;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoai;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2DataGridView dgvNganSach;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBudget;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTienDaDung;
    }
}