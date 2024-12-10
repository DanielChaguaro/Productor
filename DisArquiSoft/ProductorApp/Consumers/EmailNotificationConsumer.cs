using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class EmailNotificationConsumer
{
    private readonly IModel _channel;

    public EmailNotificationConsumer(IModel channel)
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
            Console.WriteLine($"[Email] Received: {message}");
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine($"Started consuming messages from {queueName}...");
    }
}
