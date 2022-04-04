namespace Toggler_Service.DTOs
{
    public class ToggleBatchCreationDTO
    {
        public List<ToggleNameDTO> Toggles { get; set; }
        public string Version { get; set; }
        public bool Value { get; set; }
    }
}
