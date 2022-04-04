namespace Toggler_Service.Models
{
    public class ToggleService : Entity
    {
        public long ToggleId { get; set; }
        public long ServiceId { get; set; }
        public bool? Value { get; set; }
        public virtual Service Service { get; set; }
        public virtual Toggle Toggle { get; set; }
    }
}
