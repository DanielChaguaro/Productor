﻿using RabbitMQ.Client;

class Program
{
    static void Main(string[] args)
    {
        var connection = new RabbitMQConnection().GetConnection();
        using var channel = connection.CreateModel();

        var rabbitMQConnection = new RabbitMQConnection();


        // Initialize managers
        var exchangeManager = new ExchangeManager(channel);
        var queueManager = new QueueManager(channel);

        // Declare exchanges
        exchangeManager.DeclareFanoutExchange("notification_fanout_exchange");
        exchangeManager.DeclareTopicExchange("notification_topic_exchange");

        // Declare and bind queues
        queueManager.DeclareQueue("email_notification_queue");
        queueManager.DeclareQueue("sms_notification_queue");

        queueManager.BindQueueToExchange("email_notification_queue", "notification_fanout_exchange");
        queueManager.BindQueueToExchange("sms_notification_queue", "notification_topic_exchange", "sms.*");

        // Start consumers
        var emailConsumer = new EmailNotificationConsumer(channel);
        emailConsumer.StartConsuming("email_notification_queue");

        var smsConsumer = new SmsNotificationConsumer(channel);
        smsConsumer.StartConsuming("sms_notification_queue");

        var exchangeName = "notification_topic_exchange";
        var emailProducer = new EmailNotificationProducer(channel, exchangeName);

        // Crear un mensaje de prueba
        var message = new NotificationMessage
        {
            Type  = "user@example.com",
            Recipient = "Order Confirmation",
            Content = "Your order has been confirmed!"
        };

        // Publicar el mensaje
        emailProducer.PublishNotification(message);

        Console.WriteLine("Consumers started. Press [enter] to exit.");
        Console.ReadLine();
    }
}

