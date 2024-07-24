namespace Presentation
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/[controller]";
        public static class Identity
        {
            public const string UserRegistration = "registration";
            public const string Login = "login";
            public const string RefreshToken = "refresh-token";
            public const string ConfirmEmail = "confirm-email";
            public const string ForgotPassword = "forgot-password";
            public const string ResetForgottenPassword = "reset-forgotten-password";
            public const string SendOtpMessageOnWhatsapp = "otp-message-on-whatsapp";
        }

        public static class Users
        {
            public const string GetAllUsers = "";
            public const string GetUserById = "{id}";
            public const string GetUserByEmail = "email/{email}";
            public const string UpdateUser = "";
            public const string ChangePassword = "change-password";
            public const string UpdateProfileImage = "profile-image";
            public const string DeleteProfileImage = "profile-image";
            public const string DeleteAccount = "";
        }

        public static class Cities
        {
            public const string GetAllCities = "";
            public const string GetCityById = "{id}";
            public const string CreateCity = "";
            public const string UpdateCity = "{id}";
        }

        public static class Universities
        {
            public const string GetAllUniversities = "";
            public const string GetUniversityById = "{id}";
            public const string CreateUniversity = "{cityId}";
            public const string UpdateUniversity = "{id}";
            public const string DeleteUniversity = "{cityId}/{id}";
            public const string GetUniversitiesByCityId = "city/{cityId}";
        }

        public static class Apartments
        {
            public const string GetAllApartmentsTest = "test";
            public const string GetAllApartments = "";
            public const string GetApartmentById = "{id}";
            public const string GetApartmentsByUserId = "user/{userId}";
            public const string CreateApartment = "";
            public const string UpdateApartment = "{id}";
            public const string DeleteApartment = "{id}";
            public const string UploadApartmentImages = "{id}/images";
            public const string GetApartmentImages = "{id}/images";
            public const string UpdateApartmentImage = "{id}/image/{imageId}";
            public const string DeleteApartmentImage = "{id}/image/{imageId}";
            public const string GetApartmentImageById = "image/{imageId}";
            public const string GetUserApartmentDiscounts = "discounts/{userId}";
            public const string GetActiveApartmentWithDiscountsNumber = "available-apartments-number";
            public const string Availability = "{id}/availability";
            public const string Visibility = "{id}/visibility";
        }

        public static class Suspensions
        {
            public const string GetAllSuspensions = "";
            public const string GetSuspendedUserById = "{userId}";
            public const string CreateSuspension = "";
            public const string DeleteSuspension = "{userId}";
            public const string GetAllSuspensionReasons = "reasons";
        }

        public static class Favourites
        {
            public const string GetFavouritesByUserId = "user/{userId}";
            public const string GetFavouriteById = "";
            public const string AddToFavourites = "AddTofavourites";
            public const string RemoveFromFavourites = "Removefavourites";
        }

        public static class Tenants
        {
            public const string GetTenantById = "{id}";
            public const string GetAllTenantsByOwnerId = "owner/{ownerId}";
            public const string CreateTenant = "";
            public const string UpdateTenant = "{id}";
            public const string CreateContract = "{tenantId}/apartments/{apartmentId}/contracts";
            public const string NumberOfTenants = "percentage-of-tenants-have-terminated-contracts/{ownerId}";

        }

        public static class Discounts
        {
            public const string GetDiscountsByApartmentId = "apartment/{apartmentId}";
            public const string GetDiscountById = "{id}";
            public const string GetDiscountsForUser = "user/{userId}";
            public const string CreateDiscount = "";
            public const string AddDiscountToApartment = "{id}/{apartmentId}";
                public const string RemoveDiscountFromApartment = "{id}/{apartmentId}";
            public const string UpdateDiscount = "{id}";
            public const string DeleteDiscount = "{id}";
        }

        public static class UserFollowing
        {
            public const string FollowOwner = "follow/{ownerId}";
            public const string UnfollowOwner = "unfollow/{ownerId}";
            public const string GetFollowers = "followers/{ownerId}";
            public const string GetFollowing = "following/{ownerId}";
        }

        public static class Notifications
        {
            public const string GetNotifications = "user/{userId}";
            public const string UpdateNotificationsStatus = "status/{id}/";
            public const string GetNotificationById = "{id}";
        }

        public static class Contract
        {
            public const string GetContractById = "{id}";
            public const string UpdateContract = "{id}";
            public const string DeleteContract = "{id}";
            public const string GetContractsByApartmentId = "apartment/{apartmentId}";
            public const string GetContractsByTenantId = "tenant/{tenantId}";
            public const string AddPayment = "{id}/payments";
            public const string GetPaymentById = "payment/{paymentId}";
            public const string GetPaymentsByContractId = "{id}/payments";
            public const string UpdatePayment = "payment/{paymentId}";
            public const string GetContractByOwnerId ="owner/{ownerId}";
            public const string GetTotalPaymentsSumByContractId = "payments/sum/{id}";
        }
    }
}

