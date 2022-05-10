using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user phone number store.
/// </summary>
public class UserPhoneNumberStore : UserPasswordStore, IUserPhoneNumberStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserPhoneNumberStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserPhoneNumberStore

    /// <summary>
    /// Gets the telephone number, if any, for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose telephone number should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the user's telephone number, if any.
    /// </returns>
    public Task<string> GetPhoneNumberAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Phone);
    }

    /// <summary>
    /// Gets a flag indicating whether the specified <paramref name="user" />'s telephone number has
    /// been confirmed.
    /// </summary>
    ///
    /// <param name="user">                 The user to return a flag for, indicating whether their
    ///                                     telephone number is confirmed. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// returning true if the specified <paramref name="user" /> has a confirmed telephone number
    /// otherwise false.
    /// </returns>
    public Task<bool> GetPhoneNumberConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PhoneConfirmed);
    }

    /// <summary>
    /// Sets the telephone number for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose telephone number should be set. </param>
    /// <param name="phoneNumber">          The telephone number to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetPhoneNumberAsync(UserEntity user, string phoneNumber, CancellationToken cancellationToken)
    {
        user.Phone = phoneNumber;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Sets a flag indicating if the specified <paramref name="user" />'s phone number has been
    /// confirmed.
    /// </summary>
    ///
    /// <param name="user">                 The user whose telephone number confirmation status
    ///                                     should be set. </param>
    /// <param name="confirmed">            A flag indicating whether the user's telephone number has
    ///                                     been confirmed. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetPhoneNumberConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
    {
        user.PhoneConfirmed = confirmed;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    #endregion
}