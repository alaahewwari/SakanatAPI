using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Presentation.Utils
{
    public class WhatsAppVerification
    {
        public static string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999); // Generates a 6-digit random number
            return otp.ToString();
        }

    }
}
