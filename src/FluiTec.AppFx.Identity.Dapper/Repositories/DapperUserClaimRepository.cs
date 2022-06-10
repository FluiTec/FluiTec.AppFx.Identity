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
    /// A dapper user claim repository.
    /// </summary>
    public class DapperUserClaimRepository : DapperWritableKeyTableDataRepository<UserClaimEntity, int>, IUserClaimRepository
    {
        public DapperUserClaimRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task<IEnumerable<UserClaimEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task ReplaceAsync(UserEntity user, BaseClaim claim, BaseClaim newClaim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<BaseClaim> userClaims, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserClaimEntity>> GetByClaimAsync(BaseClaim claim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}