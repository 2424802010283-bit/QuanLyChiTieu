using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace QuanLyChiTieu
{
    public partial class frmTongQuan : Form
    {
        // 1. Nhúng API đổi màu Progress Bar (Chỉ khai báo 1 lần duy nhất ở đây)
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public frmTongQuan()
        {
            InitializeComponent();
        }

       
           
    }
}