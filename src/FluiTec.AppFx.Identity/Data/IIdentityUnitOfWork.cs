using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Identity.Data.Repositories;

namespace FluiTec.AppFx.Identity.Data
{
    /// <summary>
    /// Interface for identity unit of work.
    /// </summary>
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        ///
        /// <value>
        /// The user repository.
        /// </value>
        public IUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the role repository.
        /// </summary>
        ///
        /// <value>
        /// The role repository.
        /// </value>
        public IRoleRepository RoleRepository { get; }

        /// <summary>
        /// Gets the user role repository.
        /// </summary>
        ///
        /// <value>
        /// The user role repository.
        /// </value>
        public IUserRoleRepository UserRoleRepository { get; }

        /// <summary>
        /// Gets the user claim repository.
        /// </summary>
        ///
        /// <value>
        /// The user claim repository.
        /// </value>
        public IUserClaimRepository UserClaimRepository { get; }

        /// <summary>
        /// Gets the role claim repository.
        /// </summary>
        ///
        /// <value>
        /// The role claim repository.
        /// </value>
        public IRoleClaimRepository RoleClaimRepository { get; }

        /// <summary>
        /// Gets the user login repository.
        /// </summary>
        ///
        /// <value>
        /// The user login repository.
        /// </value>
        public IUserLoginRepository UserLoginRepository { get; }
    }
}