using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data.Repositories;

namespace FluiTec.AppFx.Identity.Data
{
    /// <summary>   Interface for identity unit of work. </summary>
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        /// <summary>   Gets the user repository. </summary>
        /// <value> The user repository. </value>
        IUserRepository UserRepository { get; }

        /// <summary>   Gets the user login repository. </summary>
        /// <value> The user login repository. </value>
        IUserLoginRepository LoginRepository { get; }

        /// <summary>   Gets the role repository. </summary>
        /// <value> The role repository. </value>
        IRoleRepository RoleRepository { get; }

        /// <summary>   Gets the user-claim repository. </summary>
        /// <value> The user-claim repository. </value>
        IUserClaimRepository UserClaimRepository { get; }

        /// <summary>   Gets the role-claim repository. </summary>
        /// <value> The role-claim repository. </value>
        IRoleClaimRepository RoleClaimRepository { get; }

        /// <summary>   Gets the user role repository. </summary>
        /// <value> The user role repository. </value>
        IUserRoleRepository UserRoleRepository { get; }
    }
}