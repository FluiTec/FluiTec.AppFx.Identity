using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user two factor store test.</summary>
[TestClass]
public class UserTwoFactorStoreTest : StoreTest<IUserTwoFactorStore<UserEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserTwoFactorStore<UserEntity> CreateStore()
    {
        return new UserTwoFactorStore(DataService);
    }

    /// <summary> (Unit Test Method) can get two factor enabled asynchronous.</summary>
    [TestMethod]
    public void CanGetTwoFactorEnabledAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.TwoFactorEnabled, Store.GetTwoFactorEnabledAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set two factor enabled asynchronous.</summary>
    [TestMethod]
    public void CanSetTwoFactorEnabledAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetTwoFactorEnabledAsync(user, true, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(true, dbUser.TwoFactorEnabled);
    }
}