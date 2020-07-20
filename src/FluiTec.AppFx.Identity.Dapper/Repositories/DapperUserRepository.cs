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
    /// <summary>   A dapper user repository. </summary>
    public class DapperUserRepository : DapperWritableKeyTableDataRepository<UserEntity, int>, IUserRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperUserRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public UserEntity Get(string identifier)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserEntity.));
            return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command,
                new {Identifier = identifier},
                UnitOfWork.Transaction);
        }

        public UserEntity FindByLoweredName(string loweredName)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity FindByNormalizedEmail(string normalizedEmail)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserEntity> FindByIds(IEnumerable<int> userIds)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity FindByLogin(string providerName, string providerKey)
        {
            throw new System.NotImplementedException();
        }
    }
}