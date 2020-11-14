using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.EntityStores
{
    /// <summary>   A user store. </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public class UserStore : IUserStore<UserEntity>, IUserPhoneNumberStore<UserEntity>, IUserEmailStore<UserEntity>,
        IUserPasswordStore<UserEntity>,
        IUserClaimStore<UserEntity>, IUserLoginStore<UserEntity>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should
        ///     be canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the user matching the specified
        ///     <paramref name="normalizedUserName" /> if it exists.
        /// </returns>
        public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(
                () => UnitOfWork.UserRepository.FindByNormalizedName(normalizedUserName), cancellationToken);
        }

        /// <summary>   Updates the specified <paramref name="user" /> in the user store. </summary>
        /// <param name="user">                 The user to update. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
                    UnitOfWork.UserRoleRepository.RemoveByUser(user);
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing the normalized user name for the specified <paramref name="user" />.
        /// </returns>
        public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedName);
        }

        /// <summary>   Sets the given normalized name for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose name should be set. </param>
        /// <param name="normalizedName">       The normalized name to set. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPhoneNumberStore

        /// <summary>   Sets the telephone number for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose telephone number should be set. </param>
        /// <param name="phoneNumber">          The telephone number to set. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="user">
        ///     The user to return a flag for, indicating whether their
        ///     telephone number is confirmed.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="user">
        ///     The user whose telephone number confirmation status
        ///     should be set.
        /// </param>
        /// <param name="confirmed">
        ///     A flag indicating whether the user's telephone number has
        ///     been confirmed.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="user">
        ///     The user whose email confirmation status should be
        ///     returned.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="confirmed">
        ///     A flag indicating if the email address has been confirmed,
        ///     true if the address is confirmed otherwise false.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The task object containing the results of the asynchronous lookup operation, the
        ///     normalized email address if any associated with the specified user.
        /// </returns>
        public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        /// <summary>   Sets the normalized email for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose email address to set. </param>
        /// <param name="normalizedEmail">
        ///     The normalized email to set for the specified
        ///     <paramref name="user" />.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPasswordStore

        /// <summary>   Sets the password hash for the specified <paramref name="user" />. </summary>
        /// <param name="user">                 The user whose password hash to set. </param>
        /// <param name="passwordHash">         The password hash to set. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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
        /// <param name="user">
        ///     The user to return a flag for, indicating whether they
        ///     have a password or not.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
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

        #region IUserClaimStore

        /// <summary>
        ///     Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the
        ///     specified <paramref name="user" /> as an asynchronous operation.
        /// </summary>
        /// <param name="user">                 The role whose claims to retrieve. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
        ///     asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
        /// </returns>
        public Task<IList<Claim>> GetClaimsAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IList<Claim>>.Factory.StartNew(
                () =>
                {
                    // add roles as claims
                    var roles = UnitOfWork.UserRoleRepository.FindByUser(user);
                    var rClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));

                    // add basic claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name)
                    };
                    if (!string.IsNullOrWhiteSpace(user.Phone))
                        claims.Add(new Claim(ClaimTypes.HomePhone, user.Phone));
                    if (!string.IsNullOrWhiteSpace(user.FullName))
                        claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
                    claims.AddRange(rClaims);

                    // fetch user- und role claims from the db
                    var dbClaims = UnitOfWork.UserRepository.FindAllClaims(user)
                        .Select(c => new Claim(c.Type, c.Value));
                    claims.AddRange(dbClaims);

                    return claims;
                },
                cancellationToken);
        }

        /// <summary>   Add claims to a user as an asynchronous operation. </summary>
        /// <param name="user">                 The user to add the claim to. </param>
        /// <param name="claims">
        ///     The collection of
        ///     <see cref="T:System.Security.Claims.Claim" />s to add.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var identityClaims = claims.Select(c =>
                    new UserClaimEntity {UserId = user.Id, Type = c.Type, Value = c.Value});

                UnitOfWork.UserClaimRepository.AddRange(identityClaims);
            }, cancellationToken);
        }

        /// <summary>
        ///     Replaces the given <paramref name="claim" /> on the specified <paramref name="user" />
        ///     with the <paramref name="newClaim" />
        /// </summary>
        /// <param name="user">                 The user to replace the claim on. </param>
        /// <param name="claim">                The claim to replace. </param>
        /// <param name="newClaim">
        ///     The new claim to replace the existing
        ///     <paramref name="claim" /> with.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var entity = UnitOfWork.UserClaimRepository.GetByUserAndType(user, claim.Type);
                entity.Type = newClaim.Type;
                entity.Value = newClaim.Value;
                UnitOfWork.UserClaimRepository.Update(entity);
            }, cancellationToken);
        }

        /// <summary>
        ///     Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">
        ///     The user to remove the specified
        ///     <paramref name="claims" /> from.
        /// </param>
        /// <param name="claims">
        ///     A collection of
        ///     <see cref="T:System.Security.Claims.Claim" />s to remove.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var claimTypes = claims.Select(c => c.Type).ToList();
                var entities = UnitOfWork.UserClaimRepository.GetByUser(user).Where(c => claimTypes.Contains(c.Type));
                foreach (var entity in entities)
                    UnitOfWork.UserClaimRepository.Delete(entity);
            }, cancellationToken);
        }

        /// <summary>
        ///     Returns a list of users who contain the specified
        ///     <see cref="T:System.Security.Claims.Claim" />.
        /// </summary>
        /// <param name="claim">                The claim to look for. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
        ///     asynchronous query, a list of who contain the specified
        ///     claim.
        /// </returns>
        public Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return Task<IList<UserEntity>>.Factory.StartNew(() =>
            {
                var userIds = UnitOfWork.UserClaimRepository.GetUserIdsForClaimType(claim.Type);
                var users = UnitOfWork.UserRepository.FindByIds(userIds);

                var rUsers = UnitOfWork.RoleClaimRepository.GetUsersForClaimType(claim.Type);
                return users.Concat(rUsers).Distinct(new UserComparer()).ToList();
            }, cancellationToken);
        }

        #endregion

        #region IUserLoginStore

        /// <summary>
        ///     Adds an external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to the
        ///     specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">                 The user to add the login to. </param>
        /// <param name="login">
        ///     The external
        ///     <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" />
        ///     to add to the specified <paramref name="user" />.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task AddLoginAsync(UserEntity user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(() =>
            {
                UnitOfWork.LoginRepository.Add(new UserLoginEntity
                {
                    ProviderName = login.LoginProvider,
                    ProviderKey = login.ProviderKey,
                    ProviderDisplayName = login.ProviderDisplayName,
                    UserId = user.Id
                });
                return user;
            }, cancellationToken);
        }

        /// <summary>
        ///     Attempts to remove the provided login information from the specified
        ///     <paramref name="user" />. and returns a flag indicating whether the removal succeed or
        ///     not.
        /// </summary>
        /// <param name="user">                 The user to remove the login information from. </param>
        /// <param name="loginProvider">        The login provide whose information should be removed. </param>
        /// <param name="providerKey">
        ///     The key given by the external login provider for the
        ///     specified user.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task RemoveLoginAsync(UserEntity user, string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(
                () => { UnitOfWork.LoginRepository.RemoveByNameAndKey(loginProvider, providerKey); },
                cancellationToken);
        }

        /// <summary>
        ///     Retrieves the associated logins for the specified
        ///     <param ref="user" />
        ///     .
        /// </summary>
        /// <param name="user">                 The user whose associated logins to retrieve. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation,
        ///     containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the
        ///     specified <paramref name="user" />, if any.
        /// </returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IList<UserLoginInfo>>.Factory.StartNew(() =>
            {
                var entities = UnitOfWork.LoginRepository.FindByUserId(user.Id);
                return entities.Select(e => new UserLoginInfo(e.ProviderName, e.ProviderKey, e.ProviderDisplayName))
                    .ToList();
            }, cancellationToken);
        }

        /// <summary>
        ///     Retrieves the user associated with the specified login provider and login provider key.
        /// </summary>
        /// <param name="loginProvider">
        ///     The login provider who provided the
        ///     <paramref name="providerKey" />.
        /// </param>
        /// <param name="providerKey">
        ///     The key provided by the <paramref name="loginProvider" />
        ///     to identify a user.
        /// </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation,
        ///     containing the user, if any which matched the specified login provider and key.
        /// </returns>
        public Task<UserEntity> FindByLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(
                () => UnitOfWork.UserRepository.FindByLogin(loginProvider, providerKey), cancellationToken);
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