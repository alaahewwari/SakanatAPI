using Sieve.Models;

namespace BusinessLogic.Extensions
{
    public static class SieveExtensions
    {
        public static void SetDefaultPagination(this SieveModel sieveModel)
        {
            sieveModel.PageSize ??= 12;
            sieveModel.Page ??= 1;
        }
    }
}
