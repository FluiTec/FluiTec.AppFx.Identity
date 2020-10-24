﻿using System;
using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
    /// <summary>   A dapper user login repository.</summary>
    public class DapperUserLoginRepository : DapperWritableKeyTableDataRepository<UserLoginEntity, int>, IUserLoginRepository
    {
        /// <summary>   Constructor.</summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperUserLoginRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>   Removes the by name and key.</summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        public void RemoveByNameAndKey(string providerName, string providerKey)
        {
            var sql = SqlBuilder.Adapter;

            var command =
                $"DELETE FROM {sql.RenderTableName(EntityType)} WHERE {sql.RenderPropertyName(nameof(UserLoginEntity.ProviderName))} = @ProviderName " +
                $"AND {sql.RenderPropertyName(nameof(UserLoginEntity.ProviderKey))} = @ProviderKey";
            UnitOfWork.Connection.Execute(command, new { ProviderName = providerName, ProviderKey = providerKey },
                UnitOfWork.Transaction);
        }

        /// <summary>   Finds the user identifiers in this collection.</summary>
        /// <param name="userId">   Identifier for the user. </param>
        /// <returns>An enumerator that allows foreach to be used to process the user identifiers in this
        /// collection.</returns>
        public IEnumerable<UserLoginEntity> FindByUserId(Guid userId)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(UserLoginEntity.UserId));
            return UnitOfWork.Connection.Query<UserLoginEntity>(command, new { UserId = userId },
                UnitOfWork.Transaction);
        }

        /// <summary>   Searches for the first name and key.</summary>
        /// <param name="providerName"> Name of the provider. </param>
        /// <param name="providerKey">  The provider key. </param>
        /// <returns>   The found name and key.</returns>
        public UserLoginEntity FindByNameAndKey(string providerName, string providerKey)
        {
            var command = SqlBuilder.SelectByFilter(EntityType,
                new[] { nameof(UserLoginEntity.ProviderName), nameof(UserLoginEntity.ProviderKey) });
            return UnitOfWork.Connection.QuerySingleOrDefault<UserLoginEntity>(command,
                new { ProviderName = providerName, ProviderKey = providerKey }, UnitOfWork.Transaction);
        }
    }
}
