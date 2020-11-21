using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for user-claim repository. </summary>
    public interface IUserClaimRepository : IWritableKeyTableDataRepository<UserClaimEntity, int>
    {
        /// <summary>   Gets the users in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the users in this collection.
        /// </returns>
        IEnumerable<UserClaimEntity> GetByUser(UserEntity user);

        /// <summary>   Gets by user asynchronous.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>   The by user.</returns>
        Task<IEnumerable<UserClaimEntity>> GetByUserAsync(UserEntity user);

        /// <summary>   Gets the user identifiers for claim types in this collection. </summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the user identifiers for claim
        ///     types in this collection.
        /// </returns>
        IEnumerable<Guid> GetUserIdsForClaimType(string claimType);

        /// <summary>   Gets user identifiers for claim type asynchronous.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The user identifiers for claim type.</returns>
        Task<IEnumerable<Guid>> GetUserIdsForClaimTypeAsync(string claimType);

        /// <summary>   Gets by user and type. </summary>
        /// <param name="user">         The user. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by user and type. </returns>
        UserClaimEntity GetByUserAndType(UserEntity user, string claimType);
    }
}