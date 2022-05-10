using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for user role repository.
/// </summary>
public interface IUserRoleRepository : IWritableKeyTableDataRepository<UserRoleEntity, int>
{
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
    Task<UserRoleEntity> FindByUserIdAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for the first user asynchronous.
    /// </summary>
    ///
    /// <param name="user"> The user. </param>
    ///
    /// <returns>
    /// The find by user.
    /// </returns>
    Task<IEnumerable<RoleEntity>> FindByUserAsync(UserEntity user);

    /// <summary>
    /// Searches for the first role asynchronous.
    /// </summary>
    ///
    /// <param name="role"> The role. </param>
    ///
    /// <returns>
    /// The find by role.
    /// </returns>
    Task<IEnumerable<UserEntity>> FindByRoleAsync(RoleEntity role);
}