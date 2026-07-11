using Elevate.Auth.Features.CompleteProfile;
using Elevate.Auth.Features.Login;
using Elevate.Auth.Features.Logout;
using Elevate.Auth.Features.RefreshToken;
using Elevate.Auth.Features.Register;
using Elevate.Auth.Features.ResetPassword;
using Elevate.Auth.Features.VerifyOtp;

public static class AuthEndpointsExtensions
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapLoginEndpoint();
        app.MapRegisterEndpoint();
        app.MapVerifyOtpEndpoint();
        app.MapRefreshTokenEndpoint();
        app.MapResetPasswordEndpoint();
        app.MapLogoutEndpoint();
        app.MapCompleteProfileEndpoint();
        app.MapVerifyOtpEndpoint();


    }
}