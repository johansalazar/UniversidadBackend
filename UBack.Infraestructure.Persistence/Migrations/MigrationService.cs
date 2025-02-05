using Microsoft.EntityFrameworkCore.Migrations;
using UBack.Infraestructure.Persistence.Contexts;

namespace UBack.Infraestructure.Persistence.Migrations
{
    public class MigrationService
    {
        private readonly UniversidadDbContext _context;
        private readonly IMigrator _migrator;

        public MigrationService(UniversidadDbContext context, IMigrator migrator)
        {
            _context = context;
            _migrator = migrator;
        }

        public async Task MigrateDatabaseAsync()
        {
            try
            {
                // Ejecutar migraciones si hay cambios pendientes
                await _migrator.MigrateAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores (si es necesario)
                Console.WriteLine($"Error al ejecutar migraciones: {ex.Message}");
            }
        }
    }
}