using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
/// (Unit Test Class) a user phone number store test.
/// </summary>
[TestClass]
public class UserPhoneNumberStoreTest : StoreTest<IUserPhoneNumberStore<UserEntity>>
{
    /// <summary>
    /// Creates the store.
    /// </summary>
    ///
    /// <returns>
    /// The new store.
    /// </returns>
    protected override IUserPhoneNumberStore<UserEntity> CreateStore()
    {
        return new UserPhoneNumberStore(DataService);
    }

    /// <summary>
    /// (Unit Test Method) can get phone number asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetPhoneNumberAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.Phone, Store.GetPhoneNumberAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can get phone number confirmed asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetPhoneNumberConfirmedAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.PhoneConfirmed, Store.GetPhoneNumberConfirmedAsync(user, CancellationToken.None).Result);
    }

    /// <summary>
    /// (Unit Test Method) can set phone number asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetPhoneNumberAsync()
    {
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetPhoneNumberConfirmedAsync(user, true, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(true, dbUser.PhoneConfirmed);
    }

    /// <summary>
    /// (Unit Test Method) can set phone number confirmed asynchronous.
    /// </summary>
    [TestMethod]
    public void CanSetPhoneNumberConfirmedAsync()
    {
        const string phone = "myhash";
        var user = GetSampleUser1();
        Store.CreateAsync(user, CancellationToken.None).Wait();

        Store.SetPhoneNumberAsync(user, phone, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(phone, dbUser.Phone);
    }
}