using System;
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
    /// <summary>   A dapper role repository.</summary>
    public abstract class DapperRoleRepository : DapperWritableKeyTableDataRepository<RoleEntity, Guid>, IRoleRepository
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperRoleRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        #endregion

        #region IRoleRepository

        /// <summary>   Gets a role entity using the given identifier.</summary>
        /// <param name="identifier">   The identifier to get. </param>
        /// <returns>   A RoleEntity.</returns>
        public RoleEntity Get(string identifier)
        {
            if (!Guid.TryParse(identifier, out var guidResult)) return null;

            var command = SqlBuilder.SelectByFilter(EntityType, nameof(RoleEntity.Id));
            return UnitOfWork.Connection.QuerySingleOrDefault<RoleEntity>(command,
                new { Id = guidResult },
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first normalized name.</summary>
        /// <param name="normalizedName">   Name of the normalized. </param>
        /// <returns>   The found normalized name.</returns>
        public RoleEntity FindByNormalizedName(string normalizedName)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(RoleEntity.NormalizedName));
            return UnitOfWork.Connection.QuerySingleOrDefault<RoleEntity>(command,
                new { NormalizedName = normalizedName },
                UnitOfWork.Transaction);
        }

        /// <summary>   Finds the identifiers in this collection.</summary>
        /// <param name="roleIds">  List of identifiers for the roles. </param>
        /// <returns>An enumerator that allows foreach to be used to process the identifiers in this
        /// collection.</returns>
        public abstract IEnumerable<RoleEntity> FindByIds(IEnumerable<int> roleIds);

        /// <summary>   Finds the names in this collection.</summary>
        /// <param name="names">    The names. </param>
        /// <returns>An enumerator that allows foreach to be used to process the names in this collection.</returns>
        public abstract IEnumerable<RoleEntity> FindByNames(IEnumerable<string> names);

        #endregion
    }
}