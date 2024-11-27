using System;
using System.Text;
using RabbitMQ.Client;

namespace ProductorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configuración de conexión a RabbitMQ
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // Cambiar si RabbitMQ está en otro servidor
                UserName = "guest",
                Password = "guest"
            };

            Console.WriteLine("Escribe el destinatario del correo electrónico:");
            string recipient = Console.ReadLine();

            //Console.WriteLine("Escribe el asunto del correo electrónico:");
            //string subject = Console.ReadLine();

            Console.WriteLine("Escribe el cuerpo del correo electrónico:");
            string bodyContent = Console.ReadLine();

            // Crear un mensaje que represente al correo electrónico
            //Asunto: {subject}\n
            var emailMessage = $"Destinatario: {recipient}\nCuerpo: {bodyContent}";

            // Crear conexión y canal
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            const string queueName = "email-queue";

            // Declarar la cola (si no existe, se crea automáticamente)
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Convertir el mensaje a bytes
            var body = Encoding.UTF8.GetBytes(emailMessage);

            // Publicar el mensaje en la cola
            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body);

            Console.WriteLine("[x] Correo electrónico enviado a la cola:");
            Console.WriteLine(emailMessage);
        }
    }
}
