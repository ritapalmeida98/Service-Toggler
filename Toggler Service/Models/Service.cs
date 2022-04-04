namespace Toggler_Service.Models
{
    public class Service : Entity
    {
        public string Identifier { get; set; }
        public string Version { get; set; }
        public virtual List<ToggleService> ToggleServices { get; set; }
    }
}
