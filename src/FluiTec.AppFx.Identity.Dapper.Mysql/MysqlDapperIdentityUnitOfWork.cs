using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Dapper.Mysql.Repositories;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Mysql
{
    /// <summary>   A mysql dapper identity unit of work. </summary>
    public class MysqlDapperIdentityUnitOfWork : DapperIdentityUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public MysqlDapperIdentityUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public MysqlDapperIdentityUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
        }

        /// <summary>   Registers the repositories. </summary>
        protected override void RegisterRepositories()
        {
            base.RegisterRepositories();
            RepositoryProviders.Add(typeof(IUserRepository),
                (uow, log) => new MysqlDapperUserRepository((DapperIdentityUnitOfWork)uow, log));
        }
    }
}