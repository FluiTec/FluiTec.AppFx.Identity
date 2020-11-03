using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for role-claim repository.</summary>
    public interface IRoleClaimRepository : IWritableKeyTableDataRepository<RoleClaimEntity, int>
    {
        /// <summary>   Gets the roles in this collection.</summary>
        /// <param name="role"> The role. </param>
        /// <returns>An enumerator that allows foreach to be used to process the roles in this collection.</returns>
        IEnumerable<RoleClaimEntity> GetByRole(RoleEntity role);

        /// <summary>   Gets the role identifiers for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the role identifiers for
        ///     claim types in this collection.
        /// </returns>
        IEnumerable<Guid> GetRoleIdsForClaimType(string claimType);

        /// <summary>   Gets by role and type.</summary>
        /// <param name="role">         The role. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by role and type.</returns>
        RoleClaimEntity GetByRoleAndType(RoleEntity role, string claimType);

        /// <summary>   Gets the users for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the users for claim types in
        ///     this collection.
        /// </returns>
        IEnumerable<UserEntity> GetUsersForClaimType(string claimType);
    }
}