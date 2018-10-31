using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            string result = "";

            do
            {
                result = SendMessage();

            } while (result == "s");
        }

        static string SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "spinksy", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    string message = "Hello from Spinksy";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "spinksy", basicProperties: null, body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine("\n Press [s] to send another message\n Press [enter] to exit.");
            return Console.ReadLine();
        }
    }
}
