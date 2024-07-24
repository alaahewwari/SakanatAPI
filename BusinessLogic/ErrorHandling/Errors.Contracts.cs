using System;
using ErrorOr;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Contracts
        {
            public static Error ContractNotFound => Error.NotFound(
                               "ContractNotFound",
                                              "Contract not found"
                           );
            public static Error NoContractsFound => Error.NotFound(
                               "NoContractsFound",
                                              "No contracts found"
                           );

            public static Error InvalidContract => Error.Failure(
                               "InvalidContract",
                                              "Invalid contract");
        }
    }
}
