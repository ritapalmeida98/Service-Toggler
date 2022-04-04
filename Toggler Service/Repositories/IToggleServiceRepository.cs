using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public interface IToggleServiceRepository
    {
        void Add(ToggleService toggleService);
        void Update(ToggleService toggleService);
        void Remove(ToggleService toggleService);
        void SaveChanges();
        List<ToggleService> GetAllByService(long serviceId);
        ToggleService GetByToggleAndService(long toggleId, long serviceId);
    }
}
