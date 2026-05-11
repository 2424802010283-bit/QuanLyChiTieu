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
    public class MucTieuBLL
    {
        private readonly MucTieuDAL _dal = new MucTieuDAL();

        public List<MucTieuDTO> LayDanhSach(int maNguoiDung)
        {
            // 1. Gọi DAL để lấy DataTable (Giả sử hàm trong DAL của bạn tên là GetMucTieu)
            DataTable dt = _dal.GetMucTieu(maNguoiDung);
            List<MucTieuDTO> ds = new List<MucTieuDTO>();

            // 2. Duyệt từng dòng trong DataTable để nạp vào List DTO
            foreach (DataRow row in dt.Rows)
            {
                MucTieuDTO mt = new MucTieuDTO
                {
                    MaMucTieu = Convert.ToInt32(row["MaMucTieu"]),
                    TenMucTieu = row["TenMucTieu"].ToString(),
                    SoTienCanDat = Convert.ToDecimal(row["SoTienCanDat"]),
                    SoTienHienCo = Convert.ToDecimal(row["SoTienHienCo"]),
                    HanChot = Convert.ToDateTime(row["HanChot"]),
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"])
                };
                ds.Add(mt);
            }
            return ds;
        }

        public bool Them(MucTieuDTO mt)
        {
            if (string.IsNullOrEmpty(mt.TenMucTieu) || mt.SoTienCanDat <= 0) return false;
            // Bây giờ DAL.Them đã nhận mt, nên sẽ không còn lỗi tham số
            return _dal.Them(mt) > 0;
        }

        public bool Xoa(int maMucTieu)
        {
            return _dal.Xoa(maMucTieu) > 0;
        }
    
}
    }