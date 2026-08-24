using IronGridC2Consumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Consumer.Data
{
    public class IronGridC2DbContext : DbContext
    {
        public IronGridC2DbContext(DbContextOptions<IronGridC2DbContext> options) : base(options)
        {

        }
        public DbSet<AssetLiveStatus> AssetLiveStatus { get; set; } = null!;
        public DbSet<Assets> Assets { get; set; } = null!;
        public DbSet<Units> Units { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Assets>()
                .HasOne(k => k.units)
                .WithMany(k => k.assets)
                .HasForeignKey(k => k.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AssetLiveStatus>()
                .HasOne(k => k.assets)
                .WithOne(k => k.assetLiveStatus)
                .HasForeignKey<AssetLiveStatus>(k => k.AssetId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Units>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Assets>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<AssetLiveStatus>()
                .HasKey(k => k.AssetId);
        }
    }


}
