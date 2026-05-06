using System;
using System.Data;

namespace QuanLyChiTieu.DTO
{
    public class GiaoDichDTO
    {
        public int MaGD { get; set; }
        public DateTime NgayGD { get; set; }
        public decimal SoTien { get; set; }
        public string GhiChu { get; set; }
        public int MaDanhMuc { get; set; }

        public GiaoDichDTO() { }

        // Kỹ thuật Mapping chuyên nghiệp từ DataRow sang Object
        public GiaoDichDTO(DataRow row)
        {
            this.MaGD = (int)row["MaGD"];
            this.NgayGD = (DateTime)row["NgayGD"];
            this.SoTien = Convert.ToDecimal(row["SoTien"]);
            this.GhiChu = row["GhiChu"].ToString();
            this.MaDanhMuc = (int)row["MaDanhMuc"];
        }
    }
}