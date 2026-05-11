using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;

using System.Text.RegularExpressions;



using System.Configuration; // Đừng quên dòng này nhé!

namespace QuanLyChiTieu.BLL
{
    public class NganSachBLL
    {
        private readonly NganSachDAL _dal = new NganSachDAL();
        public DataTable GetTheoThang(int maNguoiDung, int thang, int nam)
            => _dal.GetTheoThang(maNguoiDung, thang, nam);
        public (decimal tongNganSach, decimal tongDaChi) GetTong(int maNguoiDung, int thang, int nam)
            => _dal.GetTong(maNguoiDung, thang, nam);
        public bool Them(int maNguoiDung, int maDanhMuc, decimal soTien, int thang, int nam)
            => _dal.Them(maNguoiDung, maDanhMuc, soTien, thang, nam) > 0;
        public bool Xoa(int maNganSach) => _dal.Xoa(maNganSach) > 0;
    }
}