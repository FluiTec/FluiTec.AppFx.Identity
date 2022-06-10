using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a user two factor store test.</summary>
[TestClass]
public class UserTwoFactorStoreTest : UserSecurityStampStoreTest
{
    private readonly UserTwoFactorStore _userTwoFactorStore;

    /// <summary> Default constructor.</summary>
    public UserTwoFactorStoreTest()
    {
        _userTwoFactorStore = new UserTwoFactorStore(DataService);
    }

    /// <summary> (Unit Test Method) can get two factor enabled asynchronous.</summary>
    [TestMethod]
    public void CanGetTwoFactorEnabledAsync()
    {
        var user = GetSampleUser1();
        Assert.AreEqual(user.TwoFactorEnabled, _userTwoFactorStore.GetTwoFactorEnabledAsync(user, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set two factor enabled asynchronous.</summary>
    [TestMethod]
    public void CanSetTwoFactorEnabledAsync()
    {
        var user = GetSampleUser1();
        _userTwoFactorStore.CreateAsync(user, CancellationToken.None).Wait();

        _userTwoFactorStore.SetTwoFactorEnabledAsync(user, true, CancellationToken.None).Wait();

        var dbUser = _userTwoFactorStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(true, dbUser.TwoFactorEnabled);
    }
}