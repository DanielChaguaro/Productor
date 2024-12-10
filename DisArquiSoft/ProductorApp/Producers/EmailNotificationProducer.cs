using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

public class EmailNotificationProducer
{
    private readonly IModel _channel;
    private readonly string _exchangeName;

    public EmailNotificationProducer(IModel channel, string exchangeName)
    {
        _channel = channel;
        _exchangeName = exchangeName;
    }

    public void PublishNotification(NotificationMessage message)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: _exchangeName, routingKey: "", basicProperties: null, body: body);
    }
}

