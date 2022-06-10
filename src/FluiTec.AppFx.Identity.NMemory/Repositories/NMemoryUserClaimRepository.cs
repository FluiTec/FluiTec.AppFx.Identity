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
    /// A memory user claim repository.
    /// </summary>
    public class NMemoryUserClaimRepository : NMemoryWritableKeyTableDataRepository<UserClaimEntity, int>, IUserClaimRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryUserClaimRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
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
        public Task<IEnumerable<UserClaimEntity>> GetByUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = Table.Where(c => c.UserId == user.Id).AsEnumerable();
            return Task.FromResult(result);
        }

        /// <summary>
        /// Replace asynchronous.
        /// </summary>
        ///
        /// <param name="user">                 The user. </param>
        /// <param name="claim">                The claim. </param>
        /// <param name="newClaim">             The new claim. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        public async Task ReplaceAsync(UserEntity user, BaseClaim claim, BaseClaim newClaim, CancellationToken cancellationToken)
        {
            var original = Table.Where(c => c.UserId == user.Id
                                            && c.Type == claim.Type
                                            && c.Value == claim.Value);
            if (original.Any())
            {
                var first = original.First();
                first.Type = newClaim.Type;
                first.Value = newClaim.Value;
                await UpdateAsync(first, cancellationToken);

                // remove duplicates
                if (original.Count() > 2)
                {
                    await DeleteAsync(original.Skip(1), cancellationToken);
                }
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        ///
        /// <param name="userClaims">           The user claims. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        public Task DeleteAsync(IEnumerable<BaseClaim> userClaims, CancellationToken cancellationToken)
        {
            var dbClaims = Table
                .Where(c => userClaims
                    .Any(uc => uc.Type == c.Type && uc.Value == c.Value));

            foreach (var c in dbClaims)
                Table.Delete(c);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets by claim asynchronous.
        /// </summary>
        ///
        /// <param name="claim">                The claim. </param>
        /// <param name="cancellationToken">    A token that allows processing to be cancelled. </param>
        ///
        /// <returns>
        /// The by claim.
        /// </returns>
        public Task<IEnumerable<UserClaimEntity>> GetByClaimAsync(BaseClaim claim, CancellationToken cancellationToken)
        {
            var result = Table
                .Where(c => c.Type == claim.Type && c.Value == claim.Value || claim.Value == null)
                .AsEnumerable();
            return Task.FromResult(result);
        }
    }
}