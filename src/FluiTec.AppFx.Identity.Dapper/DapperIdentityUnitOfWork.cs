using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper
{
    /// <summary>   A dapper identity unit of work.</summary>
    public class DapperIdentityUnitOfWork : DapperUnitOfWork, IIdentityUnitOfWork
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public DapperIdentityUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public DapperIdentityUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        #endregion

        #region Methods

        /// <summary>   Registers the repositories.</summary>
        protected virtual void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IUserRepository),
                (uow, log) => new DapperUserRepository((DapperIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserClaimRepository),
                (uow, log) => new DapperUserClaimRepository((DapperIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserLoginRepository),
                (uow, log) => new DapperUserLoginRepository((DapperIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IUserRoleRepository),
                (uow, log) => new DapperUserRoleRepository((DapperIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IRoleRepository),
                (uow, log) => new DapperRoleRepository((DapperIdentityUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IRoleClaimRepository),
                (uow, log) => new DapperRoleClaimRepository((DapperIdentityUnitOfWork)uow, log));
        }

        #endregion

        #region Repositories

        /// <summary>   Gets the user repository.</summary>
        /// <value> The user repository.</value>
        public IUserRepository UserRepository => GetRepository<IUserRepository>();

        /// <summary>   Gets the user login repository.</summary>
        /// <value> The user login repository.</value>
        public IUserLoginRepository LoginRepository => GetRepository<IUserLoginRepository>();

        /// <summary>   Gets the role repository.</summary>
        /// <value> The role repository.</value>
        public IRoleRepository RoleRepository => GetRepository<IRoleRepository>();

        /// <summary>   Gets the user-claim repository.</summary>
        /// <value> The user-claim repository.</value>
        public IUserClaimRepository UserClaimRepository => GetRepository<IUserClaimRepository>();

        /// <summary>   Gets the role-claim repository.</summary>
        /// <value> The role-claim repository.</value>
        public IRoleClaimRepository RoleClaimRepository => GetRepository<IRoleClaimRepository>();

        /// <summary>   Gets the user role repository.</summary>
        /// <value> The user role repository.</value>
        public IUserRoleRepository UserRoleRepository => GetRepository<IUserRoleRepository>();

        #endregion
    }
}
