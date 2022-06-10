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
    public abstract class DapperIdentityUnitOfWork : DapperUnitOfWork, IIdentityUnitOfWork
    {
        protected DapperIdentityUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        protected DapperIdentityUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }
        
        protected virtual void RegisterRepositories()
        {
            RegisterRepositoryProvider<IRoleRepository>((uow, logger) 
                => new DapperRoleRepository((DapperUnitOfWork)uow, logger));
            RegisterRepositoryProvider<IUserRepository>((uow, logger) 
                => new DapperUserRepository((DapperUnitOfWork)uow, logger));
            RegisterRepositoryProvider<IUserRoleRepository>((uow, logger) 
                => new DapperUserRoleRepository((DapperUnitOfWork)uow, logger));
            RegisterRepositoryProvider<IRoleClaimRepository>((uow, logger) 
                => new DapperRoleClaimRepository((DapperUnitOfWork)uow, logger));
            RegisterRepositoryProvider<IUserClaimRepository>((uow, logger) 
                => new DapperUserClaimRepository((DapperUnitOfWork)uow, logger));
            RegisterRepositoryProvider<IUserLoginRepository>((uow, logger) 
                => new DapperUserLoginRepository((DapperUnitOfWork)uow, logger));
        }

        public IUserRepository UserRepository => GetRepository<IUserRepository>();
        
        public IRoleRepository RoleRepository => GetRepository<IRoleRepository>();

        public IUserRoleRepository UserRoleRepository => GetRepository<IUserRoleRepository>();

        public IUserClaimRepository UserClaimRepository => GetRepository<IUserClaimRepository>();
        
        public IRoleClaimRepository RoleClaimRepository => GetRepository<IRoleClaimRepository>();
        
        public IUserLoginRepository UserLoginRepository => GetRepository<IUserLoginRepository>();
    }
}