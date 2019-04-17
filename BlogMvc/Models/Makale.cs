using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Makale
    {
          [Key]
        public int MakaleID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Foto { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }
        public int? Okunma { get; set; }
        public int? KategoriID { get; set; }

        public virtual Kategori Kategori { get; set; }
        public int? UyeID { get; set; }
        public virtual Uye Uye { get; set; }
        public virtual ICollection<MakaleEtiket> MakaleEtiketler { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        
    }
}