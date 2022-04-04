using Toggler_Service.DTOs;
using Toggler_Service.Models;
using Toggler_Service.Repositories;

namespace Toggler_Service.Services
{
    public class ToggleServiceService : IToggleServiceService
    {
        public IToggleRepository _toggleRepository;
        public IServiceRepository _serviceRepository;
        public IToggleServiceRepository _repository;

        public ToggleServiceService(IToggleRepository toggleRepository, IServiceRepository serviceRepository, IToggleServiceRepository repository)
        {
            _repository = repository;
            _toggleRepository = toggleRepository;
            _serviceRepository = serviceRepository;
        }

        public ApiResponseDTO Register(Toggle toggle, Service service, bool value)
        {
            var res = AddOrUpdateToggleService(toggle, service, value);
            if (res.IsSuccess)
            {
                try
                {
                    _repository.SaveChanges();
                    res.IsSuccess = true;
                    return res;
                }
                catch (Exception ex)
                {
                    res.ErrorMessage = ex.ToString();
                    return res;
                }
            }
            return res;
        }

        public ApiResponseDTO Register(Toggle toggle, ToggleServiceDTO dto)
        {
            var res = new ApiResponseDTO { IsSuccess = false };
            if (dto.IncludeAllServices)
            {
                var allServices = _serviceRepository.GetAll();
                foreach (var service in allServices)
                {
                    var addOrUpdate = AddOrUpdateToggleService(toggle, service, dto.Value);
                    if (!addOrUpdate.IsSuccess) return addOrUpdate;
                }
            }
            else
            {
                if (dto.Inclusions != null && dto.Inclusions.Any())
                {
                    foreach (var inclusion in dto.Inclusions)
                    {
                        var service = _serviceRepository.GetByIdentifierAndVersion(inclusion.Identifier, inclusion.Version);
                        if (service != null)
                        {
                            var addOrUpdate = AddOrUpdateToggleService(toggle, service, dto.Value);
                            if (!addOrUpdate.IsSuccess) return addOrUpdate;
                        }
                    }
                }

                if (dto.Exclusions != null && dto.Exclusions.Any())
                {
                    var allServices = _serviceRepository.GetAll();
                    var includeServices = new List<Service>();

                    foreach (var exclusion in dto.Exclusions)
                    {
                        var service = _serviceRepository.GetByIdentifierAndVersion(exclusion.Identifier, exclusion.Version);
                        if (service != null)
                        {
                            var toggleService = _repository.GetByToggleAndService(toggle.Id, service.Id);
                            if (toggleService != null)
                            {
                                _repository.Remove(toggleService);
                            }
                        }

                        foreach (var existingService in allServices)
                        {
                            if (exclusion.Identifier != existingService.Identifier || exclusion.Version != existingService.Version)
                            {
                                includeServices.Add(existingService);
                            }
                        }
                    }

                    foreach (var service in includeServices)
                    {
                        var addOrUpdate = AddOrUpdateToggleService(toggle, service, dto.Value);
                        if (!addOrUpdate.IsSuccess) return addOrUpdate;
                    }
                }
            }

            try
            {
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.ToString();
                return res;
            }
            
            res.IsSuccess = true;
            return res;
        }

        private ApiResponseDTO AddOrUpdateToggleService(Toggle toggle, Service service, bool value)
        {
            var res = new ApiResponseDTO { IsSuccess = false };

            var toggleService = _repository.GetByToggleAndService(toggle.Id, service.Id);
            if (toggleService != null && toggleService.Value != value)
            {
                toggleService.Value = value;

                try
                {
                    _repository.Update(toggleService);
                }
                catch (Exception ex)
                {
                    res.ErrorMessage = ex.ToString();
                    return res;
                }
            }
            else if (toggleService == null)
            {
                toggleService = new Models.ToggleService
                {
                    ToggleId = toggle.Id,
                    ServiceId = service.Id,
                    Value = value
                };

                try
                {
                    _repository.Add(toggleService);
                }
                catch (Exception ex)
                {
                    res.ErrorMessage = ex.ToString();
                    return res;
                }
            }

            res.IsSuccess = true;
            return res;
        }
    }
}
