using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System.Data;
using System.Configuration;

namespace QuanLyChiTieu.BLL
{
    public class DanhMucBLL
    {
        private readonly DanhMucDAL _dal = new DanhMucDAL();

        // Các hàm gốc của bạn
        public List<DanhMucDTO> GetByUser(int maNguoiDung) => _dal.GetByUser(maNguoiDung);
        public List<DanhMucDTO> GetByLoai(int maNguoiDung, string loai) => _dal.GetByLoai(maNguoiDung, loai);
        public bool Them(DanhMucDTO dm) => _dal.Them(dm) > 0;
        public bool Sua(DanhMucDTO dm) => _dal.Sua(dm) > 0;
        public bool Xoa(int maDanhMuc) => _dal.Xoa(maDanhMuc) > 0;

        // ─── CÁC HÀM "PHIÊN DỊCH" ĐỂ KHỚP VỚI FORM DANH MỤC ───

        // 1. Hàm Form đang gọi để tải dữ liệu lên bảng
        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            // Tạm thời gán mã người dùng = 1 (sau này bạn có màn hình Đăng Nhập thì thay bằng Session)
            int maNguoiDung = 1;
            return _dal.GetByUser(maNguoiDung);
        }

        // 2. Hàm Form đang gọi khi nhấn nút "Thêm"
        public bool LuuDanhMuc(DanhMucDTO dm)
        {
            // Gán luôn mã người dùng = 1 trước khi lưu vào DataBase
            dm.MaNguoiDung = 1;
            return Them(dm); // Gọi lại hàm Them() ở trên
        }
    }
}