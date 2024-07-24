using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class User
    {
        public static Error FailedToAssignRole => Error.Failure(
            "User.FailedToAssignRole",
            "Something went wrong while assigning role to user");

        public static Error RoleDoesNotExist => Error.NotFound(
            "User.RoleDoesNotExist",
            "Role does not exist");

        public static Error PasswordChangeFailed => Error.Failure(
            "User.PasswordChangeFailed",
            "Failed to change password");
        
        public static Error UserNotOwner => Error.Forbidden(
                       "User.UserNotOwner",
                                  "User is not an owner");
    }
}