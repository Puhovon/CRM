using Microsoft.EntityFrameworkCore;
using WebApplication2.DTO;

namespace WebApplication2.Contexts;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Factory> Factories { get; set; }

    public virtual DbSet<Tank> Tanks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test;Username=postgres;Password=Astra140304");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("factories_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('factories_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Production)
                .HasMaxLength(60)
                .HasColumnName("production");
        });

        modelBuilder.Entity<Tank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tanks_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('tanks_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
