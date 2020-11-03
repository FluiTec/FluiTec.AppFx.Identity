using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Schema;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper
{
    /// <summary>   A service for accessing dapper identity data information.</summary>
    public abstract class DapperIdentityDataService : DapperDataService<DapperIdentityUnitOfWork>, IIdentityDataService
    {
        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected DapperIdentityDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) :
            base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Gets the schema.</summary>
        /// <value> The schema.</value>
        public override string Schema => SchemaGlobals.Schema;

        /// <summary>   Begins unit of work.</summary>
        /// <returns>   A TUnitOfWork.</returns>
        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>   Begins unit of work.</summary>
        /// <param name="other">    The other. </param>
        /// <returns>   A TUnitOfWork.</returns>
        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }
    }
}