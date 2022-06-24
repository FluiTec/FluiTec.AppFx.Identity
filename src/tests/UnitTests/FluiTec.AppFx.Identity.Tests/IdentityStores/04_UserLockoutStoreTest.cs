using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user lockout store test.</summary>
[TestClass]
public class UserLockoutStoreTest : StoreTest<IUserLockoutStore<UserEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserLockoutStore<UserEntity> CreateStore()
    {
        return new UserLockoutStore(DataService);
    }

    /// <summary> Can get lockout end date asynchronous.</summary>
    [TestMethod]
    public void CanGetLockoutEndDateAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.LockedOutTill, Store.GetLockoutEndDateAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set lockout end date asynchronous.</summary>
    [TestMethod]
    public void CanSetLockoutEndDateAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetLockoutEndDateAsync(user, DateTimeOffset.Now, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(user.LockedOutTill, dbUser.LockedOutTill);
    }

    /// <summary> (Unit Test Method) can increment access failed count asynchronous.</summary>
    [TestMethod]
    public void CanIncrementAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();
        Store.IncrementAccessFailedCountAsync(user, CancellationToken.None).Wait();
        Assert.AreEqual(1, user.AccessFailedCount);
    }

    /// <summary> (Unit Test Method) can reset access failed count asynchronous.</summary>
    [TestMethod]
    public void CanResetAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();
        Store.IncrementAccessFailedCountAsync(user, CancellationToken.None).Wait();
        Store.ResetAccessFailedCountAsync(user, CancellationToken.None).Wait();
        Assert.AreEqual(0, user.AccessFailedCount);
    }

    /// <summary> (Unit Test Method) can get access failed count asynchronous.</summary>
    [TestMethod]
    public void CanGetAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(0, Store.GetAccessFailedCountAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get lockout enabled asynchronous.</summary>
    [TestMethod]
    public void CanGetLockoutEnabledAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.LockoutEnabled, Store.GetLockoutEnabledAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set lockout enabled asynchronous.</summary>
    [TestMethod]
    public void CanSetLockoutEnabledAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetLockoutEnabledAsync(user, false, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(user.LockoutEnabled, dbUser.LockoutEnabled);
    }
}