using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Entities.Base;

namespace FluiTec.AppFx.Identity.Data.Repositories
{
    /// <summary>   Interface for user repository. </summary>
    public interface IUserRepository : IWritableKeyTableDataRepository<UserEntity, Guid>
    {
        /// <summary>   Gets a user entity using the given identifier. </summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   An UserEntity. </returns>
        UserEntity Get(string identifier);

        /// <summary>   Gets an asynchronous.</summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   The asynchronous.</returns>
        Task<UserEntity> GetAsync(string identifier);

        /// <summary>   Searches for the first normalized name. </summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The found normalized name. </returns>
        UserEntity FindByNormalizedName(string normalizedName);

        /// <summary>   Searches for the first normalized name asynchronous.</summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The find by normalized name.</returns>
        Task<UserEntity> FindByNormalizedNameAsync(string normalizedName);

        /// <summary>   Searches for the first normalized email. </summary>
        /// <param name="normalizedEmail">  The normalized email. </param>
        /// <returns>   The found normalized email. </returns>
        UserEntity FindByNormalizedEmail(string normalizedEmail);

        /// <summary>   Searches for the first normalized email asynchronous.</summary>
        /// <param name="normalizedEmail">  The normalized email. </param>
        /// <returns>   The find by normalized email.</returns>
        Task<UserEntity> FindByNormalizedEmailAsync(string normalizedEmail);

        /// <summary>   Searches for the first login. </summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The found login. </returns>
        UserEntity FindByLogin(string providerName, string providerKey);

        /// <summary>   Searches for the first login asynchronous.</summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The find by login.</returns>
        Task<UserEntity> FindByLoginAsync(string providerName, string providerKey);

        /// <summary>   Finds the identifiers in this collection.</summary>
        /// <param name="userIds">  List of identifiers for the users. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the identifiers in this
        ///     collection.
        /// </returns>
        IEnumerable<UserEntity> FindByIds(IEnumerable<Guid> userIds);

        /// <summary>   Searches for the first identifiers asynchronous.</summary>
        /// <param name="userIds">  List of identifiers for the users. </param>
        /// <returns>   The find by identifiers.</returns>
        Task<IEnumerable<UserEntity>> FindByIdsAsync(IEnumerable<Guid> userIds);

        /// <summary>   Finds all claims in this collection.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>An enumerator that allows foreach to be used to process all claims in this collection.</returns>
        IEnumerable<ClaimEntity> FindAllClaims(UserEntity user);

        /// <summary>   Searches for all claims asynchronous.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>   The find all claims.</returns>
        Task<IEnumerable<ClaimEntity>> FindAllClaimsAsync(UserEntity user);
    }
}