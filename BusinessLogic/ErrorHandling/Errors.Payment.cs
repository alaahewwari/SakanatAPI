
using ErrorOr;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Payment
        {
            public static Error PaymentNotFound => Error.NotFound(
                               "PaymentNotFound",
                                              "Payment not found"
                           );
            public static Error NoPaymentFound => Error.NotFound(
                               "NoPaymentsFound",
                                              "No payments found"
                           );

            public static Error InvalidPayment => Error.Failure(
                               "InvalidPayment",
                                              "Invalid payment");
            public static Error PaymentAmountExceedsRentPrice => Error.Failure(
                                              "PaymentAmountExceedsRentPrice",
                                                                                           "Payment amount exceeds rent price"
                                          );
        }
    }
}
