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
    /// A memory role repository.
    /// </summary>
    public class NMemoryRoleRepository : NMemorySequentialGuidRepository<RoleEntity>, IRoleRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryRoleRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
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
        public Task<RoleEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            var result = Table.Single(r => r.NormalizedName == normalizedName);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets by role identifiers.
        /// </summary>
        ///
        /// <param name="roleIds">              List of identifiers for the roles. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by role identifiers.
        /// </returns>
        public Task<IEnumerable<RoleEntity>> GetByRoleIdsAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken)
        {
            var result = Table.Where(r => roleIds.Contains(r.Id)).AsEnumerable();
            return Task.FromResult(result);
        }
    }
}