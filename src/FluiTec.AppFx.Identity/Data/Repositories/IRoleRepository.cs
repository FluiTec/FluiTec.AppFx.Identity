using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for role repository.
/// </summary>
public interface IRoleRepository : IWritableKeyTableDataRepository<RoleEntity, Guid>
{
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
    public Task<RoleEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken);

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
    public Task<IEnumerable<RoleEntity>> GetByRoleIdsAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken);
}