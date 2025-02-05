using Microsoft.EntityFrameworkCore;
using UBack.Domain.Dominio.Entities;

namespace UBack.Infraestructure.Persistence.Contexts
{
    /// <summary>
    /// Contexto principal para la base de datos de IoT, maneja la persistencia de las entidades 
    /// del dominio como usuarios, dispositivos IoT, servidores, ubicaciones y configuraciones MQTT.
    /// </summary>
    public class UniversidadDbContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto que recibe las opciones de configuración de DbContext.
        /// </summary>
        /// <param name="options">Opciones de configuración para el DbContext</param>
        public UniversidadDbContext(DbContextOptions<UniversidadDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relación Usuario - Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            // Un profesor solo puede estar asignado a una materia si su rol es Profesor
            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Profesor)
                .WithMany()
                .HasForeignKey(m => m.IdProfesor)
                .OnDelete(DeleteBehavior.Restrict);

            // Un estudiante solo puede inscribirse si su rol es Estudiante
            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Estudiante)
                .WithMany()
                .HasForeignKey(i => i.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
