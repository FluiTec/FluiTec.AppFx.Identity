using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>
    ///     Repository for users using Dapper.
    /// </summary>
    public class DapperUserRepository: DapperSequentialGuidRepository<UserEntity>, IUserRepository
    {
        public DapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task<UserEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserEntity>> GetByClaimAsync(BaseClaim baseClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByLoginAsync(string provider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}