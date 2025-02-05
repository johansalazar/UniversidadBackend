using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UBack.Infraestructure.Persistence.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UniversidadDbContext>
    {
        public UniversidadDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversidadDbContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");  // Reemplaza con tu cadena de conexión

            return new UniversidadDbContext(optionsBuilder.Options);
        }
    }
}
