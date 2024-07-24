using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class City
    {
        public static Error CityAlreadyExists => Error.Conflict(
            "City.CityAlreadyExists",
            "City already exists"
        );

        public static Error CityNotFound => Error.NotFound(
            "City.CityNotFound",
            "City not found!"
        );

        public static Error NoCitiesFound => Error.NotFound(
            "City.NoCitiesFound",
            "No cities found!"
        );

        //Failed to Create city 
        public static Error FailedToCreateCity => Error.Failure(
            "City.FailedToCreateCity",
            "Failed to create new city"
        );

        public static Error FailedToUpdateCity => Error.Failure(
            "City.FailedToUpdateCity",
            "Failed to update this city"
        );
    }
}