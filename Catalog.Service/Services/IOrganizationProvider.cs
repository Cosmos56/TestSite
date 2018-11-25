using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Service.Services
{
    public interface IOrganizationProvider<T> where T : class 
    {
        Task<IEnumerable<T>> GetAll(string searchString);
    }
}