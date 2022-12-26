using Confluent.Kafka;
using Producer.API.Kafka;

namespace Consumer.API.Kafka
{
    public class BusConsumer<T>
    {
        private readonly string? _host;
        private readonly string? _port;
        private string _topic = null!;
        private readonly ILogger<BusConsumer<UserNameChangedEvent>> _logger;

        public BusConsumer(IConfiguration configuration, ILogger<BusConsumer<UserNameChangedEvent>> logger)
        {
            _logger = logger;
            _host = configuration.GetSection("KafkaSettings")["Host"];
            _port = configuration.GetSection("KafkaSettings")["Port"];
        }

        public void SetTopic(string topic)
        {
            _topic = topic;
        }

        public async Task Consume(CancellationToken token)
        {
            if (string.IsNullOrEmpty(_topic))
                throw new ArgumentNullException(nameof(_topic));

            var config = new ConsumerConfig()
            {
                BootstrapServers = $"{_host}:{_port}",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
            var consumer = new ConsumerBuilder<Ignore, T>(config).SetValueDeserializer(new CustomValueDeserializer<T>()).Build();
            consumer.Subscribe(_topic);

            while (!token.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken: token);

                _logger.LogInformation(consumeResult.Message.Value!.ToString());

                //“at least once” delivery semantics
                try
                {
                    consumer.Commit(consumeResult);
                }
                catch (KafkaException e)
                {
                    _logger.LogError($"Commit error: {e.Error.Reason}");
                }

                await Task.Delay(2000, token);
            }
        }
    }
}