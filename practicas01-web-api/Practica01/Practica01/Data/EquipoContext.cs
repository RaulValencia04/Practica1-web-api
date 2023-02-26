using Microsoft.EntityFrameworkCore;
using Practica01.Models;

namespace Practica01.Data
{
    public class EquipoContext : DbContext
    {

        public EquipoContext(DbContextOptions<EquipoContext> options ) : base(options) {
            

        }

        public DbSet<Equipo> equipos { get; set; }

    }
}
