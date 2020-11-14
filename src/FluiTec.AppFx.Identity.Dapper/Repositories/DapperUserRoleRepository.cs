using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>   A dapper user role repository.</summary>
    public class DapperUserRoleRepository : DapperWritableKeyTableDataRepository<UserRoleEntity, int>,
        IUserRoleRepository
    {
        #region Constructors

        /// <summary>   Constructor.</summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperUserRoleRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        #endregion

        #region IUserRoleRepository

        /// <summary>   Searches for the first user identifier and role identifier.</summary>
        /// <param name="userId">   Identifier for the user. </param>
        /// <param name="roleId">   Identifier for the role. </param>
        /// <returns>   The found user identifier and role identifier.</returns>
        public UserRoleEntity FindByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            var command = SqlBuilder.SelectByFilter(EntityType,
                new[] {nameof(UserRoleEntity.UserId), nameof(UserRoleEntity.RoleId)});
            return UnitOfWork.Connection.QuerySingleOrDefault<UserRoleEntity>(command,
                new {UserId = userId, RoleId = roleId}, UnitOfWork.Transaction);
        }

        /// <summary>   Finds the users in this collection.</summary>
        /// <param name="user"> The user. </param>
        /// <returns>An enumerator that allows foreach to be used to process the users in this collection.</returns>
        public IEnumerable<RoleEntity> FindByUser(UserEntity user)
        {
            var cmdUserRoles = SqlBuilder.SelectByFilter(EntityType,
                new[] {nameof(UserRoleEntity.UserId)});
            var userRoles = UnitOfWork.Connection.Query<UserRoleEntity>(cmdUserRoles,
                new {UserId = user.Id}, UnitOfWork.Transaction);

            return UnitOfWork.GetRepository<IRoleRepository>().FindByIds(userRoles.Select(ur => ur.RoleId).ToList());
        }

        /// <summary>   Finds the roles in this collection.</summary>
        /// <param name="role"> The role. </param>
        /// <returns>An enumerator that allows foreach to be used to process the roles in this collection.</returns>
        public IEnumerable<UserEntity> FindByRole(RoleEntity role)
        {
            var cmdUserRoles = SqlBuilder.SelectByFilter(EntityType,
                new[] {nameof(UserRoleEntity.RoleId)});
            var userRoles = UnitOfWork.Connection.Query<UserRoleEntity>(cmdUserRoles,
                new {RoleId = role.Id}, UnitOfWork.Transaction);

            return UnitOfWork.GetRepository<IUserRepository>().FindByIds(userRoles.Select(ur => ur.UserId).ToList());
        }

        /// <summary>   Removes the by user described by user.</summary>
        /// <param name="user"> The user. </param>
        public void RemoveByUser(UserEntity user)
        {
            var command = SqlBuilder.DeleteBy(EntityType, nameof(UserRoleEntity.UserId));
            UnitOfWork.Connection.Execute(command, new {UserId = user.Id}, UnitOfWork.Transaction);
        }

        /// <summary>   Removes the by role described by role.</summary>
        /// <param name="role"> The role. </param>
        public void RemoveByRole(RoleEntity role)
        {
            var command = SqlBuilder.DeleteBy(EntityType, nameof(UserRoleEntity.RoleId));
            UnitOfWork.Connection.Execute(command, new {RoleId = role.Id}, UnitOfWork.Transaction);
        }

        #endregion
    }
}