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
    /// A dapper role claim repository.
    /// </summary>
    public class DapperRoleClaimRepository : DapperWritableKeyTableDataRepository<RoleClaimEntity, int>, IRoleClaimRepository
    {
        public DapperRoleClaimRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task<IEnumerable<RoleClaimEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<RoleClaimEntity>> GetByClaimAsync(BaseClaim claim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}