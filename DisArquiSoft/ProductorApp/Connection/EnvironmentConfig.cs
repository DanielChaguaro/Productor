public static class EnvironmentConfig
{
    public static string RabbitMQPassword => Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
}