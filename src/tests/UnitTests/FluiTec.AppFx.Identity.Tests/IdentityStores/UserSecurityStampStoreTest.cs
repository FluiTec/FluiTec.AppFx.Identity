using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user security stamp store test.</summary>
[TestClass]
public class UserSecurityStampStoreTest : UserStoreTest
{
    private readonly UserSecurityStampStore _securityStampStore;

    /// <summary> Default constructor.</summary>
    public UserSecurityStampStoreTest()
    {
        _securityStampStore = new UserSecurityStampStore(DataService);
    }

    /// <summary> (Unit Test Method) can get security stamp asynchronous.</summary>
    [TestMethod]
    public void CanGetSecurityStampAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.SecurityStamp, _securityStampStore.GetSecurityStampAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) sets security stamp asynchronous.</summary>
    [TestMethod]
    public void SetSecurityStampAsync()
    {
        var user = GetSampleUser1();
        var updatetStamp = $"up_{user.SecurityStamp}";
        _securityStampStore.CreateAsync(user, CancellationToken.None).Wait();

        _securityStampStore.SetSecurityStampAsync(user, updatetStamp, CancellationToken.None).Wait();

        var dbUser = _securityStampStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetStamp, dbUser.SecurityStamp);
    }
}