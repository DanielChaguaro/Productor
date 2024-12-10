public class Notification
{
    public string Channel { get; set; } // email, sms, push
    public string Message { get; set; }
    public string Recipient { get; set; }
}