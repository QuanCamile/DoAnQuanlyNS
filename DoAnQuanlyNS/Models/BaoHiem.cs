namespace DoAnQuanlyNS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaoHiem")]
    public partial class BaoHiem
    {
        [Key]
        [StringLength(10)]
        public string MaBaoHiem { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiBaoHiem { get; set; }

        public DateTime NgayHetHan { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
