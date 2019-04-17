using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Yetki
    {
        [Key]
        public int YetkiID { get; set; }
        public string YetkiAdı { get; set; }
        public virtual ICollection<Uye> Uyeler { get; set; }
    }
}