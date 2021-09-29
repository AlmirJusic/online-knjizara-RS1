using Microsoft.EntityFrameworkCore;
using online_knjizara.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.EF
{
    public class OnlineKnjizaraDbContext:DbContext
    {
        public OnlineKnjizaraDbContext(DbContextOptions<OnlineKnjizaraDbContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Izdavac> Izdavac { get; set; }
        public DbSet<Knjiga> Knjiga { get; set; }
        public DbSet<KomentarOcjena> KomentarOcjena { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Odlomak> Odlomak { get; set; }
        public DbSet<Skladiste> Skladiste { get; set; }
        public DbSet<Stanje> Stanje { get; set; }
        public DbSet<Zanr> Zanr { get; set; }
        public DbSet<AutorizacijskiToken> AutorizacijskiToken { get; set; }
        public DbSet<CitaocKorisnik> CitaocKorisnik { get; set; }
        public DbSet<KorisnickiNalozi> KorisnickiNalozi { get; set; }
        public DbSet<Zaposlenici> Zaposlenici { get; set; }
        public DbSet<Obavijesti> Obavijesti { get; set; }

    }
}
