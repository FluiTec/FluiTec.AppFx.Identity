using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for user repository.
/// </summary>
public interface IUserRepository : IWritableKeyTableDataRepository<UserEntity, Guid>
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
    public Task<UserEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken);

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
    Task<IEnumerable<UserEntity>> GetByClaimAsync(BaseClaim baseClaim, CancellationToken cancellationToken);

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
    Task<UserEntity> FindByLoginAsync(string provider, string providerKey, CancellationToken cancellationToken);
}