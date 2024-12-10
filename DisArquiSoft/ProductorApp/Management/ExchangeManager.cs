using RabbitMQ.Client;

public class ExchangeManager
{
    private readonly IModel _channel;

    public ExchangeManager(IModel channel)
    {
        _channel = channel;
    }

    public void DeclareFanoutExchange(string exchangeName)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout, durable: true);
    }

    public void DeclareTopicExchange(string exchangeName)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic, durable: true);
    }
}
