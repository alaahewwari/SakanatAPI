using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Suspension
        {
            public static Error NoSuspensionReasons => Error.NotFound(
                               code: "Suspension.NoSuspensionReasons",
                                              description: "No suspension reasons found"
                           );
                public static Error UserIsSuspended => Error.Forbidden(
                                   "Suspension.UserIsSuspended",
                                                  "User is already suspended"
                               );
            public static Error FailedToUnSuspendUser => Error.Failure(
                                              "Suspension.FailedToUnSuspendUser",
                                                                                           "Failed to UnSuspend user"
                                          );
            public static Error UserNotSuspended => Error.NotFound(
                                                             "Suspension.UserNotSuspended",
                                                                                                                                                       "User is not suspended"
                                                         );
        }
        
    }
}

