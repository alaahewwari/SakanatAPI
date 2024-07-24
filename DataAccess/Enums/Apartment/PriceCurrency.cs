namespace DataAccess.Enums.Apartment;
/// <summary>
/// Represents the currency of the price of an apartment
/// that can be Shekel, Dollar or Dinar.
/// </summary>
public enum PriceCurrency : byte
{
    None = 0,
    ILS = 1,
    JOD = 2,
    USD = 3
}