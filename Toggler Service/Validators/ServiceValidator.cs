using Toggler_Service.DTOs;

namespace Toggler_Service.Validators
{
    public static class ServiceValidator
    {
        private static bool ValidateIdentifier(string identifier)
        {
            return !string.IsNullOrEmpty(identifier);
        }

        public static bool ValidateVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) return false;
            Version version1 = null;
            try
            {
                version1 = new Version(version);  
            }catch (Exception)
            {
                return false;
            }

            return version1 != null;
        }

        public static bool ValidateService(ServiceDTO service)
        {
            return ValidateIdentifier(service.Identifier) && ValidateVersion(service.Version);
        }
    }
}
