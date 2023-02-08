namespace DoAnQuanlyNS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuongPhat")]
    public partial class ThuongPhat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuongPhat()
        {
            Luongs = new HashSet<Luong>();
        }

        [Key]
        [StringLength(10)]
        public string MaQuyetDinh { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        public decimal SoTienThuong { get; set; }

        public decimal SoTienPhat { get; set; }

        [Required]
        [StringLength(200)]
        public string LiDo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Luong> Luongs { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
