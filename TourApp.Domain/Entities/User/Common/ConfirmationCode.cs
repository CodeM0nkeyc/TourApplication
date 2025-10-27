namespace TourApp.Domain.Entities.User.Common;

public class ConfirmationCode
{
    public int Code { get; set; }
    public DateTime ExpireAt { get; set; }
}