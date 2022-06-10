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
    /// A dapper user role repository.
    /// </summary>
    public class DapperUserRoleRepository : DapperWritableKeyTableDataRepository<UserRoleEntity, int>, IUserRoleRepository
    {
        public DapperUserRoleRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public Task<UserRoleEntity> FindByUserIdAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleEntity>> FindByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserEntity>> FindByRoleAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserEntity>> FindByRolesAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}