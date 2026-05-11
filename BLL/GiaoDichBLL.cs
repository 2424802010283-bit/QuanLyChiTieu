using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace QuanLyChiTieu.BLL
{
    public class GiaoDichBLL
    {
        private readonly GiaoDichDAL _gdDAL = new GiaoDichDAL();
        private readonly TaiKhoanDAL _tkDAL = new TaiKhoanDAL();

        public List<GiaoDichDTO> GetByLoaiVaNgay(int maNguoiDung, string loai, DateTime ngay)
            => _gdDAL.GetByLoaiVaNgay(maNguoiDung, loai, ngay);
        public List<GiaoDichDTO> GetAll(int maNguoiDung) => _gdDAL.GetAll(maNguoiDung);
        public List<GiaoDichDTO> Search(int maNguoiDung, string kw) => _gdDAL.Search(maNguoiDung, kw);
        public List<GiaoDichDTO> GetGanDay(int maNguoiDung) => _gdDAL.GetGanDay(maNguoiDung);
        public decimal GetTong(int maNguoiDung, string loai, int thang, int nam)
            => _gdDAL.GetTong(maNguoiDung, loai, thang, nam);
        public DataTable GetThongKeTheoThang(int maNguoiDung, int nam)
            => _gdDAL.GetThongKeTheoThang(maNguoiDung, nam);
        public DataTable GetTop5DanhMucChi(int maNguoiDung, int thang, int nam)
            => _gdDAL.GetTop5DanhMucChi(maNguoiDung, thang, nam);
        public DataTable BaoCao(int maNguoiDung, DateTime tuNgay, DateTime denNgay, string loai = null)
            => _gdDAL.BaoCao(maNguoiDung, tuNgay, denNgay, loai);

        public bool Them(GiaoDichDTO gd)
        {
            int maGd = _gdDAL.Them(gd);
            if (maGd == 0) return false;
            if (gd.LoaiGiaoDich == "Thu")
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, gd.SoTien);
            else if (gd.LoaiGiaoDich == "Chi")
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, -gd.SoTien);
            else if (gd.LoaiGiaoDich == "ChuyenTien" && gd.MaTaiKhoanNhan.HasValue)
            {
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, -gd.SoTien);
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoanNhan.Value, gd.SoTien);
            }
            return true;
        }

        public bool Xoa(GiaoDichDTO gd)
        {
            if (_gdDAL.Xoa(gd.MaGiaoDich) == 0) return false;
            if (gd.LoaiGiaoDich == "Thu")
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, -gd.SoTien);
            else if (gd.LoaiGiaoDich == "Chi")
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, gd.SoTien);
            else if (gd.LoaiGiaoDich == "ChuyenTien" && gd.MaTaiKhoanNhan.HasValue)
            {
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoan, gd.SoTien);
                _tkDAL.CapNhatSoDu(gd.MaTaiKhoanNhan.Value, -gd.SoTien);
            }
            return true;
        }

        // ─── HÀM "PHIÊN DỊCH" CHO FORM GIAO DỊCH ───
        public List<GiaoDichDTO> LayDanhSachGiaoDich()
        {
            // Tạm thời gán mã người dùng = 1 
            int maNguoiDung = 1;
            return GetAll(maNguoiDung);
        }
        public bool Sua(GiaoDichDTO gd)
        {
            // Gọi hàm Sua() từ DAL vừa tạo ở trên
            return _gdDAL.Sua(gd) > 0;
        }
    }
}