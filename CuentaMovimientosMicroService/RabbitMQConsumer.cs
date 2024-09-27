using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CuentaMovimientosMicroService
{
    

    public class RabbitMQConsumer : IDisposable
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;
        private TaskCompletionSource<bool> _tcs;

        public RabbitMQConsumer(IConfiguration configuration)
        {
            _hostname = configuration["RabbitMQ:Hostname"];
            _queueName = configuration["RabbitMQ:ResponseQueueName"];
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

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public Task<bool> WaitForValidationResponseAsync()
        {
            _tcs = new TaskCompletionSource<bool>();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[Received] Respuesta recibida: {message}");

                if (message == "ClienteValido")
                {
                    _tcs.SetResult(true);
                }
                else
                {
                    _tcs.SetResult(false);
                }
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return _tcs.Task;
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

