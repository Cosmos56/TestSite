using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestSite.Model;

namespace TestSite.Services
{
    public interface IOrganizationService
    {
        Task<CatalogItem> Get(int id);

        Task<HttpResponseMessage> Create(CatalogItem details);

        Task<HttpStatusCode> Delete(int id);
    }
}