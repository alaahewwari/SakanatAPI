namespace DataAccess.Enums.Apartment;
/// <summary>
/// Represents the rent period of an apartment
/// that can be monthly, yearly, weekly or daily.
/// </summary>
public enum RentPeriod : byte
{
    None= 0,
    Monthly = 1,
    Yearly = 2,
    ForSemester = 3,
}