using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class Identity
    {
        public static Error InvalidCredentials => Error.Validation(
            "Identity.InvalidCredentials",
            "Invalid credentials"
        );

        public static Error UserNotFound => Error.NotFound(
            "Identity.UserNotFound",
            "User not found");

        public static Error TokenExpired => Error.Unauthorized(
                       "Identity.TokenExpired",
                                  "Token expired, you should login now"
                   );
        public static Error NoUsersFound => Error.NotFound(
            "Identity.NoUsersFound",
            "No users found"
        );

        public static Error UserAlreadyExist => Error.Conflict(
            "Identity.UserAlreadyExist",
            "Provided email address already exists. Can't register new user");

        public static Error FailedToCreateUser => Error.Failure(
            "Identity.FailedToCreateUser",
            "Something went wrong while creating new user");

        public static Error EmailNotConfirmed => Error.Forbidden(
            "Identity.EmailNotConfirmed",
            "Email is not confirmed");

        //move to token errors
        public static Error InvalidToken => Error.Unauthorized(
            "Identity.InvalidToken",
            "Invalid token"
        );

        //move to user errors
        public static Error PasswordResetFailed => Error.Failure(
            "Identity.PasswordResetFailed",
            "Failed to reset password"
        );

        //unauthorized
        public static Error Unauthorized => Error.Unauthorized(
            "Identity.Unauthorized",
            "Unauthorized"
        );
    }
}