using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

public class SmsNotificationProducer
{
    private readonly IModel _channel;
    private readonly string _exchangeName;

    public SmsNotificationProducer(IModel channel, string exchangeName)
    {
        _channel = channel;
        _exchangeName = exchangeName;
    }

    public void PublishNotification(NotificationMessage message, string routingKey)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: _exchangeName, routingKey: routingKey, basicProperties: null, body: body);
    }
}
