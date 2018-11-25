using System.Threading.Tasks;
using Organization.Service.Model;

namespace Organization.Service.Services
{
    public interface IRepository
    {
        Task<OrganizationDetails> Get(int id);

        Task<string> Create(OrganizationDetails details);

        Task<bool> Delete(int id);

        Task<bool> CheckIdentity(string nameField, string value);
    }
}