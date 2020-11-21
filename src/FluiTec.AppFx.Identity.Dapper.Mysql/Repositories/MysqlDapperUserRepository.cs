using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Entities.Base;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Mysql.Repositories
{
    /// <summary>   A mysql dapper user repository. </summary>
    public class MysqlDapperUserRepository : DapperUserRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MysqlDapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>   Gets find all claims including duplicates command.</summary>
        /// <returns>   The find all claims including duplicates command.</returns>
        private string GetFindAllClaimsIncludingDuplicatesCommand()
        {
            return GetFromCache(() =>
                             @"SELECT uclaim.Type, uclaim.Value
                            FROM AppFxIdentity_UserClaim AS uclaim
                            WHERE uclaim.UserId = @UserId
                            UNION
                            SELECT roleClaim.Type, roleClaim.Value
                            FROM AppFxIdentity_UserRole AS userRole
                            INNER JOIN AppFxIdentity_RoleClaim AS roleClaim
                            ON userRole.RoleId = roleClaim.RoleId
                            WHERE userRole.UserId = @UserId");
        }

        /// <summary>   Finds all claims including duplicates in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all claims including duplicates
        ///     in this collection.
        /// </returns>
        protected override IEnumerable<ClaimEntity> FindAllClaimsIncludingDuplicates(UserEntity user)
        {
            return UnitOfWork.Connection.Query<ClaimEntity>(GetFindAllClaimsIncludingDuplicatesCommand(), new {UserId = user.Id}, UnitOfWork.Transaction);
        }

        /// <summary>   Searches for all claims including duplicates asynchronous.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>   The find all claims including duplicates.</returns>
        protected override Task<IEnumerable<ClaimEntity>> FindAllClaimsIncludingDuplicatesAsync(UserEntity user)
        {
            return UnitOfWork.Connection.QueryAsync<ClaimEntity>(GetFindAllClaimsIncludingDuplicatesCommand(), new { UserId = user.Id }, UnitOfWork.Transaction);
        }
    }
}