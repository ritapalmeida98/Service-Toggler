using Toggler_Service.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Toggler_Service.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceTogglerContext _context;

        public ServiceRepository(ServiceTogglerContext context)
        {
            _context = context;
        }

        public Service Add(Service service)
        {
            _context.Set<Service>().Add(service);
            _context.SaveChanges();
            return service;
        }

        public List<Service> GetAll()
        {
            return _context.Set<Service>().ToList();
        }

        public Service GetByIdentifierAndVersion(string identifier, string version)
        {
            return _context.Set<Service>().FirstOrDefault(x => x.Identifier == identifier && x.Version == version);
        }

        public List<Service> GetByVersion(string version)
        {
            return _context.Set<Service>().Where(x => x.Version == version).ToList();
        }
    }
}
