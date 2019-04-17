using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Etiket
    {
        [Key]
        public int EtiketID { get; set; }
        public string EtiketAdı { get; set; }
        public virtual ICollection<MakaleEtiket> MakaleEtiketler { get; set; }
    }
}