using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for user login repository.
/// </summary>
public interface IUserLoginRepository : IWritableKeyTableDataRepository<UserLoginEntity, int>
{
    /// <summary>
    /// Removes the asynchronous.
    /// </summary>
    ///
    /// <param name="user">                 The user. </param>
    /// <param name="provider">             The provider. </param>
    /// <param name="providerKey">          The provider key. </param>
    /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A Task.
    /// </returns>
    Task DeleteAsync(UserEntity user, string provider, string providerKey, CancellationToken cancellationToken);

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
    Task<IEnumerable<UserLoginEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken);

    /// <summary>
    /// Gets by provider with key asynchronous.
    /// </summary>
    ///
    /// <param name="provider">             The provider. </param>
    /// <param name="providerKey">          The provider key. </param>
    /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The by provider with key.
    /// </returns>
    Task<UserLoginEntity> GetByProviderWithKeyAsync(string provider, string providerKey,
        CancellationToken cancellationToken);
}