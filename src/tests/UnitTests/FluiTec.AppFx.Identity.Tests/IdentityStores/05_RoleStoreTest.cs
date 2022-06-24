using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a role store test.</summary>
[TestClass]
public class RoleStoreTest : StoreTest<IRoleStore<RoleEntity>>
{
    /// <summary>
    ///     Creates the store.
    /// </summary>
    /// <returns>
    ///     The new store.
    /// </returns>
    protected override IRoleStore<RoleEntity> CreateStore()
    {
        return new RoleStore(DataService);
    }

    /// <summary> (Unit Test Method) can create asynchronous.</summary>
    [TestMethod]
    public void CanCreateRoleAsync()
    {
        var role = GetSampleRole1();
        var result = Store.CreateAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can update role asynchronous.</summary>
    [TestMethod]
    public void CanUpdateRoleAsync()
    {
        var role = GetSampleRole1();
        Store.CreateAsync(role, CancellationToken.None).Wait();

        role.Name = $"up_{role.Name}";
        var result = Store.UpdateAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can delete role asynchronous.</summary>
    [TestMethod]
    public void CanDeleteRoleAsync()
    {
        var role = GetSampleRole1();
        Store.CreateAsync(role, CancellationToken.None).Wait();

        var result = Store.DeleteAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can get role identifier asynchronous.</summary>
    [TestMethod]
    public void CanGetRoleIdAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.Id.ToString(), Store.GetRoleIdAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get role name asynchronous.</summary>
    [TestMethod]
    public void CanGetRoleNameAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.Name, Store.GetRoleNameAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get normalized role name asynchronous.</summary>
    [TestMethod]
    public void CanGetNormalizedRoleNameAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.NormalizedName, Store.GetNormalizedRoleNameAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set role name asynchronous.</summary>
    [TestMethod]
    public void CanSetRoleNameAsync()
    {
        var role = GetSampleRole1();
        var updatetName = $"up_{role.Name}";
        Store.CreateAsync(role, CancellationToken.None).Wait();

        Store.SetRoleNameAsync(role, updatetName, CancellationToken.None).Wait();

        var dbRole = Store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetName, dbRole.Name);
    }

    /// <summary> (Unit Test Method) can find role by identifier asynchronous.</summary>
    [TestMethod]
    public void CanFindRoleByIdAsync()
    {
        var role = GetSampleRole1();
        Store.CreateAsync(role, CancellationToken.None).Wait();

        var dbUser = Store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(role));
    }

    /// <summary> (Unit Test Method) can find role by name asynchronous.</summary>
    [TestMethod]
    public void CanFindRoleByNameAsync()
    {
        var role = GetSampleRole1();
        Store.CreateAsync(role, CancellationToken.None).Wait();

        var dbRole = Store.FindByNameAsync(role.NormalizedName, CancellationToken.None).Result;
        Assert.IsNotNull(dbRole);
        Assert.IsTrue(dbRole.Equals(role));
    }
}