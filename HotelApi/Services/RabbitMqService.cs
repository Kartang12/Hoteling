using HotelingLibrary.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HotelApi.Services
{
    public interface IRabbitMqService
    {
        void SendMessage(MessageBase obj, string exchangeName);
        void SendMessage(string message, string exchangeName);
    }

    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(MessageBase obj, string exchangeName)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message, exchangeName);
        }

        public void SendMessage(string message, string exchangeName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: $"{exchangeName}", type: ExchangeType.Fanout, durable: true);
                //channel.QueueDeclare(queue: "HotelChangedQueue",
                //               durable: true,
                //               exclusive: false,
                //               autoDelete: false,
                //               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: $"{exchangeName}",
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            }
        }
    }
}
