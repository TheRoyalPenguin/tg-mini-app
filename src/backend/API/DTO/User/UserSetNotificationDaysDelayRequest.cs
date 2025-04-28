namespace API.DTO.User;

public class UserSetNotificationDaysDelayRequest
{
    public required int UserId { get; set; }
    public required int NotificationDaysDelay { get; set; }
}