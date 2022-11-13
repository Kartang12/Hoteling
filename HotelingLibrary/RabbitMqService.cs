//using HotelingLibrary.Messages;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;

//namespace HotelingLibrary
//{
//    public interface IRabbitMqService
//    {
//        void SendMessage(MessageBase obj, string queueName);
//        void SendMessage(string message, string queueName);
//    }

//    public class RabbitMqService : IRabbitMqService
//    {
//        public void SendMessage(MessageBase obj, string queueName)
//        {
//            var message = JsonSerializer.Serialize(obj);
//            SendMessage(message, queueName);
//        }

//        public void SendMessage(string message, string queueName)
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost" };
//            using (var connection = factory.CreateConnection())
//            using (var channel = connection.CreateModel())
//            {
//                channel.ExchangeDeclare(exchange: "HotelChanged", type: ExchangeType.Fanout);
//                //channel.QueueDeclare(queue: "MyQueue",
//                //               durable: false,
//                //               exclusive: false,
//                //               autoDelete: false,
//                //               arguments: null);

//                var body = Encoding.UTF8.GetBytes(message);

//                channel.BasicPublish(exchange: "",
//                               routingKey: "MyQueue",
//                               basicProperties: null,
//                               body: body);
//            }
//        }
//    }
//}
