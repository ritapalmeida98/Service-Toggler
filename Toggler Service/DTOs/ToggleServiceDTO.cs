namespace Toggler_Service.DTOs
{
    public class ToggleServiceDTO
    {
        public bool Value { get; set; }
        public bool IncludeAllServices { get; set; }   
        public List<ServiceVersionDTO> Inclusions {get;set;}
        public List<ServiceVersionDTO> Exclusions {get;set;}
}
}
