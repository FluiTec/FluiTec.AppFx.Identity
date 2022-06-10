using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.Extensions.Logging;
using NMemory;
using NMemory.Indexes;
using NMemory.Tables;

namespace FluiTec.AppFx.Identity.NMemory
{
    /// <summary>
    /// A service for accessing memory identity data information.
    /// </summary>
    public class NMemoryIdentityDataService : NMemoryDataService<NMemoryIdentityUnitOfWork>, IIdentityDataService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        public NMemoryIdentityDataService(ILoggerFactory loggerFactory, IEntityNameService nameService) : base(loggerFactory, nameService)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="loggerFactory">    The logger factory. </param>
        public NMemoryIdentityDataService(ILoggerFactory? loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public override string Name => nameof(NMemoryIdentityDataService);

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override NMemoryIdentityUnitOfWork BeginUnitOfWork()
        {
            return new NMemoryIdentityUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
        ///
        /// <param name="other">    The other. </param>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override NMemoryIdentityUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is NMemoryUnitOfWork work))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(NMemoryUnitOfWork)}!");
            return new NMemoryIdentityUnitOfWork(work, work.DataService,
                LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <param name="other">    The other. </param>
        /// 
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        IIdentityUnitOfWork IDataService<IIdentityUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>
        /// Configure database.
        /// </summary>
        ///
        /// <param name="database"> The database. </param>
        ///
        /// <returns>
        /// A Database.
        /// </returns>
        protected override Database ConfigureDatabase(Database database)
        {
            // tables
            var roles = database.Tables.Create<RoleEntity, Guid>(e => e.Id);
            var users = database.Tables.Create<UserEntity, Guid>(e => e.Id);
            var userRoles = database.Tables.Create(e => e.Id,
                new IdentitySpecification<UserRoleEntity>(e => e.Id));
            var roleClaims = database.Tables.Create(e => e.Id, 
                new IdentitySpecification<RoleClaimEntity>(e => e.Id));
            var userClaims = database.Tables.Create(e => e.Id, 
                new IdentitySpecification<UserClaimEntity>(e => e.Id));
            var userLogins = database.Tables.Create(e => e.Id, 
                new IdentitySpecification<UserLoginEntity>(e => e.Id));

            // relations
            database.Tables.CreateRelation(roles.PrimaryKeyIndex, 
                userRoles.CreateIndex(new DictionaryIndexFactory(), e => e.RoleId),
                uid => uid, uid => uid);
            database.Tables.CreateRelation(users.PrimaryKeyIndex,
                userRoles.CreateIndex(new DictionaryIndexFactory(), e => e.UserId),
                uid => uid, uid => uid);
            database.Tables.CreateRelation(roles.PrimaryKeyIndex,
                roleClaims.CreateIndex(new DictionaryIndexFactory(), e => e.RoleId),
                uid => uid, uid => uid);
            database.Tables.CreateRelation(users.PrimaryKeyIndex,
                userClaims.CreateIndex(new DictionaryIndexFactory(), e => e.UserId),
                uid => uid, uid => uid);
            database.Tables.CreateRelation(users.PrimaryKeyIndex,
                userLogins.CreateIndex(new DictionaryIndexFactory(), e => e.UserId),
                uid => uid, uid => uid);

            return database;
        }
    }
}