using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace QuanLyChiTieu.BLL
{
    public class NguoiDungBLL
    {
        private readonly NguoiDungDAL _dal = new NguoiDungDAL();

        // Hàm phụ dùng để kiểm tra định dạng Email hoặc SĐT
        private bool KiemTraDinhDangEmailSdt(string input)
        {
            // Regex SĐT: Bắt đầu bằng số 0, theo sau là 9 chữ số (tổng 10 số)
            string patternSdt = @"^0\d{9}$";

            // Regex Email: Chuẩn định dạng email có @ và dấu chấm
            string patternEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Trả về true nếu khớp 1 trong 2 định dạng trên
            return Regex.IsMatch(input, patternSdt) || Regex.IsMatch(input, patternEmail);
        }

        public NguoiDungDTO DangNhap(string taiKhoan, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau))
                return null;
            return _dal.KiemTraDangNhap(taiKhoan, matKhau);
        }

        public string DangKy(NguoiDungDTO nd, string matKhauNhapLai, bool dongYDieuKhoan)
        {
            if (!dongYDieuKhoan)
                return "Bạn phải đồng ý với điều khoản sử dụng!";

            if (string.IsNullOrWhiteSpace(nd.HoTen) || string.IsNullOrWhiteSpace(nd.EmailOrSDT) || string.IsNullOrWhiteSpace(nd.MatKhau))
                return "Vui lòng điền đầy đủ thông tin!";

            // KIỂM TRA ĐỊNH DẠNG Ở ĐÂY
            if (!KiemTraDinhDangEmailSdt(nd.EmailOrSDT))
                return "Vui lòng nhập đúng định dạng Email hoặc Số điện thoại (10 số)!";

            if (nd.MatKhau != matKhauNhapLai)
                return "Mật khẩu nhập lại không khớp!";

            if (_dal.KiemTraTonTai(nd.EmailOrSDT))
                return "Email hoặc Số điện thoại này đã được đăng ký!";

            if (_dal.DangKy(nd))
                return "OK";

            return "Đăng ký thất bại do lỗi hệ thống!";
        }
    }
}