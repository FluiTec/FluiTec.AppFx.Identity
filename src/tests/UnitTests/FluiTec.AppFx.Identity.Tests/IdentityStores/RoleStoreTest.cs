using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.IdentityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.IdentityStores;

/// <summary> (Unit Test Class) a role store test.</summary>
[TestClass]
public class RoleStoreTest : UserLockoutStoreTest
{
    private readonly IRoleStore<RoleEntity> _store;

    /// <summary> Default constructor.</summary>
    public RoleStoreTest()
    {
        _store = new RoleStore(DataService);
    }

    /// <summary> Gets sample role 1.</summary>
    ///
    /// <returns> The sample role 1.</returns>
    protected RoleEntity GetSampleRole1()
    {
        return new RoleEntity
        {
            Name = "Administrator"
        };
    }

    /// <summary> (Unit Test Method) can create asynchronous.</summary>
    [TestMethod]
    public void CanCreateRoleAsync()
    {
        var role = GetSampleRole1();
        var result = _store.CreateAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can update role asynchronous.</summary>
    [TestMethod]
    public void CanUpdateRoleAsync()
    {
        var role = GetSampleRole1();
        _store.CreateAsync(role, CancellationToken.None).Wait();

        role.Name = $"up_{role.Name}";
        var result = _store.UpdateAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can delete role asynchronous.</summary>
    [TestMethod]
    public void CanDeleteRoleAsync()
    {
        var role = GetSampleRole1();
        _store.CreateAsync(role, CancellationToken.None).Wait();

        var result = _store.DeleteAsync(role, CancellationToken.None).Result;

        Assert.IsTrue(result.Succeeded);
    }

    /// <summary> (Unit Test Method) can get role identifier asynchronous.</summary>
    [TestMethod]
    public void CanGetRoleIdAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.Id.ToString(), _store.GetRoleIdAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get role name asynchronous.</summary>
    [TestMethod]
    public void CanGetRoleNameAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.Name, _store.GetRoleNameAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can get normalized role name asynchronous.</summary>
    [TestMethod]
    public void CanGetNormalizedRoleNameAsync()
    {
        var role = GetSampleRole1();
        Assert.AreEqual(role.NormalizedName, _store.GetNormalizedRoleNameAsync(role, CancellationToken.None).Result);
    }

    /// <summary> (Unit Test Method) can set role name asynchronous.</summary>
    [TestMethod]
    public void CanSetRoleNameAsync()
    {
        var role = GetSampleRole1();
        var updatetName = $"up_{role.Name}";
        _store.CreateAsync(role, CancellationToken.None).Wait();

        _store.SetRoleNameAsync(role, updatetName, CancellationToken.None).Wait();

        var dbRole = _store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
        Assert.AreEqual(updatetName, dbRole.Name);
    }

    /// <summary> (Unit Test Method) can find role by identifier asynchronous.</summary>
    [TestMethod]
    public void CanFindRoleByIdAsync()
    {
        var role = GetSampleRole1();
        _store.CreateAsync(role, CancellationToken.None).Wait();

        var dbUser = _store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
        Assert.IsNotNull(dbUser);
        Assert.IsTrue(dbUser.Equals(role));
    }

    /// <summary> (Unit Test Method) can find role by name asynchronous.</summary>
    [TestMethod]
    public void CanFindRoleByNameAsync()
    {
        var role = GetSampleRole1();
        _store.CreateAsync(role, CancellationToken.None).Wait();

        var dbRole = _store.FindByNameAsync(role.NormalizedName, CancellationToken.None).Result;
        Assert.IsNotNull(dbRole);
        Assert.IsTrue(dbRole.Equals(role));
    }
}