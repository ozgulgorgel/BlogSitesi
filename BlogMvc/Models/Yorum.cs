using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Yorum
    {
        [Key]
        public int YorumID { get; set; }
        public string Icerik { get; set; }
        public int? UyeID { get; set; }
        public virtual Uye Uye { get; set; }
        public int MakaleID { get; set; }
        public virtual Makale Makale { get; set; }
        public DateTime Tarih { get; set; }
    }
}