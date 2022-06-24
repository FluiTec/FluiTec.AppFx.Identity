using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary>
///     A user role store test.
/// </summary>
[TestClass]
public class UserRoleStoreTest : StoreTest<IUserRoleStore<UserEntity>>
{
    /// <summary>
    ///     Initializes this object.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        RoleStore = new RoleStore(DataService);
        UserStore = new UserStore(DataService);
    }

    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IUserRoleStore<UserEntity> CreateStore()
    {
        return new UserRoleStore(DataService);
    }

    /// <summary>
    ///     (Unit Test Method) can add to role asynchronous.
    /// </summary>
    [TestMethod]
    public void CanAddToRoleAsync()
    {
        var role = GetSampleRole1();
        var user = GetSampleUser1();

        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        RoleStore.CreateAsync(role, CancellationToken.None).Wait();
        Store.AddToRoleAsync(user, role.NormalizedName, CancellationToken.None).Wait();
        var result = Store.IsInRoleAsync(user, role.NormalizedName, CancellationToken.None).Result;
        Assert.IsTrue(result);
    }

    /// <summary>
    ///     (Unit Test Method) can remove from role asynchronous.
    /// </summary>
    [TestMethod]
    public void CanRemoveFromRoleAsync()
    {
        var role = GetSampleRole1();
        var user = GetSampleUser1();

        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        RoleStore.CreateAsync(role, CancellationToken.None).Wait();
        Store.AddToRoleAsync(user, role.NormalizedName, CancellationToken.None).Wait();
        Store.RemoveFromRoleAsync(user, role.NormalizedName, CancellationToken.None).Wait();
        var result = Store.IsInRoleAsync(user, role.NormalizedName, CancellationToken.None).Result;
        Assert.IsFalse(result);
    }

    /// <summary>
    ///     (Unit Test Method) can get roles asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetRolesAsync()
    {
        var user = GetSampleUser1();
        var role = GetSampleRole1();

        var roles1 = Store.GetRolesAsync(user, CancellationToken.None).Result;
        Assert.IsTrue(!roles1.Any());

        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        RoleStore.CreateAsync(role, CancellationToken.None).Wait();
        Store.AddToRoleAsync(user, role.NormalizedName, CancellationToken.None).Wait();
        var roles2 = Store.GetRolesAsync(user, CancellationToken.None).Result;
        Assert.IsTrue(roles2.Contains(role.NormalizedName));
    }

    /// <summary>
    ///     (Unit Test Method) can is in role asynchronous.
    /// </summary>
    [TestMethod]
    public void CanIsInRoleAsync()
    {
        var role = GetSampleRole1();
        var user = GetSampleUser1();
        var result = Store.IsInRoleAsync(user, role.NormalizedName, CancellationToken.None).Result;
        Assert.IsFalse(result);
    }

    /// <summary>
    ///     (Unit Test Method) can get users in role asynchronous.
    /// </summary>
    [TestMethod]
    public void CanGetUsersInRoleAsync()
    {
        var user = GetSampleUser1();
        var role = GetSampleRole1();

        var roles1 = Store.GetRolesAsync(user, CancellationToken.None).Result;
        Assert.IsTrue(!roles1.Any());

        UserStore.CreateAsync(user, CancellationToken.None).Wait();
        RoleStore.CreateAsync(role, CancellationToken.None).Wait();
        Store.AddToRoleAsync(user, role.NormalizedName, CancellationToken.None).Wait();

        var users = Store.GetUsersInRoleAsync(role.NormalizedName, CancellationToken.None).Result;
        Assert.IsNotNull(users.SingleOrDefault(u => u.NormalizedEmail == user.NormalizedEmail));
    }
}