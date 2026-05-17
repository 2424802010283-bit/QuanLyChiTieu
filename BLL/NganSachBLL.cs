using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System.Collections.Generic;
using System.Data;

namespace QuanLyChiTieu.BLL
{
    public class NganSachBLL
    {
        private readonly NganSachDAL _dal = new NganSachDAL();

        public DataTable GetTheoThang(int maNguoiDung, int thang, int nam)
            => _dal.GetTheoThang(maNguoiDung, thang, nam);

        public (decimal tongNganSach, decimal tongDaChi) GetTong(int maNguoiDung, int thang, int nam)
            => _dal.GetTong(maNguoiDung, thang, nam);

        public bool Xoa(int maNganSach) => _dal.Xoa(maNganSach) > 0;

        // CÁC HÀM MỚI BỔ SUNG:
        public List<NganSachDTO> GetAll(int maNguoiDung) => _dal.GetAll(maNguoiDung);

        public bool Them(NganSachDTO ns) => _dal.Them(ns) > 0;

        public bool Sua(NganSachDTO ns) => _dal.Sua(ns) > 0;
    }
}