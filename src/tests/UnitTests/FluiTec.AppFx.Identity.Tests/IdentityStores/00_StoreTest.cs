using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using FluiTec.AppFx.Identity.NMemory;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
///     A store test.
/// </summary>
/// <typeparam name="TStore">   Type of the store. </typeparam>
public abstract class StoreTest<TStore>
{
    #region Constructors

    /// <summary>
    ///     Specialized default constructor for use only by derived class.
    /// </summary>
#pragma warning disable CS8618
    protected StoreTest()
#pragma warning restore CS8618
    {
        DataService = new NMemoryIdentityDataService(null);
        // ReSharper disable once VirtualMemberCallInConstructor
        Initialize();
    }

    #endregion

    #region Fields

    /// <summary>
    ///     The store.
    /// </summary>
    protected TStore Store;

    /// <summary>
    ///     The role store.
    /// </summary>
    protected IRoleStore<RoleEntity> RoleStore;

    /// <summary>
    ///     The user store.
    /// </summary>
    protected IUserStore<UserEntity> UserStore;

    /// <summary>
    ///     (Immutable) the data service.
    /// </summary>
    protected readonly IIdentityDataService DataService;

    #endregion

    #region Abstract

    /// <summary>
    ///     Initializes this object.
    /// </summary>
    protected virtual void Initialize()
    {
        Store = CreateStore();
        RoleStore = new RoleStore(DataService);
        UserStore = new UserStore(DataService);
    }

    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected abstract TStore CreateStore();

    #endregion

    #region Samples

    /// <summary>
    ///     Gets sample user 1.
    /// </summary>
    /// <returns>
    ///     The sample user 1.
    /// </returns>
    protected UserEntity GetSampleUser1()
    {
        return new UserEntity
        {
            Email = "m.mustermann@musterfirma.de",
            EmailConfirmed = true,
            Phone = "+49(1111)11111",
            PhoneConfirmed = true,
            PasswordHash = "hash",
            SecurityStamp = "stamp",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
            LockedOutPermanently = false
        };
    }

    /// <summary> Gets sample role 1.</summary>
    /// <returns> The sample role 1.</returns>
    protected RoleEntity GetSampleRole1()
    {
        return new RoleEntity
        {
            Name = "Administrator"
        };
    }

    #endregion
}