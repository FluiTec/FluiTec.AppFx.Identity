using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.NMemory.Repositories
{
    /// <summary>
    /// A memory user repository.
    /// </summary>
    public class NMemoryUserRepository : NMemorySequentialGuidRepository<UserEntity>, IUserRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryUserRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Searches for the first normalized name asynchronous.
        /// </summary>
        ///
        /// <param name="normalizedName">       Name of the normalized. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by normalized name.
        /// </returns>
        public Task<UserEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            var result = Table.Single(u => u.NormalizedEmail == normalizedName);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets by claim asynchronous.
        /// </summary>
        ///
        /// <param name="baseClaim">            The base claim. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by claim.
        /// </returns>
        public async Task<IEnumerable<UserEntity>> GetByClaimAsync(BaseClaim baseClaim, CancellationToken cancellationToken)
        {
            var userClaims = await UnitOfWork
                .GetRepository<IUserClaimRepository>()
                .GetByClaimAsync(baseClaim, cancellationToken);

            var roleClaims = await UnitOfWork
                .GetRepository<IRoleClaimRepository>()
                .GetByClaimAsync(baseClaim, cancellationToken);

            var roleUsers = await UnitOfWork
                .GetRepository<IUserRoleRepository>()
                .FindByRolesAsync(roleClaims.Select(rc => rc.RoleId), cancellationToken);

            var uids = userClaims
                .Select(uc => uc.UserId)
                .Concat(roleUsers.Select(ru => ru.Id))
                .Distinct();
            return await GetByUserIdsAsync(uids, cancellationToken);
        }

        /// <summary>
        /// Searches for the first login asynchronous.
        /// </summary>
        ///
        /// <param name="provider">             The provider. </param>
        /// <param name="providerKey">          The provider key. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by login.
        /// </returns>
        public async Task<UserEntity> FindByLoginAsync(string provider, string providerKey, CancellationToken cancellationToken)
        {
            var login = await UnitOfWork
                .GetRepository<IUserLoginRepository>()
                .GetByProviderWithKeyAsync(provider, providerKey, cancellationToken);

            return await GetAsync(login.UserId, cancellationToken);
        }

        /// <summary>
        /// Gets by user identifiers asynchronous.
        /// </summary>
        ///
        /// <param name="userIds">              List of identifiers for the users. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by user identifiers.
        /// </returns>
        public Task<IEnumerable<UserEntity>> GetByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
        {
            var result = Table.Where(r => userIds.Contains(r.Id)).AsEnumerable();
            return Task.FromResult(result);
        }
    }
}