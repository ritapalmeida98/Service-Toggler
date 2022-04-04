using Toggler_Service.DTOs;
using Toggler_Service.Models;

namespace Toggler_Service.Services
{
    public interface IToggleServiceService
    {
        ApiResponseDTO Register(Toggle toggle, ToggleServiceDTO dto);
        ApiResponseDTO Register(Toggle toggle, Service service, bool value);
    }
}
