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

        /// <summary>   Searches for the first lowered name. </summary>
        /// <param name="loweredName">  Name of the lowered. </param>
        /// <returns>   The found lowered name. </returns>
        RoleEntity FindByLoweredName(string loweredName);

        /// <summary>   Finds the identifiers in this collection. </summary>
        /// <param name="roleIds">  List of identifiers for the roles. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the identifiers in this
        ///     collection.
        /// </returns>
        IEnumerable<RoleEntity> FindByIds(IEnumerable<int> roleIds);

        /// <summary>   Finds the names in this collection. </summary>
        /// <param name="names">    The names. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the names in this collection.
        /// </returns>
        IEnumerable<RoleEntity> FindByNames(IEnumerable<string> names);
    }
}
