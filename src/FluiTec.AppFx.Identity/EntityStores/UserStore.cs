using System;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.EntityStores
{
    /// <summary>   A user store. </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public class UserStore : IUserStore<UserEntity>, IUserPhoneNumberStore<UserEntity>, IUserEmailStore<UserEntity>, IUserPasswordStore<UserEntity>
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public UserStore(IIdentityDataService dataService)
        {
            UnitOfWork = dataService.BeginUnitOfWork() ?? throw new ArgumentNullException(nameof(dataService));
        }

        #endregion

        #region Properties

        /// <summary>   Gets the unit of work. </summary>
        /// <value> The unit of work. </value>
        protected IIdentityUnitOfWork UnitOfWork { get; }

        #endregion

        #region IUserStore

        /// <summary>   Creates the specified <paramref name="user" /> in the user store. </summary>
        /// <param name="user">                 The user to create. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        ///     of the creation operation.
        /// </returns>
        public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IdentityResult>.Factory.StartNew(() =>
            {
                try
                {
                    UnitOfWork.UserRepository.Add(user);
                    return IdentityResult.Success;
                }
                catch (Exception)
                {
                    return IdentityResult.Failed();
                }
            }, cancellationToken);
        }

        /// <summary>
        ///     Finds and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">               The user ID to search for. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the user matching the specified <paramref name="userId" /> if it
        ///     exists.
        /// </returns>
        public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(() => UnitOfWork.UserRepository.Get(userId),
                cancellationToken);
        }

        /// <summary>
        ///     Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        /// <param name="normalizedUserName">   The normalized user name to search for. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should
        ///                                     be canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the user matching the specified
        ///     <paramref name="normalizedUserName" /> if it exists.
        /// </returns>
        public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(
                () => UnitOfWork.UserRepository.FindByLoweredName(normalizedUserName), cancellationToken);
        }

        /// <summary>   Updates the specified <paramref name="user" /> in the user store. </summary>
        /// <param name="user">                 The user to update. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        ///     of the update operation.
        /// </returns>
        public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IdentityResult>.Factory.StartNew(() =>
            {
                try
                {
                    UnitOfWork.UserRepository.Update(user);
                    return IdentityResult.Success;
                }
                catch (Exception)
                {
                    return IdentityResult.Failed();
                }
            }, cancellationToken);
        }

        /// <summary>   Deletes the specified <paramref name="user" /> from the user store. </summary>
        /// <param name="user">                 The user to delete. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        ///     of the update operation.
        /// </returns>
        public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IdentityResult>.Factory.StartNew(() =>
            {
                try
                {
                    var roles = UnitOfWork.UserRoleRepository.FindByUser(user);
                    foreach(var role in roles)
                        UnitOfWork.UserRoleRepository.Delete(role);

                    UnitOfWork.UserRepository.Delete(user);
                    return IdentityResult.Success;
                }
                catch (Exception)
                {
                    return IdentityResult.Failed();
                }
            }, cancellationToken);
        }

        /// <summary>   Gets the user identifier for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose identifier should be retrieved. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the identifier for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        /// <summary>   Gets the user name for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose name should be retrieved. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the name for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        /// <summary>
        ///     Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">                 The user whose name should be set. </param>
        /// <param name="userName">             The user name to set. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.Name = userName;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        /// <summary>   Gets the normalized user name for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose normalized name should be retrieved. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the normalized user name for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeName(user.Name));
        }

        /// <summary>   Sets the given normalized name for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose name should be set. </param>
        /// <param name="normalizedName">       The normalized name to set. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPhoneNumberStore

        /// <summary>   Sets the telephone number for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose telephone number should be set. </param>
        /// <param name="phoneNumber">          The telephone number to set. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetPhoneNumberAsync(UserEntity user, string phoneNumber, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.Phone = phoneNumber;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        /// <summary>
        ///     Gets the telephone number, if any, for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">                 The user whose telephone number should be retrieved. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the user's telephone number, if any.
        /// </returns>
        public Task<string> GetPhoneNumberAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Phone);
        }

        /// <summary>
        ///     Gets a flag indicating whether the specified <paramref name="user" />'s telephone number
        ///     has been confirmed.
        /// </summary>
        /// <param name="user">                 The user to return a flag for, indicating whether their
        ///                                     telephone number is confirmed. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, returning true if the specified <paramref name="user" /> has a confirmed
        ///     telephone number otherwise false.
        /// </returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneConfirmed);
        }

        /// <summary>
        ///     Sets a flag indicating if the specified <paramref name="user" />'s phone number has been
        ///     confirmed.
        /// </summary>
        /// <param name="user">                 The user whose telephone number confirmation status
        ///                                     should be set. </param>
        /// <param name="confirmed">            A flag indicating whether the user's telephone number has
        ///                                     been confirmed. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetPhoneNumberConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PhoneConfirmed = confirmed;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        #endregion
        
        #region IUserEmailStore

        /// <summary>   Sets the <paramref name="email" /> address for a <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose email should be set. </param>
        /// <param name="email">                The email to set. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task SetEmailAsync(UserEntity user, string email, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.Email = email;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        /// <summary>   Gets the email address for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose email should be returned. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The task object containing the results of the asynchronous operation, the email address
        ///     for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        ///     Gets a flag indicating whether the email address for the specified
        ///     <paramref name="user" /> has been verified, true if the email address is verified
        ///     otherwise false.
        /// </summary>
        /// <param name="user">                 The user whose email confirmation status should be
        ///                                     returned. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The task object containing the results of the asynchronous operation, a flag indicating
        ///     whether the email address for the specified <paramref name="user" />
        ///     has been confirmed or not.
        /// </returns>
        public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        ///     Sets the flag indicating whether the specified <paramref name="user" />'s email address
        ///     has been confirmed or not.
        /// </summary>
        /// <param name="user">                 The user whose email confirmation status should be set. </param>
        /// <param name="confirmed">            A flag indicating if the email address has been confirmed,
        ///                                     true if the address is confirmed otherwise false. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.EmailConfirmed = confirmed;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        /// <summary>
        ///     Gets the user, if any, associated with the specified, normalized email address.
        /// </summary>
        /// <param name="normalizedEmail">      The normalized email address to return the user for. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The task object containing the results of the asynchronous lookup operation, the user if
        ///     any associated with the specified normalized email address.
        /// </returns>
        public Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(
                () => UnitOfWork.UserRepository.FindByNormalizedEmail(normalizedEmail), cancellationToken);
        }

        /// <summary>   Returns the normalized email for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose email address to retrieve. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The task object containing the results of the asynchronous lookup operation, the
        ///     normalized email address if any associated with the specified user.
        /// </returns>
        public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeMail(user.Email));
        }

        /// <summary>   Sets the normalized email for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose email address to set. </param>
        /// <param name="normalizedEmail">      The normalized email to set for the specified
        ///                                     <paramref name="user" />. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPasswordStore

        /// <summary>   Sets the password hash for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose password hash to set. </param>
        /// <param name="passwordHash">         The password hash to set. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PasswordHash = passwordHash;
                UnitOfWork.UserRepository.Update(user);
            }, cancellationToken);
        }

        /// <summary>   Gets the password hash for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose password hash to retrieve. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, returning the password hash for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        ///     Gets a flag indicating whether the specified <paramref name="user" /> has a password.
        /// </summary>
        /// <param name="user">                 The user to return a flag for, indicating whether they
        ///                                     have a password or not. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, returning true if the specified <paramref name="user" /> has a password
        ///     otherwise false.
        /// </returns>
        public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        ///     resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        ///     resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                UnitOfWork.Commit();
            _disposed = true;
        }

        #endregion
    }
}
