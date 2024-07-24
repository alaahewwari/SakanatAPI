using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class Following
    {
        public static Error UserAlreadyFollowed => Error.Conflict(
            code: "Following.UserAlreadyFollowed",
            description: "You are already follow this user"
        );
        public static Error UserAlreadyUnfollowed => Error.Conflict(
                       code: "Following.UserAlreadyUnfollowed",
                                  description: "You are already unfollowed this user"
                   );

        public static Error NotFollowUser => Error.Conflict(
            code: "Following.NotFolloweUser",
            description:"You are not follow this user"
            );
        //CannotfollowYourself
        public static Error CannotfollowYourself => Error.Conflict(
                                  code: "Following.CannotfollowYourself",
                                                                   description: "You Can't Follow Yourself"
                                  );
        public static Error CannotUnfollowYourself => Error.Conflict(
                       code:"Following.CannotUnfollowYourself",
                                  description:"You Can't Unfollow Yourself"
                       );
        public static Error FollowingFailed => Error.Conflict(
                       code:"Following.FollowingFailed",
                                  description:"Failed to follow user"
                       );

        public static Error UnfollowingFailed => Error.Conflict(
                       code: "Following.UnfollowingFailed",
                       description: "Failed to unfollow user");
        //NoFollowersFound
        public static Error NoFollowersFound => Error.NotFound(
                       code: "Following.NoFollowersFound",
                                  description: "No followers found"
                   );

        public static Error NoFollowingFound => Error.NotFound(
                                  code: "Following.NoFollowingFound",
                                                                   description: "No following found"
                              );

    }
}