﻿using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for claim repository. </summary>
    public interface IClaimRepository : IWritableKeyTableDataRepository<ClaimEntity, int>
    {
        /// <summary>   Gets the users in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the users in this collection.
        /// </returns>
        IEnumerable<ClaimEntity> GetByUser(UserEntity user);

        /// <summary>   Gets the user identifiers for claim types in this collection. </summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the user identifiers for claim
        ///     types in this collection.
        /// </returns>
        IEnumerable<int> GetUserIdsForClaimType(string claimType);

        /// <summary>   Gets by user and type. </summary>
        /// <param name="user">         The user. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by user and type. </returns>
        ClaimEntity GetByUserAndType(UserEntity user, string claimType);
    }
}