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
    /// A memory user role repository.
    /// </summary>
    public class NMemoryUserRoleRepository : NMemoryWritableKeyTableDataRepository<UserRoleEntity, int>, IUserRoleRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryUserRoleRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Searches for the first user identifier and role identifier asynchronous.
        /// </summary>
        ///
        /// <param name="userId">               Identifier for the user. </param>
        /// <param name="roleId">               Identifier for the role. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by user identifier and role identifier.
        /// </returns>
        public Task<UserRoleEntity> FindByUserIdAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
        {
            var result = Table.Single(ur => ur.UserId == userId && ur.RoleId == roleId);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Searches for the first user asynchronous.
        /// </summary>
        ///
        /// <param name="user">                 The user. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by user.
        /// </returns>
        public Task<IEnumerable<RoleEntity>> FindByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var roleIds = Table
                .Where(u => u.UserId == user.Id)
                .Select(ur => ur.RoleId);
            return UnitOfWork.GetRepository<IRoleRepository>().GetByRoleIdsAsync(roleIds, cancellationToken);
        }

        /// <summary>
        /// Searches for the first role asynchronous.
        /// </summary>
        ///
        /// <param name="role">                 The role. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by role.
        /// </returns>
        public Task<IEnumerable<UserEntity>> FindByRoleAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            var userIds = Table
                .Where(u => u.RoleId == role.Id)
                .Select(ur => ur.UserId);
            return UnitOfWork.GetRepository<IUserRepository>().GetByUserIdsAsync(userIds, cancellationToken);
        }

        /// <summary>
        /// Searches for the first roles asynchronous.
        /// </summary>
        ///
        /// <param name="roleIds">              List of identifiers for the roles. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The find by roles.
        /// </returns>
        public Task<IEnumerable<UserEntity>> FindByRolesAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken)
        {
            var userIds = Table
                .Where(u => roleIds.Contains(u.RoleId))
                .Select(ur => ur.UserId);
            return UnitOfWork.GetRepository<IUserRepository>().GetByUserIdsAsync(userIds, cancellationToken);
        }
    }
}