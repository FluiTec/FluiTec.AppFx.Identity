using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
/// (Unit Test Class) a user login store test.
/// </summary>
[TestClass]
public class UserLoginStoreTest : StoreTest<IUserLoginStore<UserEntity>>
{
    /// <summary>
    /// Creates the store.
    /// </summary>
    ///
    /// <returns>
    /// The new store.
    /// </returns>
    protected override IUserLoginStore<UserEntity> CreateStore()
    {
        return new UserLoginStore(DataService);
    }

    /// <summary>
    /// (Unit Test Method) can add login asynchronous.
    /// </summary>
    [TestMethod]
    public void CanAddLoginAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();
        Store
            .AddLoginAsync(user, new UserLoginInfo("provider", "providerKey", "display"), CancellationToken.None)
            .Wait();
        var result = Store.FindByLoginAsync("provider", "providerKey", CancellationToken.None).Result;

        Assert.IsNotNull(result);
    }

    /// <summary>
    /// (Unit Test Method) can remove login asynchronous.
    /// </summary>
    [TestMethod]
    public void CanRemoveLoginAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();
        Store
            .AddLoginAsync(user, new UserLoginInfo("provider", "providerKey", "display"), CancellationToken.None)
            .Wait();
        Store
            .RemoveLoginAsync(user, "provider", "providerKey", CancellationToken.None)
            .Wait();
        var result = Store.FindByLoginAsync("provider", "providerKey", CancellationToken.None).Result;
    }

    /// <summary>
    /// (Unit Test Method) can get logins asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetLoginsAsync()
    {
        //throw new NotImplementedException();
    }

    /// <summary>
    /// (Unit Test Method) can find by login asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByLoginAsync()
    {
        //throw new NotImplementedException();
    }
}