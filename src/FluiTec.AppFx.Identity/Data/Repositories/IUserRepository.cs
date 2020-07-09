using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for user repository. </summary>
    public interface IUserRepository : IWritableKeyTableDataRepository<UserEntity, int>
    {
        /// <summary>   Gets a user entity using the given identifier. </summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   An UserEntity. </returns>
        UserEntity Get(string identifier);

        /// <summary>   Searches for the first lowered name. </summary>
        /// <param name="loweredName">  Name of the lowered. </param>
        /// <returns>   The found lowered name. </returns>
        UserEntity FindByLoweredName(string loweredName);

        /// <summary>   Searches for the first normalized email. </summary>
        /// <param name="normalizedEmail">  The normalized email. </param>
        /// <returns>   The found normalized email. </returns>
        UserEntity FindByNormalizedEmail(string normalizedEmail);

        /// <summary>   Finds the identifiers in this collection. </summary>
        /// <param name="userIds">  List of identifiers for the users. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the identifiers in this
        ///     collection.
        /// </returns>
        IEnumerable<UserEntity> FindByIds(IEnumerable<int> userIds);

        /// <summary>   Searches for the first login. </summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The found login. </returns>
        UserEntity FindByLogin(string providerName, string providerKey);
    }
}