using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Entities.Base;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.Repositories
{
    /// <summary>   A mssql dapper user repository. </summary>
    public class MssqlDapperUserRepository : DapperUserRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlDapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>   Finds all claims including duplicates in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all claims including duplicates
        ///     in this collection.
        /// </returns>
        protected override IEnumerable<ClaimEntity> FindAllClaimsIncludingDuplicates(UserEntity user)
        {
            var command = @"SELECT uclaim.Type, uclaim.Value
                            FROM [AppFxIdentity].[UserClaim] AS uclaim
                            WHERE uclaim.UserId = @UserId
                            UNION
                            SELECT roleClaim.Type, roleClaim.Value
                            FROM [AppFxIdentity].[UserRole] AS userRole
                            INNER JOIN [AppFxIdentity].[RoleClaim] AS roleClaim
                            ON userRole.RoleId = roleClaim.RoleId
                            WHERE userRole.UserId = @UserId";
            return UnitOfWork.Connection.Query<ClaimEntity>(command, new {UserId = user.Id}, UnitOfWork.Transaction);
        }
    }
}
