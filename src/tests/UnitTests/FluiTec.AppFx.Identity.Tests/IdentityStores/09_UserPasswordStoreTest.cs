using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
/// (Unit Test Class) a user password store test.
/// </summary>
[TestClass]
public class UserPasswordStoreTest : StoreTest<IUserPasswordStore<UserEntity>>
{
    /// <summary>
    /// Creates the store.
    /// </summary>
    ///
    /// <returns>
    /// The new store.
    /// </returns>
    protected override IUserPasswordStore<UserEntity> CreateStore()
    {
        return new UserPasswordStore(DataService);
    }

    /// <summary>
    /// (Unit Test Method) can set password hash asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetPasswordHashAsync()
    {
        const string hash = "myhash";
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetPasswordHashAsync(user, hash, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(hash, dbUser.PasswordHash);
    }

    /// <summary>
    /// (Unit Test Method) can get password hash asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetPasswordHashAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.PasswordHash, Store.GetPasswordHashAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can has password asynchronous.
    /// </summary>
    [TestMethod]
    public void CanHasPasswordAsync()
    {
        var user = GetSampleUser1();
        Assert.IsTrue(Store.HasPasswordAsync(user, CancellationToken.None).Result);
    }
}