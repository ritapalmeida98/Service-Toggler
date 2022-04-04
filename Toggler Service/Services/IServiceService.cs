using Toggler_Service.DTOs;
using Toggler_Service.Models;

namespace Toggler_Service.Services
{
    public interface IServiceService
    {
        ApiResponseDTO Register(ServiceDTO service);
        ApiResponseDTO RegisterMany(List<ServiceDTO> service);
        IEnumerable<ServiceDTO> GetAll();
        ServiceDTO Get(string identifier, string version);
        List<Service> GetByVersion(string version);
        List<GetTogglesByServiceDTO> GetToggles(string identifier, string version);
    }
}
