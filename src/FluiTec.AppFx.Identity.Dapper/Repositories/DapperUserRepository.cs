using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Entities.Base;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>   A dapper user repository. </summary>
    public abstract class DapperUserRepository : DapperWritableKeyTableDataRepository<UserEntity, Guid>, IUserRepository
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
            ExpectIdentityKey = false;
        }

        #endregion

        #region IUserRepository

        /// <summary>   Gets a user entity using the given identifier. </summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   An UserEntity. </returns>
        public UserEntity Get(string identifier)
        {
            if (!Guid.TryParse(identifier, out var guidResult)) return null;

            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.Id));
            return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command,
                new {Id = guidResult},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets an asynchronous.</summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   The asynchronous.</returns>
        public Task<UserEntity> GetAsync(string identifier)
        {
            if (!Guid.TryParse(identifier, out var guidResult)) return null;

            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.Id));
            return UnitOfWork.Connection.QuerySingleOrDefaultAsync<UserEntity>(command,
                new { Id = guidResult },
                UnitOfWork.Transaction);
        }
        
        /// <summary>   Searches for the first normalized name. </summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The found normalized name. </returns>
        public UserEntity FindByNormalizedName(string normalizedName)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.NormalizedName));
            return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command,
                new {NormalizedName = normalizedName},
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first normalized name asynchronous.</summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The find by normalized name.</returns>
        public Task<UserEntity> FindByNormalizedNameAsync(string normalizedName)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.NormalizedName));
            return UnitOfWork.Connection.QuerySingleOrDefaultAsync<UserEntity>(command,
                new { NormalizedName = normalizedName },
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first normalized email. </summary>
        /// <param name="normalizedEmail">  The normalized email. </param>
        /// <returns>   The found normalized email. </returns>
        public UserEntity FindByNormalizedEmail(string normalizedEmail)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.NormalizedEmail));
            return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command,
                new {NormalizedEmail = normalizedEmail},
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first normalized email asynchronous.</summary>
        /// <param name="normalizedEmail">  The normalized email. </param>
        /// <returns>   The find by normalized email.</returns>
        public Task<UserEntity> FindByNormalizedEmailAsync(string normalizedEmail)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.NormalizedEmail));
            return UnitOfWork.Connection.QuerySingleOrDefaultAsync<UserEntity>(command,
                new { NormalizedEmail = normalizedEmail },
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets find by login command.</summary>
        /// <returns>   The find by login command.</returns>
        private string GetFindByLoginCommand()
        {
            return GetFromCache(() =>
            {
                var sql = SqlBuilder.Adapter;
                return
                    $"SELECT {SqlBuilder.Adapter.RenderPropertyList(EntityType, SqlCache.TypePropertiesChache(typeof(UserEntity)).ToArray())} FROM {sql.RenderTableName(EntityType)} " +
                    $"INNER JOIN {sql.RenderTableName(typeof(UserLoginEntity))} ON " +
                    $"{sql.RenderTableName(EntityType)}.{sql.RenderPropertyName(nameof(UserEntity.Id))} = {sql.RenderTableName(typeof(UserLoginEntity))}.{sql.RenderPropertyName(nameof(UserLoginEntity.UserId))} " +
                    $"WHERE {sql.RenderTableName(typeof(UserLoginEntity))}.{sql.RenderPropertyName(nameof(UserLoginEntity.ProviderName))} = @ProviderName " +
                    $"AND {sql.RenderTableName(typeof(UserLoginEntity))}.{sql.RenderPropertyName(nameof(UserLoginEntity.ProviderKey))} = @ProviderKey";
            });
        }

        /// <summary>   Searches for the first login. </summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The found login. </returns>
        public virtual UserEntity FindByLogin(string providerName, string providerKey)
        {
            return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(GetFindByLoginCommand(),
                new {ProviderName = providerName, ProviderKey = providerKey},
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first login asynchronous.</summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The find by login.</returns>
        public virtual Task<UserEntity> FindByLoginAsync(string providerName, string providerKey)
        {
            return UnitOfWork.Connection.QuerySingleOrDefaultAsync<UserEntity>(GetFindByLoginCommand(),
                new { ProviderName = providerName, ProviderKey = providerKey },
                UnitOfWork.Transaction);
        }

        /// <summary>   Finds the identifiers in this collection.</summary>
        /// <param name="userIds">  List of identifiers for the users. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the identifiers in this
        ///     collection.
        /// </returns>
        public IEnumerable<UserEntity> FindByIds(IEnumerable<Guid> userIds)
        {
            var command = SqlBuilder.SelectByInFilter(EntityType, nameof(UserEntity.Id), "UserIds");
            return UnitOfWork.Connection.Query<UserEntity>(command,
                new {UserIds = userIds}, UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first identifiers asynchronous.</summary>
        /// <param name="userIds">  List of identifiers for the users. </param>
        /// <returns>   The find by identifiers.</returns>
        public Task<IEnumerable<UserEntity>> FindByIdsAsync(IEnumerable<Guid> userIds)
        {
            var command = SqlBuilder.SelectByInFilter(EntityType, nameof(UserEntity.Id), "UserIds");
            return UnitOfWork.Connection.QueryAsync<UserEntity>(command,
                new { UserIds = userIds }, UnitOfWork.Transaction);
        }

        /// <summary>   Finds all claims including duplicates in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all claims including duplicates
        ///     in this collection.
        /// </returns>
        protected abstract IEnumerable<ClaimEntity> FindAllClaimsIncludingDuplicates(UserEntity user);

        /// <summary>   Searches for all claims including duplicates asynchronous.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>   The find all claims including duplicates.</returns>
        protected abstract Task<IEnumerable<ClaimEntity>> FindAllClaimsIncludingDuplicatesAsync(UserEntity user);

        /// <summary>   Finds all claims in this collection.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>An enumerator that allows foreach to be used to process all claims in this collection.</returns>
        public IEnumerable<ClaimEntity> FindAllClaims(UserEntity user)
        {
            var claims = FindAllClaimsIncludingDuplicates(user);

            var distinctUserClaims = new List<ClaimEntity>();
            foreach (var claim in claims)
                if (distinctUserClaims.All(c => c.Type != claim.Type))
                    distinctUserClaims.Add(claim);
            return distinctUserClaims;
        }

        /// <summary>   Searches for all claims asynchronous.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>   The find all claims.</returns>
        public async Task<IEnumerable<ClaimEntity>> FindAllClaimsAsync(UserEntity user)
        {
            var claims = await FindAllClaimsIncludingDuplicatesAsync(user);

            var distinctUserClaims = new List<ClaimEntity>();
            foreach (var claim in claims)
                if (distinctUserClaims.All(c => c.Type != claim.Type))
                    distinctUserClaims.Add(claim);
            return distinctUserClaims;
        }

        #endregion
    }
}