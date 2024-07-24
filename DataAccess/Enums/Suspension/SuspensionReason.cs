namespace DataAccess.Enums.Suspension;
/// <summary>
/// Represents the reason for suspending a user
/// that is inappropriate content, profile, pictures or fraudulent activity.
/// </summary>
public enum SuspensionReason : byte
{
    None = 0,
    InappropriateContent= 1,
    InappropriateProfile= 2,
    InappropriatePictures= 3,
    FraudulentActivity= 4
}