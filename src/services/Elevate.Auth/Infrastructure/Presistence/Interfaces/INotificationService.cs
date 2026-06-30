
public interface INotificationService
{
    Task SendOtpAsync(string email, string otpCode, CancellationToken ct = default);
}