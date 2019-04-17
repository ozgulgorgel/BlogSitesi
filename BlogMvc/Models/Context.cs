using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvc.Models
{
    public class Context:DbContext
    {
        public DbSet<Makale> Makaleler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Etiket> Etiketler { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<MakaleEtiket> MakaleEtiketler { get; set; }

    }
}