using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Repositories;
using FluiTec.AppFx.Identity.NMemory.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.NMemory
{
    /// <summary>
    /// A memory identity unit of work.
    /// </summary>
    public class NMemoryIdentityUnitOfWork : NMemoryUnitOfWork, IIdentityUnitOfWork
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryIdentityUnitOfWork(INMemoryDataService dataService, ILogger<IUnitOfWork>? logger) : base(
            dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public NMemoryIdentityUnitOfWork(NMemoryUnitOfWork parentUnitOfWork, IDataService dataService,
            ILogger<IUnitOfWork>? logger) : base(parentUnitOfWork, dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        ///
        /// <value>
        /// The user repository.
        /// </value>
        public IUserRepository UserRepository => GetRepository<IUserRepository>();

        /// <summary>
        /// Gets the role repository.
        /// </summary>
        ///
        /// <value>
        /// The role repository.
        /// </value>
        public IRoleRepository RoleRepository => GetRepository<IRoleRepository>();

        /// <summary>
        /// Gets the user role repository.
        /// </summary>
        ///
        /// <value>
        /// The user role repository.
        /// </value>
        public IUserRoleRepository UserRoleRepository => GetRepository<IUserRoleRepository>();

        /// <summary>
        /// Gets the user claim repository.
        /// </summary>
        ///
        /// <value>
        /// The user claim repository.
        /// </value>
        public IUserClaimRepository UserClaimRepository => GetRepository<IUserClaimRepository>();

        /// <summary>
        /// Gets the role claim repository.
        /// </summary>
        ///
        /// <value>
        /// The role claim repository.
        /// </value>
        public IRoleClaimRepository RoleClaimRepository => GetRepository<IRoleClaimRepository>();

        /// <summary>
        /// Gets the user login repository.
        /// </summary>
        ///
        /// <value>
        /// The user login repository.
        /// </value>
        public IUserLoginRepository UserLoginRepository => GetRepository<IUserLoginRepository>();

        /// <summary>
        ///     Registers the repositories.
        /// </summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IRoleClaimRepository), (uow, log) 
                => new NMemoryRoleClaimRepository((NMemoryIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserClaimRepository), (uow, log)
                => new NMemoryUserClaimRepository((NMemoryIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IRoleRepository), (uow, log)
                => new NMemoryRoleRepository((NMemoryIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserRepository), (uow, log)
                => new NMemoryUserRepository((NMemoryIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserRoleRepository), (uow, log)
                => new NMemoryUserRoleRepository((NMemoryIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserLoginRepository), (uow, log)
                => new NMemoryUserLoginRepository((NMemoryIdentityUnitOfWork)uow, log));
        }
    }
}