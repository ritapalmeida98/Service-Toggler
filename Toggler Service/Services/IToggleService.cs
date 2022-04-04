using Toggler_Service.DTOs;

namespace Toggler_Service.Services
{
    public interface IToggleService
    {
        ApiResponseDTO Register(ToggleDTO dto);
        ApiResponseDTO Bulk(ToggleBatchCreationDTO dto);
        ApiResponseDTO AddServices(string name, ToggleServiceDTO dto);
    }
}
