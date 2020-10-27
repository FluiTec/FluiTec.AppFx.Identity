﻿using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Entities.Base;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.Repositories
{
    /// <summary>   A pgsql dapper user repository. </summary>
    public class PgsqlDapperUserRepository : DapperUserRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public PgsqlDapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
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
            var command = @"SELECT ""uclaim"".""Type"", ""uclaim"".""Value""
                            FROM ""AppFxIdentity"".""UserClaim"" AS uclaim
                            WHERE ""uclaim"".""UserId"" = '54108773-bf98-4b4e-b3dc-b0256a90ae0b'
                            UNION
                            SELECT roleClaim.""Type"", roleClaim.""Value""
                            FROM ""AppFxIdentity"".""UserRole"" AS userRole
                            INNER JOIN ""AppFxIdentity"".""RoleClaim"" AS roleClaim
                            ON userRole.""RoleId"" = roleClaim.""RoleId""
                            WHERE userRole.""UserId"" = '54108773-bf98-4b4e-b3dc-b0256a90ae0b'";
            return UnitOfWork.Connection.Query<ClaimEntity>(command, new {UserId = user.Id}, UnitOfWork.Transaction);
        }
    }
}