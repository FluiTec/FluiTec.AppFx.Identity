using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

[TestClass]
public class UserEmailStoreTest : StoreTest<IUserEmailStore<UserEntity>>
{
    /// <summary>
    /// Creates the store.
    /// </summary>
    ///
    /// <returns>
    /// The new store.
    /// </returns>
    protected override IUserEmailStore<UserEntity> CreateStore()
    {
        return new UserEmailStore(DataService);
    }

    /// <summary>
    /// (Unit Test Method) can set email asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetEmailAsync()
    {
        const string mail = "myhash";
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetEmailAsync(user, mail, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(mail, dbUser.Email);
    }

    /// <summary>
    /// (Unit Test Method) can get email asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetEmailAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Email, Store.GetEmailAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can get email confirmed asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetEmailConfirmedAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.EmailConfirmed, Store.GetEmailConfirmedAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can set email confirmed asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetEmailConfirmedAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetEmailConfirmedAsync(user, true, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(true, dbUser.EmailConfirmed);
    }

    /// <summary>
    /// (Unit Test Method) can find by email asynchronous.
    /// </summary>
    [TestMethod]
    public void CanFindByEmailAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        var result = Store.FindByEmailAsync(user.NormalizedEmail, CancellationToken.None).Result;

        Assert.IsNotNull(result);
    }

    /// <summary>
    /// (Unit Test Method) can get normalized email asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetNormalizedEmailAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.NormalizedEmail, Store.GetNormalizedEmailAsync(user, CancellationToken.None).Result);
    }
}