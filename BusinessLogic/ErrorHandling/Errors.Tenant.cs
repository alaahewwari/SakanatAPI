
using ErrorOr;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Tenant
        {
            public static Error NoTenantsFound => Error.NotFound(
                               "NoTenantsFound",
                                              "No tenants found"
                           );
            public static Error TenantNotFound => Error.NotFound(
                "TenantNotFound",
                               "Tenant not found"
                );
            public static Error TenantAlreadyExists => Error.Conflict(
                               "TenantAlreadyExists",
                                              "Tenant already exists"
                               );
            
            public static Error TenantHasContractOnThisTime => Error.Conflict(
                               "TenantHasContractOnThisTime",
                                              "Tenant has contract on this time"
                               );
        }
    }
}
