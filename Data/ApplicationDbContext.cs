using MarzanStored.Dtos;
using MarzanStored.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarzanStored.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<OrdenesDto> OrdenesDto { get; set; }
    }
}
