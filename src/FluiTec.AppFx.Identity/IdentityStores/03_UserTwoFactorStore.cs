using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user two factor store.
/// </summary>
public class UserTwoFactorStore : UserSecurityStampStore, IUserTwoFactorStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserTwoFactorStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserTwoFactorStore

    /// <summary>
    /// Sets a flag indicating whether the specified <paramref name="user" /> has two factor
    /// authentication enabled or not, as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="user">                 The user whose two factor authentication enabled status
    ///                                     should be set. </param>
    /// <param name="enabled">              A flag indicating whether the specified <paramref name="user" />
    ///                                     has two factor authentication enabled. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetTwoFactorEnabledAsync(UserEntity user, bool enabled, CancellationToken cancellationToken)
    {
        user.TwoFactorEnabled = enabled;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Returns a flag indicating whether the specified <paramref name="user" /> has two factor
    /// authentication enabled or not, as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="user">                 The user whose two factor authentication enabled status
    ///                                     should be set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing a flag indicating whether the specified.
    /// </returns>
    public Task<bool> GetTwoFactorEnabledAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.TwoFactorEnabled);
    }

    #endregion
}