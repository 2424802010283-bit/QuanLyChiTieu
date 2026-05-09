using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyChiTieu.DAL;
using QuanLyChiTieu.DTO;
using System.Data;
using System.Configuration; // Đừng quên dòng này nhé!

// Lấy chuỗi từ App.config
namespace QuanLyChiTieu.BLL
{
    internal class DanhMucBLL
    {
        DanhMucDAL dal = new DanhMucDAL();

        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            return dal.LayDanhSachDanhMuc();
        }
        public bool LuuDanhMuc(DanhMucDTO dm)
        {
            return dal.ThemDanhMuc(dm);
        }

    }

}
