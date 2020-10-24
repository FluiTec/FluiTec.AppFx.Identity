using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for role repository. </summary>
    public interface IRoleRepository : IWritableKeyTableDataRepository<RoleEntity, Guid>
    {
        /// <summary>   Gets a role entity using the given identifier. </summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   A RoleEntity. </returns>
        RoleEntity Get(string identifier);

        /// <summary>   Searches for the first normalized name.</summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The found normalized name.</returns>
        RoleEntity FindByNormalizedName(string normalizedName);

        /// <summary>   Finds the names in this collection. </summary>
        /// <param name="names">    The names. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the names in this collection.
        /// </returns>
        IEnumerable<RoleEntity> FindByNames(IEnumerable<string> names);

        /// <summary>   Finds the identifiers in this collection.</summary>
        /// <param name="roleIds">    The roleIds. </param>
        /// <returns>An enumerator that allows foreach to be used to process the identifiers in this
        /// collection.</returns>
        IEnumerable<RoleEntity> FindByIds(IEnumerable<Guid> roleIds);
    }
}
