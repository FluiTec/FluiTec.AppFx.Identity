using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Dapper.Pgsql.Repositories;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql
{
    /// <summary>   A pgsql dapper identity unit of work. </summary>
    public class PgsqlDapperIdentityUnitOfWork : DapperIdentityUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public PgsqlDapperIdentityUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public PgsqlDapperIdentityUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
        }

        /// <summary>   Registers the repositories. </summary>
        protected override void RegisterRepositories()
        {
            base.RegisterRepositories();
            RepositoryProviders.Add(typeof(IUserRepository),
                (uow, log) => new PgsqlDapperUserRepository((DapperIdentityUnitOfWork)uow, log));
        }
    }
}