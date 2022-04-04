using Toggler_Service.DTOs;
using Toggler_Service.Models;
using Toggler_Service.Repositories;
using Toggler_Service.Validators;

namespace Toggler_Service.Services
{
    public class ServiceService : IServiceService
    {
        public IServiceRepository _repository;
        public IToggleServiceRepository _toggleServiceRepository;

        public ServiceService(IServiceRepository repository, IToggleServiceRepository toggleServiceRepository)
        {
            _repository = repository;
            _toggleServiceRepository = toggleServiceRepository;
        }

        public IEnumerable<ServiceDTO> GetAll()
        {
            var list = _repository.GetAll();
            var dtoList = new List<ServiceDTO>();

            foreach (var item in list)
            {
                dtoList.Add(new ServiceDTO
                {
                    Identifier = item.Identifier,
                    Version = item.Version.ToString(),
                });
            }

            return dtoList;
        }

        private bool ServiceExists(string identifier, string version)
        {
            return _repository.GetByIdentifierAndVersion(identifier, version) != null;
        }

        public ApiResponseDTO Register(ServiceDTO serviceDTO)
        {
            var res = new ApiResponseDTO { IsSuccess = false };

            if (!ServiceValidator.ValidateService(serviceDTO))
            {
                res.ErrorMessage = "Service is invalid.";
                return res;
            }

            if (ServiceExists(serviceDTO.Identifier, serviceDTO.Version))
            {
                res.ErrorMessage = "Service already exists.";
                return res;
            }

            var service = new Service
            {
                Identifier = serviceDTO.Identifier,
                Version = serviceDTO.Version
            };

            try
            {
                _repository.Add(service);
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.ToString();
                return res;
            }

            res.IsSuccess = true;
            return res;
        }

        public ApiResponseDTO RegisterMany(List<ServiceDTO> list)
        {
            var res = new ApiResponseDTO { IsSuccess = false };
            var error = 0;

            foreach (var item in list)
            {
                var register = Register(item);
                if (!register.IsSuccess)
                {
                    error++;
                }
            }

            if(error > 0)
            {
                if(error == list.Count)
                {
                    res.IsSuccess = false;
                    res.ErrorMessage = "None of the services were created.";
                }
                else
                {
                    res.IsSuccess = true;
                    res.ErrorMessage = "Some of the services were not created.";
                }
            }
            else
            {

                res.IsSuccess = true;
            }

            return res;
        }

        public ServiceDTO Get(string identifier, string version)
        {
            var res = _repository.GetByIdentifierAndVersion(identifier, version);

            if(res == null)
            {
                return null;
            }

            var service = new ServiceDTO
            {
                Identifier = identifier,
                Version = version
            };

            return service;
        }

        public List<GetTogglesByServiceDTO> GetToggles(string identifier, string version)
        {
            var service = _repository.GetByIdentifierAndVersion(identifier, version);

            if (service != null)
            {
                var res = _toggleServiceRepository.GetAllByService(service.Id);

                if (res != null)
                {
                    var toggles = new List<GetTogglesByServiceDTO>();
                    foreach (var item in res)
                    {
                        toggles.Add(new GetTogglesByServiceDTO { Toggle = item.Toggle.Name, Value = item.Value.Value });
                    }

                    return toggles;
                }
            }
            
            return null;
        }

        public List<Service> GetByVersion(string version)
        {
            return _repository.GetByVersion(version);
        }
    }
}
