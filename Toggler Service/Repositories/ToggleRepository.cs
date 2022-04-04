using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public class ToggleRepository : IToggleRepository
    {
        private readonly ServiceTogglerContext _context;

        public ToggleRepository(ServiceTogglerContext context)
        {
            _context = context;
        }

        public Toggle Add(Toggle toggle)
        {
            _context.Set<Toggle>().Add(toggle);
            _context.SaveChanges();
            return toggle;
        }

        public List<Toggle> GetAll()
        {
            throw new NotImplementedException();
        }

        public Toggle GetByName(string name)
        {
            return _context.Set<Toggle>().FirstOrDefault(x => x.Name == name);
        }
    }
}
