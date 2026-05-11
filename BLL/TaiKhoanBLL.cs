using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;


using System.Data;
namespace QuanLyChiTieu.BLL
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _dal = new TaiKhoanDAL();
        public List<TaiKhoanDTO> GetByUser(int maNguoiDung) => _dal.GetByUser(maNguoiDung);
        public bool Them(TaiKhoanDTO tk) => _dal.Them(tk) > 0;
        public bool Sua(TaiKhoanDTO tk) => _dal.Sua(tk) > 0;
        public bool Xoa(int maTaiKhoan) => _dal.Xoa(maTaiKhoan) > 0;
        public decimal GetTongSoDu(int maNguoiDung) => _dal.GetTongSoDu(maNguoiDung);
    }
}
