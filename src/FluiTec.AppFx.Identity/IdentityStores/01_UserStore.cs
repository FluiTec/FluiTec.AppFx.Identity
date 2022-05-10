using System;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user store.
/// </summary>
public class UserStore : IdentityStore, IUserStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserStore(IIdentityDataService dataService) : base(dataService)
    {

    }

    #endregion

    #region Read-NoDb

    /// <summary>
    /// Gets the user identifier for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose identifier should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the identifier for the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken) 
        => Task.FromResult(user.Id.ToString());

    /// <summary>
    /// Gets the user name for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose name should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the name for the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        => Task.FromResult(user.Email);

    /// <summary>
    /// Gets the normalized user name for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose normalized name should be retrieved. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the normalized user name for the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        => Task.FromResult(user.NormalizedEmail);

    #endregion

    #region Write

    /// <summary>
    /// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose name should be set. </param>
    /// <param name="userName">             The user name to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
    {
        user.Email = userName;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Sets the given normalized name for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose name should be set. </param>
    /// <param name="normalizedName">       The normalized name to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedName;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    #endregion

    #region CRUD

    /// <summary>
    /// Creates the specified <paramref name="user" /> in the user store.
    /// </summary>
    ///
    /// <param name="user">                 The user to create. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the creation
    /// operation.
    /// </returns>
    public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.UserRepository.AddAsync(user, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.ToString() });
        }
    }

    /// <summary>
    /// Updates the specified <paramref name="user" /> in the user store.
    /// </summary>
    ///
    /// <param name="user">                 The user to update. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update
    /// operation.
    /// </returns>
    public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.ToString() });
        }
    }

    /// <summary>
    /// Deletes the specified <paramref name="user" /> from the user store.
    /// </summary>
    ///
    /// <param name="user">                 The user to delete. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the delete
    /// operation.
    /// </returns>
    public virtual async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: remove related entities
            await UnitOfWork.UserRepository.DeleteAsync(user, cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.ToString() });
        }
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
    /// </summary>
    ///
    /// <param name="userId">               The user ID to search for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the user matching the specified <paramref name="userId" /> if it exists.
    /// </returns>
    public async Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(userId, out var uid))
        {
            return await UnitOfWork.UserRepository.GetAsync(uid, cancellationToken);
        }

        return null;
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified normalized user name.
    /// </summary>
    ///
    /// <param name="normalizedUserName">   The normalized user name to search for. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should
    ///                                     be canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the user matching the specified <paramref name="normalizedUserName" /> if it
    /// exists.
    /// </returns>
    public async Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await UnitOfWork.UserRepository.FindByNormalizedNameAsync(normalizedUserName, cancellationToken);
    }

    #endregion
}