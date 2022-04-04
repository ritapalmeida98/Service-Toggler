using Toggler_Service.DTOs;
using Toggler_Service.Models;
using Toggler_Service.Repositories;
using Toggler_Service.Validators;

namespace Toggler_Service.Services
{
    public class ToggleService : IToggleService
    {
        public IToggleRepository _repository;
        public IServiceService _serviceService;
        public IToggleServiceService _toggleServiceService;

        public ToggleService(IToggleRepository repository, IServiceService serviceService, IToggleServiceService toggleServiceService)
        {
            _repository = repository;
            _serviceService = serviceService;
            _toggleServiceService = toggleServiceService;
        }

        public ApiResponseDTO AddServices(string name, ToggleServiceDTO dto)
        {
            var res = new ApiResponseDTO { IsSuccess = false };
            var toggle = _repository.GetByName(name);

            if (toggle == null)
            {
                res.ErrorMessage = "Toggle doesn't exist.";
                return res;
            }

            if (!ToggleValidator.ValidateToggleService(dto))
            {
                res.ErrorMessage = "Toggle Service is invalid.";
                return res;
            }

            var toggleServiceRegister = _toggleServiceService.Register(toggle, dto);
            if (!toggleServiceRegister.IsSuccess)
            {
                return toggleServiceRegister;
            }

            res.IsSuccess = true;
            return res;
        }

        public bool ToggleExists(string name)
        {
            return _repository.GetByName(name) != null;
        }

        public ApiResponseDTO Register(ToggleDTO dto)
        {
            var res = new ApiResponseDTO { IsSuccess = false };

            if (!ToggleValidator.ValidateToggle(dto))
            {
                res.ErrorMessage = "Toggle is invalid.";
                return res;
            }

            if (ToggleExists(dto.Name))
            {
                res.ErrorMessage = "Toggle already exists.";
                return res;
            }

            var toggle = new Toggle
            {
                Name = dto.Name
            };

            try
            {
                _repository.Add(toggle);
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.ToString();
                return res;
            }

            res.IsSuccess = true;
            return res;
        }

        public ApiResponseDTO Bulk(ToggleBatchCreationDTO dto)
        {
            var res = new ApiResponseDTO { IsSuccess = false };

            var errorToggles = 0;

            if (!ServiceValidator.ValidateVersion(dto.Version))
            {
                res.ErrorMessage = "Version is invalid.";
                return res;
            }

            var services = _serviceService.GetByVersion(dto.Version);

            if (services == null || !services.Any())
            {
                res.ErrorMessage = "Services with given version don't exist.";
                return res;
            }

            foreach (var toggleDto in dto.Toggles)
            {
                if (ToggleValidator.ValidateName(toggleDto.Name))
                {
                    var toggle = _repository.GetByName(toggleDto.Name);
                    if (toggle == null)
                    {
                        toggle = new Toggle
                        {
                            Name = toggleDto.Name
                        };

                        try
                        {
                            _repository.Add(toggle);
                        }
                        catch (Exception ex)
                        {
                            res.ErrorMessage = ex.ToString();
                            return res;
                        }
                    }

                    foreach (var service in services)
                    {
                        var toggleServiceRegister = _toggleServiceService.Register(toggle, service, dto.Value);
                        if (!toggleServiceRegister.IsSuccess)
                        {
                            res.ErrorMessage = toggleServiceRegister.ErrorMessage;
                        }
                    }
                }
                else
                {
                    errorToggles++;
                }
            }

            if(errorToggles > 0)
            {
                if(errorToggles == dto.Toggles.Count)
                {
                    res.IsSuccess = false;
                    res.ErrorMessage = "None of the toggle services were created.";
                }
                else
                {
                    res.IsSuccess = true;
                    res.ErrorMessage = "Some of the toggle services were not created.";
                }
            }

            res.IsSuccess = true;
            return res;
        }
    }
}
