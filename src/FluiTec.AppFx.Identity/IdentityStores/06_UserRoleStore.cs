using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user role store.
/// </summary>
public class UserRoleStore : RoleStore, IUserRoleStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserRoleStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserRoleStore

    /// <summary>
    /// Add the specified <paramref name="user" /> to the named role.
    /// </summary>
    ///
    /// <param name="user">                 The user to add to the named role. </param>
    /// <param name="roleName">             The name of the role to add the user to. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public async Task AddToRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
    {
        var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper(), cancellationToken);
        await UnitOfWork.UserRoleRepository.AddAsync(new UserRoleEntity { RoleId = role.Id, UserId = user.Id }, cancellationToken);
    }

    /// <summary>
    /// Remove the specified <paramref name="user" /> from the named role.
    /// </summary>
    ///
    /// <param name="user">                 The user to remove the named role from. </param>
    /// <param name="roleName">             The name of the role to remove. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public async Task RemoveFromRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
    {
        var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper(), cancellationToken);
        var userRole = await UnitOfWork.UserRoleRepository.FindByUserIdAndRoleIdAsync(user.Id, role.Id, cancellationToken);
        await UnitOfWork.UserRoleRepository.DeleteAsync(userRole, cancellationToken);
    }

    /// <summary>
    /// Gets a list of role names the specified <paramref name="user" /> belongs to.
    /// </summary>
    ///
    /// <param name="user">                 The user whose role names to retrieve. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing a list of role names.
    /// </returns>
    public async Task<IList<string>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var roles = await UnitOfWork.UserRoleRepository.FindByUserAsync(user, cancellationToken);
        return roles.Select(r => r.NormalizedName).ToList();
    }

    /// <summary>
    /// Returns a flag indicating whether the specified <paramref name="user" /> is a member of the
    /// given named role.
    /// </summary>
    ///
    /// <param name="user">                 The user whose role membership should be checked. </param>
    /// <param name="roleName">             The name of the role to be checked. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing a flag indicating whether the specified <paramref name="user" /> is a member of
    /// the named role.
    /// </returns>
    public async Task<bool> IsInRoleAsync(UserEntity user, string roleName, CancellationToken cancellationToken)
    {
        var roles = await GetRolesAsync(user, cancellationToken);
        return roles.Select(r => r.ToUpperInvariant()).Contains(roleName.ToUpperInvariant());
    }

    /// <summary>
    /// Returns a list of Users who are members of the named role.
    /// </summary>
    ///
    /// <param name="roleName">             The name of the role whose membership should be returned. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing a list of users who are in the named role.
    /// </returns>
    public async Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await UnitOfWork.RoleRepository.FindByNormalizedNameAsync(roleName.ToUpper(), cancellationToken);
        var users = await UnitOfWork.UserRoleRepository.FindByRoleAsync(role, cancellationToken);
        return users.ToList();
    }

    #endregion
}