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
    /// <summary>   A dapper user claim repository.</summary>
    public class DapperUserClaimRepository : DapperWritableKeyTableDataRepository<UserClaimEntity, int>, 
        IUserClaimRepository
    {
        #region Constructors

        /// <summary>   Constructor.</summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperUserClaimRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        #endregion

        #region IUserClaimRepository

        /// <summary>   Gets the users in this collection.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>An enumerator that allows foreach to be used to process the users in this collection.</returns>
        public IEnumerable<UserClaimEntity> GetByUser(UserEntity user)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserClaimEntity.UserId));
            return UnitOfWork.Connection.Query<UserClaimEntity>(command, new { UserId = user.Id },
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets the user identifiers for claim types in this collection.</summary>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>An enumerator that allows foreach to be used to process the user identifiers for
        /// claim types in this collection.</returns>
        public IEnumerable<int> GetUserIdsForClaimType(string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserClaimEntity.Type),
                new[] { nameof(UserClaimEntity.UserId) });
            return UnitOfWork.Connection.Query<int>(command, new { Type = claimType },
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets by user and type.</summary>
        /// <param name="user">         The user. </param>
        /// <param name="claimType">    Type of the claim. </param>
        /// <returns>   The by user and type.</returns>
        public UserClaimEntity GetByUserAndType(UserEntity user, string claimType)
        {
            var command = SqlBuilder.SelectByFilter(EntityType,
                new[] { nameof(UserClaimEntity.Type), nameof(UserClaimEntity.UserId) });
            return UnitOfWork.Connection.QuerySingleOrDefault<UserClaimEntity>(command,
                new { Type = claimType, UserId = user.Id },
                UnitOfWork.Transaction);
        }

        #endregion
    }
}