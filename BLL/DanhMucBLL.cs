using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System.Data;
using System.Configuration; // Đừng quên dòng này nhé!


namespace QuanLyChiTieu.BLL
{
    public class DanhMucBLL
    {
        private readonly DanhMucDAL _dal = new DanhMucDAL();

        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            return _dal.LayDanhSachDanhMuc();
        }

        public bool LuuDanhMuc(DanhMucDTO dm)
        {
            return _dal.ThemDanhMuc(dm);
        }

        public List<string> LayTenTatCaDanhMuc()
        {
            return _dal.LayTenTatCaDanhMuc();
        }

    }
}
