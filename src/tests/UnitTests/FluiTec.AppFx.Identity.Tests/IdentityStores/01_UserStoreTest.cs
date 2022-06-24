using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
///     (Unit Test Class) a user store test.
/// </summary>
[TestClass]
public class UserStoreTest : StoreTest<IUserStore<UserEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserStore<UserEntity> CreateStore()
    {
        return new UserStore(DataService);
    }

    /// <summary>
    ///     (Unit Test Method) can create asynchronous.
    /// </summary>
    [TestMethod]
    public void CanCreateAsync()
    {
        var user = GetSampleUser1();
        var result = Store.CreateAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    ///     (Unit Test Method) can find by identifier asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByIdAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(user));
    }

    /// <summary>
    ///     (Unit Test Method) can find by name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByNameAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        var dbUser = Store.FindByNameAsync(user.NormalizedEmail, CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(user));
    }

    /// <summary>
    ///     (Unit Test Method) can update asynchronous.
    /// </summary>
    [TestMethod]
    public void CanUpdateAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        user.Email = "updated@musterfirma.de";
        var result = Store.UpdateAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    ///     (Unit Test Method) can delete asynchronous.
    /// </summary>
    [TestMethod]
    public void CanDeleteAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        var result = Store.DeleteAsync(user, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary>
    ///     Can get user identifier asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserIdAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Id.ToString(), Store.GetUserIdAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    ///     (Unit Test Method) can get user name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserNameAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Email, Store.GetUserNameAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    ///     (Unit Test Method) can get user normalized name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUserNormalizedNameAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.NormalizedEmail, Store.GetNormalizedUserNameAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    ///     (Unit Test Method) can set user name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetUserNameAsync()
    {
        var user = GetSampleUser1();
        var updatetName = $"up_{user.Email}";
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetUserNameAsync(user, updatetName, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetName, dbUser.Email);
    }
}