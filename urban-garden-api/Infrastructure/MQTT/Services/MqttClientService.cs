using MQTTnet;
using MQTTnet.Protocol;
using System.Text;

namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{
    public class MqttClientService : IMqttService
    {
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _options;

        public event Func<string, string, Task>? OnMessageReceived;

        public MqttClientService(string? mqttBrokerIp, string? mqttBrokerPort)
        {
            var factory = new MqttClientFactory();

            _client = factory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttBrokerIp, int.TryParse(mqttBrokerPort, out var port) ? port : 1883)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();

            _client.ApplicationMessageReceivedAsync += async e => 
            { 
                var topic = e.ApplicationMessage.Topic; 
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload); 

                if (OnMessageReceived != null) 
                await OnMessageReceived.Invoke(topic, payload); };
        }

        public async Task Connect(CancellationToken cancellationToken)
        {
            if (_client.IsConnected)
                return;

            var result = await _client.ConnectAsync(_options, cancellationToken);

            if (result.ResultCode == MqttClientConnectResultCode.Success)
                Console.WriteLine("MQTT conectado");
            else
                Console.WriteLine($"Error al conectar: {result.ResultCode}");
        }

        public async Task Disconnect(CancellationToken cancellationToken)
        {
            if (!_client.IsConnected)
                return;

            await _client.DisconnectAsync();
            Console.WriteLine("MQTT desconectado");
        }

        public async Task Publish(string topic, string payload)
        {
            if (!_client.IsConnected)
                throw new InvalidOperationException("MQTT no está conectado");

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await _client.PublishAsync(message);
        }

        public async Task Subscribe(string topic)
        {
            if (!_client.IsConnected)
                throw new InvalidOperationException("MQTT no está conectado");

            await _client.SubscribeAsync(topic);

            Console.WriteLine($"Suscripto a {topic}");
        }
    }
}