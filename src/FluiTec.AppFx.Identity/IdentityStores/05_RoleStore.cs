using System;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A role store.
/// </summary>
public class RoleStore : UserSecurityStampStore, IRoleStore<RoleEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public RoleStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IRoleStore

    /// <summary>
    /// Creates a new role in a store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role to create in the store. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
    /// of the asynchronous query.
    /// </returns>
    async Task<IdentityResult> IRoleStore<RoleEntity>.CreateAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.RoleRepository.AddAsync(role, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError {Description = e.ToString()});
        }
    }

    /// <summary>
    /// Updates a role in a store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role to update in the store. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
    /// of the asynchronous query.
    /// </returns>
    async Task<IdentityResult> IRoleStore<RoleEntity>.UpdateAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.RoleRepository.UpdateAsync(role, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.ToString() });
        }
    }

    /// <summary>
    /// Deletes a role from the store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role to delete from the store. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
    /// of the asynchronous query.
    /// </returns>
    public virtual async Task<IdentityResult> DeleteAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: remove related entities
            await UnitOfWork.RoleRepository.DeleteAsync(role, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.ToString() });
        }
    }

    /// <summary>
    /// Gets the ID for a role from the store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role whose ID should be returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the ID of the role.
    /// </returns>
    Task<string> IRoleStore<RoleEntity>.GetRoleIdAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id.ToString());
    }

    /// <summary>
    /// Gets the name of a role from the store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role whose name should be returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
    /// </returns>
    Task<string> IRoleStore<RoleEntity>.GetRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name);
    }

    /// <summary>
    /// Sets the name of a role in the store as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role whose name should be set. </param>
    /// <param name="roleName">             The name of the role. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    Task IRoleStore<RoleEntity>.SetRoleNameAsync(RoleEntity role, string roleName, CancellationToken cancellationToken)
    {
        role.Name = roleName;
        return UnitOfWork.RoleRepository.UpdateAsync(role, cancellationToken);
    }

    /// <summary>
    /// Get a role's normalized name as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role whose normalized name should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
    /// </returns>
    Task<string> IRoleStore<RoleEntity>.GetNormalizedRoleNameAsync(RoleEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.NormalizedName);
    }

    /// <summary>
    /// Set a role's normalized name as an asynchronous operation.
    /// </summary>
    ///
    /// <param name="role">                 The role whose normalized name should be set. </param>
    /// <param name="normalizedName">       The normalized name to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    Task IRoleStore<RoleEntity>.SetNormalizedRoleNameAsync(RoleEntity role, string normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Finds the role who has the specified ID as an asynchronous operation.
    /// </summary>
    /// <param name="roleId">               The role ID to look for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    /// 
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
    /// </returns>
    async Task<RoleEntity> IRoleStore<RoleEntity>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(roleId, out var uid))
        {
            return await UnitOfWork.RoleRepository.GetAsync(uid, cancellationToken);
        }

        return null;
    }

    /// <summary>
    /// Finds the role who has the specified normalized name as an asynchronous operation.
    /// </summary>
    /// <param name="normalizedRoleName">   The normalized role name to look for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should
    ///                                     be canceled. </param>
    /// 
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
    /// </returns>
    Task<RoleEntity> IRoleStore<RoleEntity>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return UnitOfWork.RoleRepository.FindByNormalizedNameAsync(normalizedRoleName, cancellationToken);
    }

    #endregion
}