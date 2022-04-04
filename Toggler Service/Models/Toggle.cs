namespace Toggler_Service.Models
{
    public class Toggle : Entity
    {
        public string Name { get; set; }
        public virtual List<ToggleService> ToggleServices { get; set; }
    }
}
