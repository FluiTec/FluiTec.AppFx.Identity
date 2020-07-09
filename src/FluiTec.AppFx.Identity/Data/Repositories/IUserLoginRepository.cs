using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for user login repository. </summary>
    public interface IUserLoginRepository : IWritableKeyTableDataRepository<UserLoginEntity, int>
    {
        /// <summary>   Removes the by name and key. </summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        void RemoveByNameAndKey(string providerName, string providerKey);

        /// <summary>   Finds the user identifiers in this collection. </summary>
        /// <param name="userId">   Identifier for the user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the user identifiers in this
        ///     collection.
        /// </returns>
        IEnumerable<UserLoginEntity> FindByUserId(Guid userId);

        /// <summary>   Searches for the first name and key. </summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The found name and key. </returns>
        UserLoginEntity FindByNameAndKey(string providerName, string providerKey);
    }
}