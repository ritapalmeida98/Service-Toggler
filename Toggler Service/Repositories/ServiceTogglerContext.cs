using Microsoft.EntityFrameworkCore;
using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public class ServiceTogglerContext : DbContext
    {
        public ServiceTogglerContext(DbContextOptions<ServiceTogglerContext> options)
               : base(options)
        { }

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Toggle> Toggles { get; set; }
        public virtual DbSet<ToggleService> ToggleServices { get; set; }
    }
}
