using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class MakaleEtiket
    {
        [Key]
        public int MakaleEtiketID { get; set; }
        public int MakaleID { get; set; }
        public virtual Makale Makale { get; set; }
        public int EtiketID { get; set; }
        public virtual Etiket Etiket { get; set; }
    }
}