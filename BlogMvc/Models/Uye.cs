using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Uye
    {
        [Key]
        public int UyeID { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adınızı giriniz")]
      
        public string KullanıcıAdı { get; set; }

       [Required(ErrorMessage = "Email Adresinizi Giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre alanını doldurunuz")]
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string Foto { get; set; }
        public int? YetkiID { get; set; }
        public virtual Yetki Yetki { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Makale> Makaleler { get; set; }
    }
}