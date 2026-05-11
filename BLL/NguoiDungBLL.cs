using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace QuanLyChiTieu.BLL
{
    public class NguoiDungBLL
    {
        private readonly NguoiDungDAL _dal = new NguoiDungDAL();
        public NguoiDungDTO DangNhap(string email, string matKhau) => _dal.DangNhap(email, matKhau);
        public bool CapNhatThongTin(NguoiDungDTO nd) => _dal.CapNhatThongTin(nd) > 0;
        public bool DoiMatKhau(int maNguoiDung, string matKhauCu, string matKhauMoi)
        {
            var user = _dal.DangNhap(Session.CurrentUser.Email, matKhauCu);
            if (user == null) return false;
            return _dal.DoiMatKhau(maNguoiDung, matKhauMoi) > 0;
        }
    }
}