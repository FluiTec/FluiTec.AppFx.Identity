using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>
    /// A dapper user login repository.
    /// </summary>
    public class DapperUserLoginRepository : DapperWritableKeyTableDataRepository<UserLoginEntity, int>, IUserLoginRepository
    {
        public DapperUserLoginRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task DeleteAsync(UserEntity user, string provider, string providerKey, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserLoginEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserLoginEntity> GetByProviderWithKeyAsync(string provider, string providerKey, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}