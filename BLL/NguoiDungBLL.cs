using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.BLL
{
    public class NguoiDungBLL
    {
        NguoiDungDAL dal = new NguoiDungDAL();
        public NguoiDungDTO DangNhap(string user, string pass)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass)) return null;
            return dal.LayThongTinNguoiDung(user, pass);
        }
        public bool DangKy(NguoiDungDTO nd)
        {
            return dal.DangKy(nd);
        }
    }
    
}
