using System;
using System.Text;
using RabbitMQ.Client;

namespace ProductorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configuración de conexión
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // Cambiar según la dirección de tu RabbitMQ
                UserName = "guest",
                Password = "guest"
            };

            Console.WriteLine("Escribe el mensaje que deseas enviar a la cola:");
            string message = Console.ReadLine();

            // Crear conexión y canal
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            const string queueName = "mi-cola";

            // Declarar la cola
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Convertir el mensaje a bytes
            var body = Encoding.UTF8.GetBytes(message);

            // Publicar el mensaje
            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body);

            Console.WriteLine($"[x] Mensaje enviado: {message}");
        }
    }
}