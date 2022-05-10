using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for role claim repository.
/// </summary>
public interface IRoleClaimRepository : IWritableKeyTableDataRepository<RoleClaimEntity, int>
{
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
    Task<IEnumerable<RoleClaimEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken);
}