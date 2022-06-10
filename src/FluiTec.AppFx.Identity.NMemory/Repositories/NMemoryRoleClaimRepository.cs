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
    /// A memory role claim repository.
    /// </summary>
    public class NMemoryRoleClaimRepository : NMemoryWritableKeyTableDataRepository<RoleClaimEntity, int>, IRoleClaimRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryRoleClaimRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Gets by user asynchronous.
        /// </summary>
        ///
        /// <param name="user">                 The user. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by user.
        /// </returns>
        public async Task<IEnumerable<RoleClaimEntity>> GetByUserAsync(UserEntity user,
            CancellationToken cancellationToken)
        {
            var roles = await UnitOfWork.GetRepository<IUserRoleRepository>()
                .FindByUserAsync(user, cancellationToken);
            var roleIds = roles.Select(r => r.Id);

            var claims = Table.Where(c => roleIds.Contains(c.RoleId));

            return claims;;
        }

        /// <summary>
        /// Gets by claim asynchronous.
        /// </summary>
        ///
        /// <param name="claim">                The claim. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by claim.
        /// </returns>
        public Task<IEnumerable<RoleClaimEntity>> GetByClaimAsync(BaseClaim claim, CancellationToken cancellationToken)
        {
            var result = Table
                .Where(c => c.Type == claim.Type && c.Value == claim.Value || claim.Value == null)
                .AsEnumerable();
            return Task.FromResult(result);
        }
    }
}