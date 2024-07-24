using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class University
    {
        public static Error UniversityAlreadyExist => Error.Conflict(
            "University.UniversityAlreadyExists",
            "University already exists"
        );

        public static Error UniversityNotFound => Error.NotFound(
            "University.UniversityNotFound",
            "University not found!"
        );

        public static Error NoUniversitiesFound => Error.NotFound(
            "University.NoUniversitiesFound",
            "No universities found!"
        );

        //Failed to Create University 
        public static Error FailedToCreateUniversity => Error.Failure(
            "University.FailedToCreateUniversity",
            "Failed to create new university"
        );

        public static Error FailedToUpdateUniversity => Error.Failure(
            "University.FailedToUpdateUniversity",
            "Failed to update this university"
        );
    }
}