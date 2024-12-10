using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;

public class SmsNotificationConsumer
{
    private readonly IModel _channel;

    public SmsNotificationConsumer(IModel channel)
    {
        _channel = channel;
    }

    public void StartConsuming(string queueName)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[SMS] Received: {message}");
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine($"Started consuming messages from {queueName}...");
    }
}
