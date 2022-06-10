using System;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user lockout store.
/// </summary>
public class UserLockoutStore : UserTwoFactorStore, IUserLockoutStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserLockoutStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserLockoutStore

    /// <summary>
    /// Gets the last <see cref="T:System.DateTimeOffset" /> a user's last lockout expired, if any.
    /// Any time in the past should be indicates a user is not locked out.
    /// </summary>
    ///
    /// <param name="user">                 The user whose lockout date should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
    /// asynchronous query, a <see cref="T:System.DateTimeOffset" /> containing the last time a
    /// user's lockout expired, if any.
    /// </returns>
    public Task<DateTimeOffset?> GetLockoutEndDateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        if (!user.LockoutEnabled || !user.LockedOutTill.HasValue) return Task.FromResult(default(DateTimeOffset?));
        return Task.FromResult(user.LockedOutPermanently == false ? user.LockedOutTill : DateTimeOffset.MaxValue);
    }

    /// <summary>
    /// Locks out a user until the specified end date has passed. Setting a end date in the past
    /// immediately unlocks a user.
    /// </summary>
    ///
    /// <param name="user">                 The user whose lockout date should be set. </param>
    /// <param name="lockoutEnd">           The <see cref="T:System.DateTimeOffset" /> after which
    ///                                     the <paramref name="user" />'s lockout should end. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetLockoutEndDateAsync(UserEntity user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
    {
        user.LockedOutTill = lockoutEnd;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Records that a failed access has occurred, incrementing the failed access count.
    /// </summary>
    ///
    /// <param name="user">                 The user whose cancellation count should be incremented. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the incremented failed access count.
    /// </returns>
    public async Task<int> IncrementAccessFailedCountAsync(UserEntity user, CancellationToken cancellationToken)
    {
        user.AccessFailedCount++;
        await UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        return user.AccessFailedCount;
    }

    /// <summary>
    /// Resets a user's failed access count.
    /// </summary>
    ///
    /// <remarks>
    /// This is typically called after the account is successfully accessed.
    /// </remarks>
    ///
    /// <param name="user">                 The user whose failed access count should be reset. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task ResetAccessFailedCountAsync(UserEntity user, CancellationToken cancellationToken)
    {
        user.AccessFailedCount = 0;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Retrieves the current failed access count for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose failed access count should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the failed access count.
    /// </returns>
    public Task<int> GetAccessFailedCountAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.AccessFailedCount);
    }

    /// <summary>
    /// Retrieves a flag indicating whether user lockout can enabled for the specified user.
    /// </summary>
    ///
    /// <param name="user">                 The user whose ability to be locked out should be
    ///                                     returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// true if a user can be locked out, otherwise false.
    /// </returns>
    public Task<bool> GetLockoutEnabledAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.LockoutEnabled);
    }

    /// <summary>
    /// Set the flag indicating if the specified <paramref name="user" /> can be locked out.
    /// </summary>
    ///
    /// <param name="user">                 The user whose ability to be locked out should be set. </param>
    /// <param name="enabled">              A flag indicating if lock out can be enabled for the
    ///                                     specified <paramref name="user" />. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetLockoutEnabledAsync(UserEntity user, bool enabled, CancellationToken cancellationToken)
    {
        user.LockoutEnabled = enabled;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    #endregion
}