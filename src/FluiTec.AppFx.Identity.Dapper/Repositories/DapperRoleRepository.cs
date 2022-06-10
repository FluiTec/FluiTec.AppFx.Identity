using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>
    ///     Repository for roles using dapper.
    /// </summary>
    public class DapperRoleRepository : DapperSequentialGuidRepository<RoleEntity>, IRoleRepository
    {
        public DapperRoleRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task<RoleEntity> FindByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleEntity>> GetByRoleIdsAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}