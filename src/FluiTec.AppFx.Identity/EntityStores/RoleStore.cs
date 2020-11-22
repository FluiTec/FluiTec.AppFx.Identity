using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.EntityStores
{
    /// <summary>   A role store. </summary>
    public class RoleStore : UserSecurityStore, IRoleStore<RoleEntity>, IUserRoleStore<UserEntity>
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public RoleStore(IIdentityDataService dataService) : base(dataService)
        {
        }

        #endregion

        #region IRoleStore

        /// <summary>   Creates a new role in a store as an asynchronous operation. </summary>
        /// <param name="role">                 The role to create in the store. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the
        ///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public async Task<IdentityResult> CreateAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            try
            {
                await UnitOfWork.RoleRepository.AddAsync(role);
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        /// <summary>   Finds the role who has the specified ID as an asynchronous operation. </summary>
        /// <param name="roleId">               The role ID to look for. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
        /// </returns>
        async Task<RoleEntity> IRoleStore<RoleEntity>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await UnitOfWork.RoleRepository.GetAsync(roleId);
        }

        /// <summary>
        ///     Finds the role who has the specified normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="normalizedRoleName">   The normalized role name to look for. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should
        ///     be canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
        /// </returns>
        async Task<RoleEntity> IRoleStore<RoleEntity>.FindByNameAsync(string normalizedRoleName,
            CancellationToken cancellationToken)
        {
            return await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(normalizedRoleName?.ToUpper());
        }

        /// <summary>   Updates a role in a store as an asynchronous operation. </summary>
        /// <param name="role">                 The role to update in the store. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the
        ///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public async Task<IdentityResult> UpdateAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            try
            {
                await UnitOfWork.RoleRepository.UpdateAsync(role);
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        /// <summary>   Deletes a role from the store as an asynchronous operation. </summary>
        /// <param name="role">                 The role to delete from the store. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the
        ///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public async Task<IdentityResult> DeleteAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            try
            {
                await UnitOfWork.UserRoleRepository.RemoveByRoleAsync(role);
                await UnitOfWork.RoleRepository.DeleteAsync(role);
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        /// <summary>   Gets the ID for a role from the store as an asynchronous operation. </summary>
        /// <param name="role">                 The role whose ID should be returned. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the ID of the role.
        /// </returns>
        public Task<string> GetRoleIdAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        /// <summary>   Gets the name of a role from the store as an asynchronous operation. </summary>
        /// <param name="role">                 The role whose name should be returned. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
        /// </returns>
        public Task<string> GetRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        /// <summary>   Sets the name of a role in the store as an asynchronous operation. </summary>
        /// <param name="role">                 The role whose name should be set. </param>
        /// <param name="roleName">             The name of the role. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public async Task SetRoleNameAsync(RoleEntity role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await UnitOfWork.RoleRepository.UpdateAsync(role);
        }

        /// <summary>   Get a role's normalized name as an asynchronous operation. </summary>
        /// <param name="role">                 The role whose normalized name should be retrieved. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
        /// </returns>
        public Task<string> GetNormalizedRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        /// <summary>   Set a role's normalized name as an asynchronous operation. </summary>
        /// <param name="role">                 The role whose normalized name should be set. </param>
        /// <param name="normalizedName">       The normalized name to set. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task SetNormalizedRoleNameAsync(RoleEntity role, string normalizedName,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserRoleStore

        /// <summary>   Add the specified <paramref name="user" /> to the named role. </summary>
        /// <param name="user">                 The user to add to the named role. </param>
        /// <param name="roleName">             The name of the role to add the user to. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public async Task AddToRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
        {
            var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper());
            await UnitOfWork.UserRoleRepository.AddAsync(new UserRoleEntity { RoleId = role.Id, UserId = user.Id });
        }

        /// <summary>   Remove the specified <paramref name="user" /> from the named role. </summary>
        /// <param name="user">                 The user to remove the named role from. </param>
        /// <param name="roleName">             The name of the role to remove. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public async Task RemoveFromRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
        {
            var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper());
            var userRole = await UnitOfWork.UserRoleRepository.FindByUserIdAndRoleIdAsync(user.Id, role.Id);
            await UnitOfWork.UserRoleRepository.DeleteAsync(userRole);
        }

        /// <summary>
        ///     Gets a list of role names the specified <paramref name="user" /> belongs to.
        /// </summary>
        /// <param name="user">                 The user whose role names to retrieve. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing a list of role names.
        /// </returns>
        public async Task<IList<string>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var userRoles = await UnitOfWork.UserRoleRepository.FindByUserAsync(user);
            return userRoles.Select(r => r.Name).ToList();
        }

        /// <summary>
        ///     Returns a flag indicating whether the specified <paramref name="user" /> is a member of
        ///     the given named role.
        /// </summary>
        /// <param name="user">                 The user whose role membership should be checked. </param>
        /// <param name="roleName">             The name of the role to be checked. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing a flag indicating whether the specified <paramref name="user" /> is
        ///     a member of the named role.
        /// </returns>
        public async Task<bool> IsInRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
        {
            var roles = await GetRolesAsync(user, cancellationToken);
            return roles.Contains(roleName);
        }

        /// <summary>   Returns a list of Users who are members of the named role. </summary>
        /// <param name="roleName">             The name of the role whose membership should be returned. </param>
        /// <param name="cancellationToken">
        ///     The <see cref="T:System.Threading.CancellationToken" />
        ///     used to propagate notifications that the operation should be
        ///     canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation, containing a list of users who are in the named role.
        /// </returns>
        public async Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper());
            var userRoles = await UnitOfWork.UserRoleRepository.FindByRoleAsync(role);
            return userRoles.ToList();
        }

        #endregion
    }
}