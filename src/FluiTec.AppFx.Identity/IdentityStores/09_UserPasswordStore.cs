using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user password store.
/// </summary>
public class UserPasswordStore : UserLoginStore, IUserPasswordStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserPasswordStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserPasswordStore

    /// <summary>
    /// Sets the password hash for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose password hash to set. </param>
    /// <param name="passwordHash">         The password hash to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Gets the password hash for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose password hash to retrieve. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// returning the password hash for the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    /// <summary>
    /// Gets a flag indicating whether the specified <paramref name="user" /> has a password.
    /// </summary>
    ///
    /// <param name="user">                 The user to return a flag for, indicating whether they
    ///                                     have a password or not. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// returning true if the specified <paramref name="user" /> has a password otherwise false.
    /// 
    /// </returns>
    public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
    }

    #endregion
}