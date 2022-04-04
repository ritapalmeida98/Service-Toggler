using Toggler_Service.Models;

namespace Toggler_Service.Repositories
{
    public interface IToggleRepository
    {
        Toggle Add(Toggle toggle);
        Toggle GetByName(string name);
        List<Toggle> GetAll();
    }
}
