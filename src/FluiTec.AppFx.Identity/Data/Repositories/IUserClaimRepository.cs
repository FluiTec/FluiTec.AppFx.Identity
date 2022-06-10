using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories;

/// <summary>
/// Interface for user claim repository.
/// </summary>
public interface IUserClaimRepository : IWritableKeyTableDataRepository<UserClaimEntity, int>
{
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
    Task<IEnumerable<UserClaimEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken);

    /// <summary>
    /// Replace asynchronous.
    /// </summary>
    ///
    /// <param name="user">                 The user. </param>
    /// <param name="claim">                The claim. </param>
    /// <param name="newClaim">             The new claim. </param>
    /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A Task.
    /// </returns>
    Task ReplaceAsync(UserEntity user, BaseClaim claim, BaseClaim newClaim, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    ///
    /// <param name="userClaims">           The user claims. </param>
    /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A Task.
    /// </returns>
    Task DeleteAsync(IEnumerable<BaseClaim> userClaims, CancellationToken cancellationToken);

    /// <summary>
    /// Gets by claim asynchronous.
    /// </summary>
    ///
    /// <param name="claim">                The claim. </param>
    /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The by claim.
    /// </returns>
    Task<IEnumerable<UserClaimEntity>> GetByClaimAsync(BaseClaim claim, CancellationToken cancellationToken);
}