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
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>   A dapper role claim repository.</summary>
    public class DapperRoleClaimRepository : DapperWritableKeyTableDataRepository<RoleClaimEntity, int>,
        IRoleClaimRepository
    {
        #region Constructors

        /// <summary>   Constructor.</summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperRoleClaimRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        #endregion

        #region IRoleClaimRepository

        /// <summary>   Gets the roles in this collection.</summary>
        /// <param name="role"> The role. </param>
        /// <returns>An enumerator that allows foreach to be used to process the roles in this collection.</returns>
        public IEnumerable<RoleClaimEntity> GetByRole(RoleEntity role)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(RoleClaimEntity.RoleId));
            return UnitOfWork.Connection.Query<RoleClaimEntity>(command, new {RoleId = role.Id},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets the role identifiers for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the role identifiers for
        ///     claim types in this collection.
        /// </returns>
        public IEnumerable<Guid> GetRoleIdsForClaimType(string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(RoleClaimEntity.Type),
                new[] {nameof(RoleClaimEntity.RoleId)});
            return UnitOfWork.Connection.Query<Guid>(command, new {Type = claimType},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets by role and type.</summary>
        /// <param name="role">         The role. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by role and type.</returns>
        public RoleClaimEntity GetByRoleAndType(RoleEntity role, string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType,
                new[] {nameof(RoleClaimEntity.Type), nameof(RoleClaimEntity.RoleId)});
            return UnitOfWork.Connection.QuerySingleOrDefault<RoleClaimEntity>(command,
                new {Type = claimType, RoleId = role.Id},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets users for claim type command.</summary>
        /// <returns>   The users for claim type command.</returns>
        private string GetUsersForClaimTypeCommand()
        {
            return GetFromCache(() =>
            {
                var sql = SqlBuilder.Adapter;
                return
                    $"SELECT {SqlBuilder.Adapter.RenderPropertyList(typeof(UserEntity), SqlCache.TypePropertiesChache(typeof(UserEntity)).ToArray())} " +
                    $"FROM {sql.RenderTableName(typeof(UserEntity))} " +
                    $"INNER JOIN {sql.RenderTableName(typeof(UserRoleEntity))} " +
                    $"ON {sql.RenderTableName(typeof(UserRoleEntity))}.{sql.RenderPropertyName(nameof(UserRoleEntity.UserId))} = {sql.RenderTableName(typeof(UserEntity))}.{sql.RenderPropertyName(nameof(UserEntity.Id))} " +
                    $"INNER JOIN {sql.RenderTableName(typeof(RoleClaimEntity))} " +
                    $"ON {sql.RenderTableName(typeof(RoleClaimEntity))}.{sql.RenderPropertyName(nameof(RoleClaimEntity.RoleId))} = {sql.RenderTableName(typeof(UserRoleEntity))}.{sql.RenderPropertyName(nameof(UserRoleEntity.RoleId))} " +
                    $"WHERE {sql.RenderTableName(typeof(RoleClaimEntity))}.{sql.RenderPropertyName(nameof(RoleClaimEntity.Type))} = @ClaimType";
            });
        }

        /// <summary>   Gets the users for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the users for claim types in
        ///     this collection.
        /// </returns>
        public IEnumerable<UserEntity> GetUsersForClaimType(string claimType)
        {
            return UnitOfWork.Connection.Query<UserEntity>(GetUsersForClaimTypeCommand(),
                new {ClaimType = claimType},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets users for claim type asynchronous.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The users for claim type.</returns>
        public Task<IEnumerable<UserEntity>> GetUsersForClaimTypeAsync(string claimType)
        {
            return UnitOfWork.Connection.QueryAsync<UserEntity>(GetUsersForClaimTypeCommand(),
                new { ClaimType = claimType },
                UnitOfWork.Transaction);
        }

        #endregion
    }
}