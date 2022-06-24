using System.Security.Claims;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
///     A user claim store test.
/// </summary>
[TestClass]
public class UserClaimStoreTest : StoreTest<IUserClaimStore<UserEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserClaimStore<UserEntity> CreateStore()
    {
        return new UserClaimStore(DataService);
    }

    /// <summary>
    ///     (Unit Test Method) can add claims asynchronous.
    /// </summary>
    [TestMethod]
    public void CanAddClaimsAsync()
    {
        var user = GetSampleUser1();

        var insertClaims = new[]
        {
            new Claim("t1", "v1"),
            new Claim("t2", "v2")
        };
        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        Store.AddClaimsAsync(user, insertClaims, CancellationToken.None).Wait();
        var dbClaims = Store.GetClaimsAsync(user, CancellationToken.None).Result;

        foreach (var iC in insertClaims)
            Assert.IsTrue(dbClaims.Any(dC => dC.Type == iC.Type && dC.Value == iC.Value));
    }

    /// <summary>
    ///     (Unit Test Method) can get claims asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetClaimsAsync()
    {
        // ensure we can get claims by user
        var user = GetSampleUser1();

        var insertClaims = new[]
        {
            new Claim("t1", "v1"),
            new Claim("t2", "v2")
        };
        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        Store.AddClaimsAsync(user, insertClaims, CancellationToken.None).Wait();
        var dbClaims = Store.GetClaimsAsync(user, CancellationToken.None).Result;

        foreach (var iC in insertClaims)
            Assert.IsTrue(dbClaims.Any(dC => dC.Type == iC.Type && dC.Value == iC.Value));
    }

    /// <summary>
    ///     (Unit Test Method) can remove claims asynchronous.
    /// </summary>
    [TestMethod]
    public void CanRemoveClaimsAsync()
    {
        // ensure we can get claims by user
        var user = GetSampleUser1();

        var insertClaims = new[]
        {
            new Claim("t1", "v1"),
            new Claim("t2", "v2")
        };
        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        Store.AddClaimsAsync(user, insertClaims, CancellationToken.None).Wait();
        var dbClaims = Store.GetClaimsAsync(user, CancellationToken.None).Result;
        Store.RemoveClaimsAsync(user, dbClaims, CancellationToken.None).Wait();
        dbClaims = Store.GetClaimsAsync(user, CancellationToken.None).Result;
        Assert.IsTrue(!dbClaims.Any());
    }

    /// <summary>
    ///     (Unit Test Method) can replace claim asynchronous.
    /// </summary>
    [TestMethod]
    public void CanReplaceClaimAsync()
    {
        var user = GetSampleUser1();

        var insertClaims = new[]
        {
            new Claim("t1", "v1"),
            new Claim("t2", "v2")
        };
        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        Store.AddClaimsAsync(user, insertClaims, CancellationToken.None).Wait();
        Store.ReplaceClaimAsync(user, insertClaims[0], new Claim("t1", "v1_1"), CancellationToken.None).Wait();

        var dbClaims = Store.GetClaimsAsync(user, CancellationToken.None).Result;
        Assert.IsTrue(dbClaims.Any(dbc => dbc.Type == "t1" && dbc.Value == "v1_1"));
    }

    /// <summary>
    ///     (Unit Test Method) can get users for claim asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUsersForClaimAsync()
    {
        var user = GetSampleUser1();

        var insertClaims = new[]
        {
            new Claim("t1", "v1"),
            new Claim("t2", "v2")
        };
        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        Store.AddClaimsAsync(user, insertClaims, CancellationToken.None).Wait();

        var query1 = Store.GetUsersForClaimAsync(new Claim("t1", string.Empty), CancellationToken.None).Result;
        var query2 = Store.GetUsersForClaimAsync(new Claim("t1", "v1"), CancellationToken.None).Result;

        Assert.IsTrue(query1.Count == 1);
        Assert.IsTrue(query2.Count == 1);
    }
}