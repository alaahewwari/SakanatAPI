
using ErrorOr;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Discount
        { 
            public static Error DiscountNotFound => Error.NotFound(
                               "Discount.DiscountNotFound",
                                              "Discount not found"
                                          );
            public static Error NoDiscountsFound => Error.NotFound(
                               "Discount.NoDiscountsFound",
                                              "No discounts found"
                                          );

            public static Error InvalidDiscount => Error.Failure(
                "Discount.InvalidDiscount",
                "Invalid discount");

            public static Error DiscountAlreadyAdded => Error.Failure(
                               "Discount.DiscountAlreadyAdded",
                                              "Discount already added to this apartment");
        }
    }
}
