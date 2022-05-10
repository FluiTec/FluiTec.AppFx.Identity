using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user claim store.
/// </summary>
public class UserClaimStore : UserRoleStore, IUserClaimStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserClaimStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserClaimStore

    /// <summary>
    /// Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the
    /// specified <paramref name="user" /> as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="user">                 The role whose claims to retrieve. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
    /// asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
    /// </returns>
    public async Task<IList<Claim>> GetClaimsAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var userClaims = await UnitOfWork.UserClaimRepository.GetByUserAsync(user, cancellationToken);
        var roleClaims = await UnitOfWork.RoleClaimRepository.GetByUserAsync(user, cancellationToken);
        var claimsTotal = userClaims.Cast<BaseClaim>().Concat(roleClaims);
        return claimsTotal.Distinct(new BaseClaimComparer()).Select(c => new Claim(c.Type, c.Value)).ToList();
    }

    /// <summary>
    /// Add claims to a user as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="user">                 The user to add the claim to. </param>
    /// <param name="claims">               The collection of <see cref="T:System.Security.Claims.Claim" />
    ///                                     s to add. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        var userClaims = claims
            .Select(c => new UserClaimEntity {UserId = user.Id, Type = c.Type, Value = c.Value});
        return UnitOfWork.UserClaimRepository.AddRangeAsync(userClaims, cancellationToken);
    }

    /// <summary>
    /// Replaces the given <paramref name="claim" /> on the specified <paramref name="user" /> with
    /// the <paramref name="newClaim" />
    /// </summary>
    ///
    /// <param name="user">                 The user to replace the claim on. </param>
    /// <param name="claim">                The claim to replace. </param>
    /// <param name="newClaim">             The new claim to replace the existing <paramref name="claim" />
    ///                                     with. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        return UnitOfWork.UserClaimRepository.ReplaceAsync(user, new BaseClaim {Type = claim.Type, Value = claim.Value},
            new BaseClaim {Type = newClaim.Type, Value = newClaim.Value}, cancellationToken);
    }

    /// <summary>
    /// Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user to remove the specified <paramref name="claims" />
    ///                                     from. </param>
    /// <param name="claims">               A collection of <see cref="T:System.Security.Claims.Claim" />
    ///                                     s to remove. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public Task RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        var userClaims = claims.Select(c => new BaseClaim {Type = c.Type, Value = c.Value});
        return UnitOfWork.UserClaimRepository.DeleteAsync(userClaims, cancellationToken);
    }

    /// <summary>
    /// Returns a list of users who contain the specified <see cref="T:System.Security.Claims.Claim" />
    /// .
    /// </summary>
    ///
    /// <param name="claim">                The claim to look for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the
    /// asynchronous query, a list of <typeparamref name="TUser" /> who contain the specified claim.
    /// 
    /// </returns>
    public async Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.UserRepository.GetByClaimAsync(new BaseClaim {Type = claim.Type, Value = claim.Value},
                cancellationToken);
        return result.ToList();
    }

    #endregion

    /// <summary>
    /// A base claim comparer.
    /// </summary>
    public class BaseClaimComparer : IEqualityComparer<BaseClaim>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        ///
        /// <param name="x">    The first object of type <paramref name="T" /> to compare. </param>
        /// <param name="y">    The second object of type <paramref name="T" /> to compare. </param>
        ///
        /// <returns>
        /// <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />
        /// .
        /// </returns>
        public bool Equals(BaseClaim x, BaseClaim y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Type == y.Type && x.Value == y.Value;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        ///
        /// <param name="obj">  The <see cref="T:System.Object" /> for which a hash code is to be
        ///                     returned. </param>
        ///
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        public int GetHashCode(BaseClaim obj)
        {
            return HashCode.Combine(obj.Type, obj.Value);
        }
    }
}