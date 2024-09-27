using System;
using System.Text;
using ClientePersonaMicroService.Infrastructure.DataBase;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ClientePersonaMicroService
{
    public class RabbitMQConsumer : IDisposable
    {
        private readonly string _hostname;
        private readonly string _requestQueueName;
        private readonly string _responseQueueName;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQConsumer(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _hostname = configuration["RabbitMQ:Hostname"];
            _requestQueueName = configuration["RabbitMQ:RequestQueueName"];
            _responseQueueName = configuration["RabbitMQ:ResponseQueueName"];
            _serviceScopeFactory = serviceScopeFactory;
            int _port = int.Parse(configuration["RabbitMQ:Port"]);


            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                Port=_port
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _requestQueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[Received] Mensaje recibido: {message}");

                var clienteId = int.Parse(message.Split(':')[1]);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var cliente = context.Clientes.Find(clienteId);

                    var responseMessage = cliente != null ? "ClienteValido" : "ClienteInvalido";

                    var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

                    _channel.BasicPublish(exchange: "",
                                         routingKey: _responseQueueName,
                                         basicProperties: null,
                                         body: responseBytes);

                    Console.WriteLine($"[Sent] Respuesta enviada: {responseMessage}");
                }
            };

            _channel.BasicConsume(queue: _requestQueueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Esperando mensajes...");
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (_channel != null && _channel.IsOpen)
            {
                _channel.Close();
            }
            if (_connection != null && _connection.IsOpen)
            {
                _connection.Close();
            }
        }
    }
}

