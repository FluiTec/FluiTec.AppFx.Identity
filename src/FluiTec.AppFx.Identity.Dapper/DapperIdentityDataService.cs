using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Identity.Dapper
{
    public abstract class DapperIdentityDataService : DapperDataService<DapperIdentityUnitOfWork>, IIdentityDataService
    {
        protected DapperIdentityDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        protected DapperIdentityDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory, IEntityNameService entityNameService) : base(dapperServiceOptions, loggerFactory, entityNameService)
        {
        }

        protected DapperIdentityDataService(IOptionsMonitor<IDapperServiceOptions> dapperServiceOptions, ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        protected DapperIdentityDataService(IOptionsMonitor<IDapperServiceOptions> dapperServiceOptions, ILoggerFactory loggerFactory, IEntityNameService entityNameService) : base(dapperServiceOptions, loggerFactory, entityNameService)
        {
        }

        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }
    }
}