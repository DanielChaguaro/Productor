using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.IO;

public class RabbitMQConnection
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;

    public RabbitMQConnection()
    {
        // Cargar la configuración desde appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Config/AppSettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Obtener las configuraciones de RabbitMQ
        var rabbitMQConfig = configuration.GetSection("RabbitMQ");
        string hostName = rabbitMQConfig["Host"];
        int port = int.Parse(rabbitMQConfig["Port"]);
        string userName = rabbitMQConfig["User"];
        
        //string password = rabbitMQConfig["Password"];
        // Inicializar la conexión
        _factory = new ConnectionFactory
        {
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = EnvironmentConfig.RabbitMQPassword
        };
    }

    public IConnection GetConnection()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = _factory.CreateConnection();
        }
        return _connection;
    }
    public bool TestConnection()
    {
        try
        {
            using var connection = _factory.CreateConnection();
            Console.WriteLine("Connection to RabbitMQ was successful!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to RabbitMQ: {ex.Message}");
            return false;
        }
    }
}

