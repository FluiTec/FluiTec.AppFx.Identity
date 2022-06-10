using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using FluiTec.AppFx.Identity.NMemory;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
/// (Unit Test Class) a user store test.
/// </summary>
[TestClass]
public class UserStoreTest
{
    private readonly IUserStore<UserEntity> _userStore;
    protected readonly IIdentityDataService DataService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserStoreTest()
    {
        DataService = new NMemoryIdentityDataService(null);
        _userStore = new UserStore(DataService);
    }

    /// <summary>
    /// Gets sample user 1.
    /// </summary>
    ///
    /// <returns>
    /// The sample user 1.
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

    /// <summary>
    /// (Unit Test Method) can create asynchronous.
    /// </summary>
    [TestMethod]
    public void CanCreateAsync()
    {
        var user = GetSampleUser1();
        var result = _userStore.CreateAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    /// (Unit Test Method) can find by identifier asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByIdAsync()
    {
        var user = GetSampleUser1();
        _userStore.CreateAsync(user, CancellationToken.None).Wait();

        var dbUser = _userStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(user));
    }

    /// <summary>
    /// (Unit Test Method) can find by name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByNameAsync()
    {
        var user = GetSampleUser1();
        _userStore.CreateAsync(user, CancellationToken.None).Wait();

        var dbUser = _userStore.FindByNameAsync(user.NormalizedEmail, CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(user));
    }

    /// <summary>
    /// (Unit Test Method) can update asynchronous.
    /// </summary>
    [TestMethod]
    public void CanUpdateAsync()
    {
        var user = GetSampleUser1();
        _userStore.CreateAsync(user, CancellationToken.None).Wait();

        user.Email = "updated@musterfirma.de";
        var result = _userStore.UpdateAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    /// (Unit Test Method) can delete asynchronous.
    /// </summary>
    [TestMethod]
    public void CanDeleteAsync()
    {
        var user = GetSampleUser1();
        _userStore.CreateAsync(user, CancellationToken.None).Wait();

        var result = _userStore.DeleteAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    /// Can get user identifier asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserIdAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Id.ToString(), _userStore.GetUserIdAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can get user name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserNameAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Email, _userStore.GetUserNameAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can get user normalized name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserNormalizedNameAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.NormalizedEmail, _userStore.GetNormalizedUserNameAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can set user name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetUserNameAsync()
    {
        var user = GetSampleUser1();
        var updatetName = $"up_{user.Email}";
        _userStore.CreateAsync(user, CancellationToken.None).Wait();

        _userStore.SetUserNameAsync(user, updatetName, CancellationToken.None).Wait();

        var dbUser = _userStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetName, dbUser.Email);
    }
}