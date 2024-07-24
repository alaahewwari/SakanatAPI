using BusinessLogic.Constants.Email;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace Presentation.Controllers;
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class IdentityController(
    IAuthenticationServicesFacade authenticationServicesFacade)
    : ApiController
{
    [HttpPost]
    [Route(ApiRoutes.Identity.UserRegistration)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request)
    {
        var baseUrl = EmailUrlConstants.EmailVerificationUrl; // Base URL of React app

    var confirmationEmailUrl = $"{baseUrl}?Email={request.Email}";

        var result = await authenticationServicesFacade.RegisterUserAsync(request, confirmationEmailUrl);
        return result.Match(
                       token => Ok(new AuthenticationResponse { Token = token }),
                                  Problem
                                         );
    }
    [HttpGet]
    [Route(ApiRoutes.Identity.ConfirmEmail)]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var result = await authenticationServicesFacade.ConfirmEmailAsync(email, token);
        return result.Match(
                       Ok,
                                  Problem
                                         );
    }
    [HttpGet]
    [Route(ApiRoutes.Identity.SendOtpMessageOnWhatsapp)]
    public async Task<IActionResult> SendOtpMessageOnWhatsapp(string phoneNumber)
    {
        try
        {
            // Initialize Twilio client with environment variables for better security
            TwilioClient.Init("ACb00a49b257f6d37953b0279b655ee837", "9b3767e13996cdcfdad879fa24c93aaf");
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:"+phoneNumber));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Your appointment is coming up on July 21 at 3PM";
            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            return Ok(new { MessageId = message.Sid, Status = "Sent" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Failed to send OTP. Please try again later.");
        }
    }
    [HttpPost]
    [Route(ApiRoutes.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        var loginResponse = await authenticationServicesFacade.LoginUserAsync(request);

        return loginResponse.Match(
            Ok,
            Problem
        );
    }
    [HttpPost]
    [Route(ApiRoutes.Identity.RefreshToken)]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var result = await authenticationServicesFacade.RefreshTokenAsync(refreshToken);
        return result.Match(
                       Ok,
                   Problem
                        );
    }
    [HttpPost]
    [Route(ApiRoutes.Identity.ForgotPassword)]
    public async Task<IActionResult> ForgotPassword(ForgetPasswordRequestDto request)
    {
        var baseUrl = EmailUrlConstants.PasswordResetUrl; // Base URL of React app 
        var encodedEmail = Uri.EscapeDataString(request.Email);
        var resetPasswordUrl = $"{baseUrl}?Email={encodedEmail}";
        var result = await authenticationServicesFacade.ForgotPasswordAsync(request.Email, resetPasswordUrl);
        return result.Match(
            token =>
            Ok(new AuthenticationResponse { Token = token }),
            Problem
            );
    }
    [HttpGet]
    [Route(ApiRoutes.Identity.ResetForgottenPassword)]
    public async Task<IActionResult> ResetForgottenPassword([FromQuery] string token, [FromQuery] string email)
    {
         var isValidToken = await authenticationServicesFacade.VerifyUserTokenAsync(token,email);
        if (isValidToken.IsError)
        {
            return Problem(isValidToken.Errors);
        }
        return Ok(isValidToken.Value);
    }
    [HttpPost]
    [Route(ApiRoutes.Identity.ResetForgottenPassword)]
    public async Task<IActionResult> ResetForgottenPassword(ResetPasswordRequestDto resetPassword)
    {
        var resetPasswordResponse = await authenticationServicesFacade.ResetPasswordAsync(resetPassword);
        return resetPasswordResponse.Match(
            Ok,
            Problem
        );
    }
}