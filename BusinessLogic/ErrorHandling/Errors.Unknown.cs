using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class Unknown
    {
        public static Error Create(Exception ex)
        {
            var metadata = new Dictionary<string, object>();

            if (ex.InnerException is not null)
                metadata.Add("innerException", ex.InnerException.Message);

            return Error.Unexpected(
                "UnknownError",
                ex.Message,
                metadata);
        }

        public static Error Create(string message)
        {
            return Error.Unexpected(
                "UnknownError",
                message);
        }
    }
}