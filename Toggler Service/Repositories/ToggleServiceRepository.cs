using Microsoft.EntityFrameworkCore;
using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public class ToggleServiceRepository : IToggleServiceRepository
    {
        private readonly ServiceTogglerContext _context;

        public ToggleServiceRepository(ServiceTogglerContext context)
        {
            _context = context;
        }

        public void Add(ToggleService toggleService)
        {
            _context.Set<ToggleService>().Add(toggleService);
        }

        public void Update(ToggleService toggleService)
        {
            _context.Set<ToggleService>().Update(toggleService);
        }

        public void Remove(ToggleService toggleService)
        {
            _context.Set<ToggleService>().Remove(toggleService);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<ToggleService> GetAllByService(long serviceId)
        {
            return _context.Set<ToggleService>().Where(x => x.ServiceId == serviceId).Include(x => x.Toggle).Include(x => x.Service).ToList();
        }

        public ToggleService GetByToggleAndService(long toggleId, long serviceId)
        {
            return _context.Set<ToggleService>().Where(x => x.ServiceId == serviceId && x.ToggleId == toggleId).FirstOrDefault();
        }
    }
}
