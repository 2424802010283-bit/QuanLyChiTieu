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
        public DataTable GetAll(int maNguoiDung) => _dal.GetAll(maNguoiDung);
        public bool Them(int maNguoiDung, string ten, decimal mucTieu, DateTime? han)
            => _dal.Them(maNguoiDung, ten, mucTieu, han) > 0;
        public bool CapNhatTichLuy(int maMucTieu, decimal soTienThem)
            => _dal.CapNhatTichLuy(maMucTieu, soTienThem) > 0;
        public bool HoanThanh(int maMucTieu)
            => _dal.CapNhatTrangThai(maMucTieu, "Hoàn thành") > 0;
        public bool Xoa(int maMucTieu) => _dal.Xoa(maMucTieu) > 0;
    
}
}