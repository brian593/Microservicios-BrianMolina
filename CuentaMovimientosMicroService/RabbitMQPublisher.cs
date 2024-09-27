using RabbitMQ.Client;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CuentaMovimientosMicroService
{
    public class RabbitMQPublisher : IDisposable
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            try
            {
                _hostname = configuration["RabbitMQ:Hostname"];
                _queueName = configuration["RabbitMQ:RequestQueueName"];
                int _port = int.Parse(configuration["RabbitMQ:Port"]);
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    UserName = configuration["RabbitMQ:Username"],
                    Password = configuration["RabbitMQ:Password"],
                    Port = _port,
                    // Opcional: agregar un tiempo de espera de conexión
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(5)
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar RabbitMQPublisher: {ex.Message}");
                throw;
            }
        }

        public void PublishValidarCliente(int clienteId)
        {
            try
            {
                var message = $"ClienteId:{clienteId}";
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"[Sent] Solicitud de validación enviada: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al publicar mensaje: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (_channel?.IsOpen == true)
            {
                _channel.Close();
                _channel.Dispose();
            }
            if (_connection?.IsOpen == true)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}