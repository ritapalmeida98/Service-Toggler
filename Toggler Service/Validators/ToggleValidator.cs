using Toggler_Service.DTOs;

namespace Toggler_Service.Validators
{
    public static class ToggleValidator
    {
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public static bool ValidateToggle(ToggleDTO toggle)
        {
            return ValidateName(toggle.Name);
        }

        public static bool ValidateToggleService(ToggleServiceDTO dto)
        {
            if (dto == null || dto.Inclusions == null || dto.Exclusions == null)
            {
                return false;
            }

            if(dto.IncludeAllServices && (dto.Inclusions.Any() || dto.Exclusions.Any()))
            {
                return false;
            }

            if(dto.Inclusions.Any() && dto.Exclusions.Any())
            {
                return false;
            }

            return true;
        }
    }
}
