using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.NMemory.Repositories
{
    /// <summary>
    /// A memory user login repository.
    /// </summary>
    public class NMemoryUserLoginRepository : NMemoryWritableKeyTableDataRepository<UserLoginEntity, int>, IUserLoginRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryUserLoginRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        ///
        /// <param name="user">                 The user. </param>
        /// <param name="provider">             The provider. </param>
        /// <param name="providerKey">          The provider key. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        public Task DeleteAsync(UserEntity user, string provider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets by user asynchronous.
        /// </summary>
        ///
        /// <param name="user">                 The user. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by user.
        /// </returns>
        public Task<IEnumerable<UserLoginEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = Table.Where(login => login.UserId == user.Id).AsEnumerable();
            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets by provider with key asynchronous.
        /// </summary>
        ///
        /// <param name="provider">             The provider. </param>
        /// <param name="providerKey">          The provider key. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by provider with key.
        /// </returns>
        public Task<UserLoginEntity> GetByProviderWithKeyAsync(string provider, string providerKey, CancellationToken cancellationToken)
        {
            var result = Table
                .Single(login => login.Provider == provider && login.ProviderKey == providerKey);
            return Task.FromResult(result);
        }
    }
}