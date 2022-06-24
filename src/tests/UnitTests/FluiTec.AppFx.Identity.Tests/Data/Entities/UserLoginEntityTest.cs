using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
///     A user login entity test.
/// </summary>
[TestClass]
public class UserLoginEntityTest : EntityTest<UserLoginEntity>
{
    /// <summary>
    ///     Creates the entity.
    /// </summary>
    /// <returns>
    ///     The new entity.
    /// </returns>
    protected override UserLoginEntity CreateEntity()
    {
        return new UserLoginEntity
        {
            Id = 1,
            Provider = "Google",
            ProviderDispayName = "Google",
            ProviderKey = "abcdefg",
            UserId = Guid.Parse("FA7F0AA7-A509-44DD-8254-0FE1473074A8")
        };
    }

    /// <summary>
    ///     Creates other entity.
    /// </summary>
    /// <returns>
    ///     The new other entity.
    /// </returns>
    protected override UserLoginEntity CreateOtherEntity()
    {
        return new UserLoginEntity
        {
            Id = 2,
            Provider = "Amazon",
            ProviderDispayName = "Amazon",
            ProviderKey = "hijklmnop",
            UserId = Guid.Parse("4399EF37-3AD2-4A80-AD2F-5848C02B59DC")
        };
    }
}