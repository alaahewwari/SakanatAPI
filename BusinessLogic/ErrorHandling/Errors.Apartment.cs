

using ErrorOr;


namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Apartment
        {
            public static Error NoApartmentsFound => Error.NotFound(
                               "Apartment.NoApartmentsFound",
                                              "No apartments found"
                           );
            public static Error ApartmentNotFound => Error.NotFound(
                "Apartment.NoApartmentsFound",
                "Apartment not found"
        );
            public static Error ApartmentAlreadyExist => Error.Conflict(
                "Apartment.ApartmentAlreadyExists",
                "Apartment already exists"
            );
            public static Error FailedToCreateApartment => Error.Failure(
                               "Apartment.FailedToCreateApartment",
                                              "Failed to create new apartment"
                           );

            public static Error FailedToUpdateApartment => Error.Failure(
                                              "Apartment.FailedToUpdateApartment",
                                              "Failed to update apartment"
                                          );
            //FailedToDeleteApartment
            public static Error FailedToDeleteApartment => Error.Failure(
                               "Apartment.FailedToDeleteApartment",
                                              "Failed to delete apartment"
                           );
            //ApartmentAlreadyFavourite
            public static Error ApartmentAlreadyFavourite => Error.Conflict(
                               "Apartment.ApartmentAlreadyFavourite",
                                              "Apartment is already a favourite"
                           );
            //InvalidInfo
            public static Error InvalidInfo => Error.Failure(
                                              "Apartment.InvalidInfo",
                                               "Invalid information"
                                          );
        }
    }
}
