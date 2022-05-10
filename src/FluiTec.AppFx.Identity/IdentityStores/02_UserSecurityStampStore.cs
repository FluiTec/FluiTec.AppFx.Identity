using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.IdentityStores;

/// <summary>
/// A user security store.
/// </summary>
public class UserSecurityStampStore : UserStore, IUserSecurityStampStore<UserEntity>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    public UserSecurityStampStore(IIdentityDataService dataService) : base(dataService)
    {
    }

    #endregion

    #region IUserSecurityStampStore

    /// <summary>
    /// Sets the provided security <paramref name="stamp" /> for the specified <paramref name="user" />
    /// .
    /// </summary>
    ///
    /// <param name="user">                 The user whose security stamp should be set. </param>
    /// <param name="stamp">                The security stamp to set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
    /// </returns>
    public Task SetSecurityStampAsync(UserEntity user, string stamp, CancellationToken cancellationToken)
    {
        user.SecurityStamp = stamp;
        return UnitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }

    /// <summary>
    /// Get the security stamp for the specified <paramref name="user" />.
    /// </summary>
    ///
    /// <param name="user">                 The user whose security stamp should be set. </param>
    /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
    ///                                     used to propagate notifications that the operation should be
    ///                                     canceled. </param>
    ///
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
    /// containing the security stamp for the specified <paramref name="user" />.
    /// </returns>
    public Task<string> GetSecurityStampAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.SecurityStamp);
    }

    #endregion
}