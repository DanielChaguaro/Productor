using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class OrderNotificationConsumer
{
    private readonly IModel _channel;

    public OrderNotificationConsumer(IModel channel)
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
            Console.WriteLine($"[x] Received message from {queueName}: {message}");
        };

        _channel.BasicConsume(queue: queueName,
                              autoAck: true,
                              consumer: consumer);
    }
}
