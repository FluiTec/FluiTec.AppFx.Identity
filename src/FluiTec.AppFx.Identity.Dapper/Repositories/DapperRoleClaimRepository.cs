using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
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
        public DapperRoleClaimRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
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
            return UnitOfWork.Connection.Query<RoleClaimEntity>(command, new { RoleId = role.Id },
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets the role identifiers for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>An enumerator that allows foreach to be used to process the role identifiers for
        /// claim types in this collection.</returns>
        public IEnumerable<int> GetRoleIdsForClaimType(string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(RoleClaimEntity.Type),
                new[] { nameof(RoleClaimEntity.RoleId) });
            return UnitOfWork.Connection.Query<int>(command, new { Type = claimType },
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets by role and type.</summary>
        /// <param name="role">         The role. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by role and type.</returns>
        public RoleClaimEntity GetByRoleAndType(RoleEntity role, string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType,
                new[] { nameof(RoleClaimEntity.Type), nameof(RoleClaimEntity.RoleId) });
            return UnitOfWork.Connection.QuerySingleOrDefault<RoleClaimEntity>(command,
                new { Type = claimType, RoleId = role.Id },
                UnitOfWork.Transaction);
        }

        #endregion
    }
}