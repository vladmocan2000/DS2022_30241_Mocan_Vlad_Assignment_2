using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void F(string guid)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var channel = factory.CreateConnection().CreateModel();
            channel.QueueDeclare("energy-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            foreach (string line in System.IO.File.ReadLines("../../../../sensor.csv"))
            {
                var message = new
                {
                    timestamp = DateTime.UtcNow,
                    device_id = guid,
                    measurement_value = line
                };

                channel.BasicPublish("", "energy-queue", null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));

                System.Threading.Thread.Sleep(4000);
            }
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => F("ea5be094-5eec-4f62-bde2-a1f0925fed76"));
            System.Threading.Thread.Sleep(2000);
            Task.Factory.StartNew(() => F("4e1eaa11-0172-406d-9585-494f4b0aa66b"));
            Console.ReadLine();
        }
    }
}