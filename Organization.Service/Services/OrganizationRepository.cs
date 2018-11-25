using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Microsoft.Extensions.Options;
using Organization.Service.Model;

namespace Organization.Service.Services
{
    public class OrganizationRepository : IRepository
    {
        #region Privat members

        private readonly IOptions<ServiceSettings> _connectonString;

        #endregion

        #region Constructors

        public OrganizationRepository(IOptions<ServiceSettings> connectonString)
        {
            _connectonString = connectonString;
        }

        #endregion

        #region Methods and Tasks

        public async Task<string> Create(OrganizationDetails details)
        {
            using (var connection = new SqlConnection(_connectonString.Value.ConnectionString))
            {
                connection.Open();
                using (var transact = connection.BeginTransaction())
                {
                    try
                    {
                        var sql =
                            "insert into Organization (OrganizationName, TypeOwnership, StateDateRegistration, Ogrn, Inn, Kpp) values (@OrganizationName, @TypeOwnership, @StateDateRegistration, @Ogrn, @Inn, @Kpp)";
                        await connection.QueryAsync(sql, details, transact);
                        transact.Commit();
                        return "Ok";
                    }
                    catch (SqlException ex)
                    {
                        transact.Rollback();
                        return ex.Message;
                    }
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectonString.Value.ConnectionString))
            {
                connection.Open();
                using (var transact = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "delete from Organization where Id = @id";
                        await connection.QueryAsync(sql, new { id }, transact);
                        transact.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transact.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<OrganizationDetails> Get(int id)
        {
            OrganizationDetails organization;

            using (var connection = new SqlConnection(_connectonString.Value.ConnectionString))
            {
                var result = await connection.QueryAsync<OrganizationDetails>("select * from Organization where Id = @id", new { id });
                organization = result.FirstOrDefault();
            }

            return organization;
        }

        public async Task<bool> CheckIdentity(string nameField, string value)
        {
            bool response;
            using (var connection = new SqlConnection(_connectonString.Value.ConnectionString))
            {
                var result = await connection.QueryAsync("select id from Organization where " + nameField + " = '" + value + "'");

                response = result.ToList().Count <= 0;
            }

            return response;
        }

        #endregion
    }
}