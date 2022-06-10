using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user lockout store test.</summary>
[TestClass]
public class UserLockoutStoreTest : UserTwoFactorStoreTest
{
    private readonly UserLockoutStore _lockoutStore;

    /// <summary> Default constructor.</summary>
    public UserLockoutStoreTest()
    {
        _lockoutStore = new UserLockoutStore(DataService);
    }

    /// <summary> Can get lockout end date asynchronous.</summary>
    [TestMethod]
    public void CanGetLockoutEndDateAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.LockedOutTill, _lockoutStore.GetLockoutEndDateAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set lockout end date asynchronous.</summary>
    [TestMethod]
    public void CanSetLockoutEndDateAsync()
    {
        var user = GetSampleUser1();
        _lockoutStore.CreateAsync(user, CancellationToken.None).Wait();

        _lockoutStore.SetLockoutEndDateAsync(user, DateTimeOffset.Now, CancellationToken.None).Wait();

        var dbUser = _lockoutStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(user.LockedOutTill, dbUser.LockedOutTill);
    }

    /// <summary> (Unit Test Method) can increment access failed count asynchronous.</summary>
    [TestMethod]
    public void CanIncrementAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        _lockoutStore.CreateAsync(user, CancellationToken.None).Wait();
        _lockoutStore.IncrementAccessFailedCountAsync(user, CancellationToken.None).Wait();
        Assert.AreEqual(1, user.AccessFailedCount);
    }

    /// <summary> (Unit Test Method) can reset access failed count asynchronous.</summary>
    [TestMethod]
    public void CanResetAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        _lockoutStore.CreateAsync(user, CancellationToken.None).Wait();
        _lockoutStore.IncrementAccessFailedCountAsync(user, CancellationToken.None).Wait();
        _lockoutStore.ResetAccessFailedCountAsync(user, CancellationToken.None).Wait();
        Assert.AreEqual(0, user.AccessFailedCount);
    }

    /// <summary> (Unit Test Method) can get access failed count asynchronous.</summary>
    [TestMethod]
    public void CanGetAccessFailedCountAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(0, _lockoutStore.GetAccessFailedCountAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get lockout enabled asynchronous.</summary>
    [TestMethod]
    public void CanGetLockoutEnabledAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.LockoutEnabled, _lockoutStore.GetLockoutEnabledAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set lockout enabled asynchronous.</summary>
    [TestMethod]
    public void CanSetLockoutEnabledAsync()
    {
        var user = GetSampleUser1();
        _lockoutStore.CreateAsync(user, CancellationToken.None).Wait();

        _lockoutStore.SetLockoutEnabledAsync(user, false, CancellationToken.None).Wait();

        var dbUser = _lockoutStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(user.LockoutEnabled, dbUser.LockoutEnabled);
    }
}