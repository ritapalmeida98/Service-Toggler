using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public interface IServiceRepository
    {
        Service Add (Service service);
        Service GetByIdentifierAndVersion(string identifier, string version);
        List<Service> GetByVersion(string version);
        List<Service> GetAll();
    }
}
