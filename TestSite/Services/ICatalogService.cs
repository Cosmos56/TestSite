using System.Collections.Generic;
using System.Threading.Tasks;
using TestSite.Model;

namespace TestSite.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetAll(string searchString);
    }
}