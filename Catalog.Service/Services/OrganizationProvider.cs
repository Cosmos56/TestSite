using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Service.Model;
using Dapper;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Services
{
    public class OrganizationProvider : IOrganizationProvider<Organization>
    {
        private readonly IOptions<ServiceSettings> _connectonString;

        public OrganizationProvider(IOptions<ServiceSettings> connectonString)
        {
            _connectonString = connectonString;
        }
        public async Task<IEnumerable<Organization>> GetAll(string searchString)
        {
            IEnumerable<Organization> organization = null;

            var sql = "select Id, OrganizationName, TypeOwnership, StateDateRegistration from Organization";

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (Regex.IsMatch(searchString, @"^[А-Яа-я ]*$"))
                {
                    sql = sql + " where OrganizationName like '%" + searchString + "%'";
                }

                if (Regex.IsMatch(searchString, @"^[\d]*$"))
                {
                    sql = sql + " where Ogrn like '%" + searchString + "%' or Inn like '%" + searchString + "%' or Kpp like '%" + searchString + "%'";
                }
            }

            using (var connection = new SqlConnection(_connectonString.Value.ConnectionString))
            {
                organization = await connection.QueryAsync<Organization>(sql);
            }

            return organization;
        }
    }
}