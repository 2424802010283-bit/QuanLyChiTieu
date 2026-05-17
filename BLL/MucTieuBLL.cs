using System;
using System.Collections.Generic;
using System.Data;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu.BLL
{
    public class MucTieuBLL
    {
        private readonly MucTieuDAL _dal = new MucTieuDAL();

        // 1. Chuyển đổi dữ liệu DataTable từ DAL sang List DTO cho Form giao diện dễ đọc
        public List<MucTieuDTO> LayDanhSach(int maNguoiDung)
        {
            DataTable dt = _dal.GetMucTieu(maNguoiDung);
            List<MucTieuDTO> ds = new List<MucTieuDTO>();

            foreach (DataRow row in dt.Rows)
            {
                MucTieuDTO mt = new MucTieuDTO
                {
                    MaMucTieu = Convert.ToInt32(row["MaMucTieu"]),
                    TenMucTieu = row["TenMucTieu"].ToString(),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),

                    // 👉 Gán đúng tên cột trong Database SQL sang biến DTO của C#
                    SoTienCanDat = Convert.ToDecimal(row["SoTienMucTieu"]),
                    SoTienHienCo = Convert.ToDecimal(row["SoTienDaTichLuy"]),
                    HanChot = Convert.ToDateTime(row["HanHoanThanh"])
                };
                ds.Add(mt);
            }
            return ds;
        }

        // 2. Kiểm tra logic trước khi Thêm
        public bool Them(MucTieuDTO mt)
        {
            if (string.IsNullOrEmpty(mt.TenMucTieu) || mt.SoTienCanDat <= 0)
                return false;

            return _dal.Them(mt) > 0;
        }

        // 3. Kiểm tra logic trước khi Sửa
        public bool Sua(MucTieuDTO mt)
        {
            if (string.IsNullOrEmpty(mt.TenMucTieu) || mt.SoTienCanDat <= 0)
                return false;

            return _dal.Sua(mt) > 0;
        }

        // 4. Gọi lệnh Xóa
        public bool Xoa(int maMucTieu)
        {
            return _dal.Xoa(maMucTieu) > 0;
        }
    }
}