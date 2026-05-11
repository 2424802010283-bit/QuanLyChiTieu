using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyChiTieu.DAL;
namespace QuanLyChiTieu.BLL
{
    public static class Session
    {
        public static NguoiDungDTO CurrentUser { get; set; }
        public static int MaNguoiDung => CurrentUser?.MaNguoiDung ?? 0;
    }
    
}
