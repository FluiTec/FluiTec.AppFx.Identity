using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user security stamp store test.</summary>
[TestClass]
public class UserSecurityStampStoreTest : StoreTest<IUserSecurityStampStore<UserEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserSecurityStampStore<UserEntity> CreateStore()
    {
        return new UserSecurityStampStore(DataService);
    }

    /// <summary> (Unit Test Method) can get security stamp asynchronous.</summary>
    [TestMethod]
    public void CanGetSecurityStampAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.SecurityStamp, Store.GetSecurityStampAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) sets security stamp asynchronous.</summary>
    [TestMethod]
    public void SetSecurityStampAsync()
    {
        var user = GetSampleUser1();
        var updatetStamp = $"up_{user.SecurityStamp}";
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetSecurityStampAsync(user, updatetStamp, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetStamp, dbUser.SecurityStamp);
    }
}