using TpApi.Model;
using Microsoft.EntityFrameworkCore;

namespace TpApi.DataAccess
{
    public class APIDBContext : DbContext
    {
        private string? v;

        DbSet<Proveedor> Proveedor { get; set; }
        DbSet<Producto> Producto { get; set; }

        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options)
        {
            
        }

        public APIDBContext(string? v)
        {
            this.v = v;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
