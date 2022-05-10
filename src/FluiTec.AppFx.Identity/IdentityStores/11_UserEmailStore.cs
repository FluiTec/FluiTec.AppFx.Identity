using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user email store.
/// </summary>
public class UserEmailStore : UserPhoneNumberStore, IUserEmailStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserEmailStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserEmailStore

    /// <summary>
    /// Sets the <paramref name="email" /> address for a <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email should be set. </param>
    /// <param name="email">                The email to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task SetEmailAsync(UserEntity user, string email, CancellationToken cancellationToken)
    {
        return SetUserNameAsync(user, email, cancellationToken);
    }

    /// <summary>
    /// Gets the email address for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email should be returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object containing the results of the asynchronous operation, the email address for
    /// the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return GetUserNameAsync(user, cancellationToken);
    }

    /// <summary>
    /// Gets a flag indicating whether the email address for the specified <paramref name="user" />
    /// has been verified, true if the email address is verified otherwise false.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email confirmation status should be
    ///                                     returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object containing the results of the asynchronous operation, a flag indicating
    /// whether the email address for the specified <paramref name="user" />
    /// has been confirmed or not.
    /// </returns>
    public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.EmailConfirmed);
    }

    /// <summary>
    /// Sets the flag indicating whether the specified <paramref name="user" />'s email address has
    /// been confirmed or not.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email confirmation status should be set. </param>
    /// <param name="confirmed">            A flag indicating if the email address has been confirmed,
    ///                                     true if the address is confirmed otherwise false. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Gets the user, if any, associated with the specified, normalized email address.
    /// </summary>
    ///
    /// <param name="normalizedEmail">      The normalized email address to return the user for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object containing the results of the asynchronous lookup operation, the user if any
    /// associated with the specified normalized email address.
    /// </returns>
    public Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return FindByNameAsync(normalizedEmail, cancellationToken);
    }

    /// <summary>
    /// Returns the normalized email for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email address to retrieve. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object containing the results of the asynchronous lookup operation, the normalized
    /// email address if any associated with the specified user.
    /// </returns>
    public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return GetNormalizedUserNameAsync(user, cancellationToken);
    }

    /// <summary>
    /// Sets the normalized email for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose email address to set. </param>
    /// <param name="normalizedEmail">      The normalized email to set for the specified <paramref name="user" />
    ///                                     . </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail, CancellationToken cancellationToken)
    {
        return SetNormalizedUserNameAsync(user, normalizedEmail, cancellationToken);
    }

    #endregion
}