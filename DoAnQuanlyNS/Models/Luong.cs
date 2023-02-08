namespace DoAnQuanlyNS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luong")]
    public partial class Luong
    {
        [Key]
        [StringLength(10)]
        public string MaLuong { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChamCong { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQuyetDinh { get; set; }

        public decimal LuongCB { get; set; }

        public double HeSoLuong { get; set; }

        public virtual ChamCong ChamCong { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual ThuongPhat ThuongPhat { get; set; }
    }
}
