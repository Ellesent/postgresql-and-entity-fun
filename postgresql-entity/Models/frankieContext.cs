using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace postgresql_entity.Models
{
    public partial class frankieContext : DbContext
    {
        public frankieContext()
        {
        }

        public frankieContext(DbContextOptions<frankieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestTable> TestTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=178.128.12.198;Database=frankie;Username=frankie;Password=Mable22");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.ToTable("testTable", "testTable");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"testTable\".\"testTable_id_seq\"'::regclass)");

                entity.Property(e => e.TestColumn)
                    .HasColumnName("testColumn")
                    .HasColumnType("char");
            });

            modelBuilder.HasSequence<int>("testTable_id_seq");
        }
    }
}
