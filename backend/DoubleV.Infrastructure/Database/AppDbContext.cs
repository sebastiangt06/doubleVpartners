using Microsoft.EntityFrameworkCore;
using DoubleV.Domain.Entities;
using DoubleV.Domain.Entities.Users;

namespace DoubleV.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                // Tabla creada por script -> sin migraciones
                entity.ToTable("Personas", tb => tb.ExcludeFromMigrations());
                entity.HasKey(e => e.Id);

                // Propiedades en inglés -> columnas en español
                entity.Property(e => e.Name)
                      .HasColumnName("Nombres")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasColumnName("Apellidos")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Identification)
                      .HasColumnName("NumeroIdentificacion")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.IdentificationType)
                      .HasColumnName("TipoIdentificacion")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnName("Email")
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("FechaCreacion")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                // Columnas calculadas ya existen en DB: solo mapéalas
                entity.Property(e => e.FullIdentification)
                      .HasColumnName("IdentificacionCompleta")
                      .ValueGeneratedOnAddOrUpdate();

                // OJO: aquí asumo que tu entidad Person tiene esta propiedad:
                // public string FullName { get; set; }
                entity.Property(e => e.FullName)
                      .HasColumnName("NombreCompleto")
                      .ValueGeneratedOnAddOrUpdate();

                // Índices: opcional (ya existen por script, puedes omitirlos)
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => new { e.IdentificationType, e.Identification }).IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Usuario", tb => tb.ExcludeFromMigrations());
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PersonId)
                      .HasColumnName("PersonaId")
                      .IsRequired();

                entity.Property(e => e.UserName)
                      .HasColumnName("Usuario")
                      .HasMaxLength(60)
                      .IsRequired();

                entity.Property(e => e.PassHash)
                      .HasColumnName("PassHash")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("FechaCreacion")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasIndex(e => e.UserName).IsUnique();

                entity.HasOne(e => e.Person)
                      .WithMany() // si Person tiene ICollection<User> Users => .WithMany(p => p.Users)
                      .HasForeignKey(e => e.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Si no tienes IEntityTypeConfiguration<>, puedes quitar esto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
