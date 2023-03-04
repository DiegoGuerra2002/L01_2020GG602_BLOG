using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020GG602.Models
{
    public class blogContext : DbContext 
    {
        public blogContext(DbContextOptions<blogContext> options) : base(options) {
        
        }
    
        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
    
    }
}
