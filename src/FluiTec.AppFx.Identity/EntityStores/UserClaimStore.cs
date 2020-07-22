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
    /// <summary>   A user claim store. </summary>
    public class UserClaimStore : RoleStore, IUserClaimStore<UserEntity>
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public UserClaimStore(IIdentityDataService dataService) : base(dataService)
        {
        }

        #endregion

        #region IUserClaimStore

        /// <summary>
        ///     Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the
        ///     specified <paramref name="user" /> as an asynchronous operation.
        /// </summary>
        /// <param name="user">                 The role whose claims to retrieve. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
        ///     asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
        /// </returns>
        public Task<IList<Claim>> GetClaimsAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IList<Claim>>.Factory.StartNew(
                () =>
                {
                    var roleIds = UnitOfWork.UserRoleRepository.FindByUser(user);
                    var roles = UnitOfWork.RoleRepository.FindByIds(roleIds);
                    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name)
                    };

                    if (!string.IsNullOrWhiteSpace(user.Phone))
                        userClaims.Add(new Claim(ClaimTypes.HomePhone, user.Phone));

                    var claims = UnitOfWork.ClaimRepository.GetByUser(user).Select(c => new Claim(c.Type, c.Value)).Concat(roleClaims).Concat(userClaims).ToList();
                    if (!string.IsNullOrWhiteSpace(user.FullName))
                        claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
                    return claims;
                },
                cancellationToken);
        }

        /// <summary>   Add claims to a user as an asynchronous operation. </summary>
        /// <param name="user">                 The user to add the claim to. </param>
        /// <param name="claims">               The collection of
        ///                                     <see cref="T:System.Security.Claims.Claim" />s to add. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var identityClaims = claims.Select(c =>
                    new ClaimEntity {UserId = user.Id, Type = c.Type, Value = c.Value});
                UnitOfWork.ClaimRepository.AddRange(identityClaims);
            }, cancellationToken);
        }

        /// <summary>
        ///     Replaces the given <paramref name="claim" /> on the specified <paramref name="user" />
        ///     with the <paramref name="newClaim" />
        /// </summary>
        /// <param name="user">                 The user to replace the claim on. </param>
        /// <param name="claim">                The claim to replace. </param>
        /// <param name="newClaim">             The new claim to replace the existing
        ///                                     <paramref name="claim" /> with. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var entity = UnitOfWork.ClaimRepository.GetByUserAndType(user, claim.Type);
                entity.Type = newClaim.Type;
                entity.Value = newClaim.Value;
                UnitOfWork.ClaimRepository.Update(entity);
            }, cancellationToken);
        }

        /// <summary>
        ///     Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">                 The user to remove the specified
        ///                                     <paramref name="claims" /> from. </param>
        /// <param name="claims">               A collection of
        ///                                     <see cref="T:System.Security.Claims.Claim" />s to remove. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>   The task object representing the asynchronous operation. </returns>
        public Task RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var claimTypes = claims.Select(c => c.Type).ToList();
                var entities = UnitOfWork.ClaimRepository.GetByUser(user).Where(c => claimTypes.Contains(c.Type));
                foreach (var entity in entities)
                    UnitOfWork.ClaimRepository.Delete(entity);
            }, cancellationToken);
        }

        /// <summary>
        ///     Returns a list of users who contain the specified
        ///     <see cref="T:System.Security.Claims.Claim" />.
        /// </summary>
        /// <param name="claim">                The claim to look for. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
        ///     asynchronous query, a list of who contain the specified
        ///     claim.
        /// </returns>
        public Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return Task<IList<UserEntity>>.Factory.StartNew(() =>
            {
                var userIds = UnitOfWork.ClaimRepository.GetUserIdsForClaimType(claim.Type);
                var users = UnitOfWork.UserRepository.FindByIds(userIds);
                return users.ToList();
            }, cancellationToken);
        }

        #endregion
    }
}