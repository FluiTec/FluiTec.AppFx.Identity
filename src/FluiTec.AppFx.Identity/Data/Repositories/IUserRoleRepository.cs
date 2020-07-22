using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for user role repository. </summary>
    public interface IUserRoleRepository : IWritableKeyTableDataRepository<UserRoleEntity, int>
    {
        /// <summary>   Searches for the first user identifier and role identifier. </summary>
        /// <param name="userId">   Identifier for the user. </param>
        /// <param name="roleId">   Identifier for the role. </param>
        /// <returns>   The found user identifier and role identifier. </returns>
        UserRoleEntity FindByUserIdAndRoleId(Guid userId, Guid roleId);

        /// <summary>   Finds the users in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the users in this collection.
        /// </returns>
        IEnumerable<int> FindByUser(UserEntity user);

        /// <summary>   Finds the roles in this collection. </summary>
        /// <param name="role"> The role. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the roles in this collection.
        /// </returns>
        IEnumerable<int> FindByRole(RoleEntity role);
    }
}