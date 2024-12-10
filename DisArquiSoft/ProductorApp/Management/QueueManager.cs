using RabbitMQ.Client;

public class QueueManager
{
    private readonly IModel _channel;

    public QueueManager(IModel channel)
    {
        _channel = channel;
    }

    public void BindQueueToExchange(string queueName, string exchangeName, string routingKey = "")
    {
        _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
    }

    public void DeclareQueue(string queueName)
    {
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
    }
}
