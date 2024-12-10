public class NotificationMessage
{
    public string Type { get; set; } // email, sms, push
    public string Recipient { get; set; }
    public string Content { get; set; }
}
