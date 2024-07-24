using System.ComponentModel;
namespace DataAccess.Enums.Apartment;
/// <summary>
/// Represents the furnished status of an apartment
/// that can be fully furnished, partially furnished or unfurnished.
/// </summary>
public enum FurnishedStatus : byte
{
    None = 0,

    Full = 1,

    Partial= 2,

    Unfurnished = 3
}