using DavidCardenasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DavidCardenasAPI.Context
{
    public class HalterofiliaContext : DbContext
    {
        public HalterofiliaContext(DbContextOptions<HalterofiliaContext> options) : base(options) { }
        public DbSet<Deportista> Deportistas { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
