﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receive
{
    class Receive
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    //Idempotent - created by publisher or consumer
                    channel.QueueDeclare(queue: "spinksy", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "spinksy", autoAck: true, consumer: consumer);

                    Console.WriteLine("Press [enter] to exit. ");
                    Console.ReadLine();


                }
            }
        }
    }
}
