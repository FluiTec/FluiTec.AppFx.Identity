using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.EntityStores
{
    /// <summary>   A user login store. </summary>
    public class UserLoginStore : UserClaimStore, IUserLoginStore<UserEntity>
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public UserLoginStore(IIdentityDataService dataService) : base(dataService)
        {
        }

        #endregion

        #region IUserLoginStore

        /// <summary>
        ///     Adds an external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to the
        ///     specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">                 The user to add the login to. </param>
        /// <param name="login">                The external
        ///                                     <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" />
        ///                                     to add to the specified <paramref name="user" />. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task AddLoginAsync(UserEntity user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(() =>
            {
                UnitOfWork.LoginRepository.Add(new UserLoginEntity
                {
                    ProviderName = login.LoginProvider,
                    ProviderKey = login.ProviderKey,
                    ProviderDisplayName = login.ProviderDisplayName,
                    UserId = user.Id
                });
                return user;
            }, cancellationToken);
        }

        /// <summary>
        ///     Attempts to remove the provided login information from the specified
        ///     <paramref name="user" />. and returns a flag indicating whether the removal succeed or
        ///     not.
        /// </summary>
        /// <param name="user">                 The user to remove the login information from. </param>
        /// <param name="loginProvider">        The login provide whose information should be removed. </param>
        /// <param name="providerKey">          The key given by the external login provider for the
        ///                                     specified user. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous
        ///     operation.
        /// </returns>
        public Task RemoveLoginAsync(UserEntity user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(
                () => { UnitOfWork.LoginRepository.RemoveByNameAndKey(loginProvider, providerKey); },
                cancellationToken);
        }

        /// <summary>   Retrieves the associated logins for the specified <param ref="user" />. </summary>
        /// <param name="user">                 The user whose associated logins to retrieve. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation,
        ///     containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the
        ///     specified <paramref name="user" />, if any.
        /// </returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task<IList<UserLoginInfo>>.Factory.StartNew(() =>
            {
                var entities = UnitOfWork.LoginRepository.FindByUserId(user.Id);
                return entities.Select(e => new UserLoginInfo(e.ProviderName, e.ProviderKey, e.ProviderDisplayName))
                    .ToList();
            }, cancellationToken);
        }

        /// <summary>
        ///     Retrieves the user associated with the specified login provider and login provider key.
        /// </summary>
        /// <param name="loginProvider">        The login provider who provided the
        ///                                     <paramref name="providerKey" />. </param>
        /// <param name="providerKey">          The key provided by the <paramref name="loginProvider" />
        ///                                     to identify a user. </param>
        /// <param name="cancellationToken">    The <see cref="T:System.Threading.CancellationToken" />
        ///                                     used to propagate notifications that the operation should be
        ///                                     canceled. </param>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation,
        ///     containing the user, if any which matched the specified login provider and key.
        /// </returns>
        public Task<UserEntity> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task<UserEntity>.Factory.StartNew(
                () => UnitOfWork.UserRepository.FindByLogin(loginProvider, providerKey), cancellationToken);
        }

        #endregion
    }
}
