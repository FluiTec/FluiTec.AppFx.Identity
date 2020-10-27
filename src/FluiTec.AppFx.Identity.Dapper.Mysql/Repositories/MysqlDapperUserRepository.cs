﻿using System.Collections.Generic;
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

        /// <summary>   Finds all claims including duplicates in this collection. </summary>
        /// <param name="user"> The user. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all claims including duplicates
        ///     in this collection.
        /// </returns>
        protected override IEnumerable<ClaimEntity> FindAllClaimsIncludingDuplicates(UserEntity user)
        {
            var command = @"SELECT uclaim.Type, uclaim.Value
                            FROM AppFxIdentity_UserClaim AS uclaim
                            WHERE uclaim.UserId = '6187929c-d82a-43b0-a850-f26557a43f8e'
                            UNION
                            SELECT roleClaim.Type, roleClaim.Value
                            FROM AppFxIdentity_UserRole AS userRole
                            INNER JOIN AppFxIdentity_RoleClaim AS roleClaim
                            ON userRole.RoleId = roleClaim.RoleId
                            WHERE userRole.UserId = '6187929c-d82a-43b0-a850-f26557a43f8e'";
            return UnitOfWork.Connection.Query<ClaimEntity>(command, new {UserId = user.Id}, UnitOfWork.Transaction);
        }
    }
}