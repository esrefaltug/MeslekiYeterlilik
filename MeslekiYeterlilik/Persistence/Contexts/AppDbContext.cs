using Microsoft.EntityFrameworkCore;
using MeslekiYeterlilik.Domain.Models;


namespace MeslekiYeterlilik.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<BelgeTipi> BelgeTipleri { get; set; }
        public DbSet<Belge> Belgeler { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BelgeTipi>().ToTable("Categories");
                       
            builder.Entity<BelgeTipi>().Property(p => p.sReferansKodu).IsRequired();
            builder.Entity<BelgeTipi>().Property(p => p.sBelgeAdi).IsRequired();
            builder.Entity<BelgeTipi>().Property(p => p.dYayimlanmaTarihi).IsRequired();
            builder.Entity<BelgeTipi>().Property(p => p.nRevizyonNumarasi);
            builder.Entity<BelgeTipi>().Property(p => p.dRevizyonTarihi).IsRequired();
            //builder.Entity<BelgeTipi>().HasMany(p => p.Belgeler).WithOne(p => p.BelgeTipi);

            //builder.Entity<BelgeTipi>().HasKey(p => p.Id);
            //builder.Entity<BelgeTipi>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<BelgeTipi>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            //builder.Entity<BelgeTipi>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
            //builder.Entity<BelgeTipi>().HasKey(p => p.pBelgeler_rowid);
            //builder.Entity<Belge>().ToTable("Products");
            //builder.Entity<Belge>().HasKey(p => p.Id);
            //builder.Entity<Belge>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<Belge>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            //builder.Entity<Belge>().Property(p => p.QuantityInPackage).IsRequired();
            //builder.Entity<Belge>().Property(p => p.UnitOfMeasurement).IsRequired();

        }
    }
}