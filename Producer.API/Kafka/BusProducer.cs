using Confluent.Kafka;

namespace Producer.API.Kafka
{
    public class BusProducer<T>
    {
        private readonly string? _host;
        private readonly string? _port;
        private string _topic = null!;

        public BusProducer(IConfiguration configuration)
        {
            _host = configuration.GetSection("KafkaSettings")["Host"];
            _port = configuration.GetSection("KafkaSettings")["Port"];
        }

        public void SetTopic(string topic)
        {
            _topic = topic;
        }

        public async Task Produce(T data)
        {
            if (string.IsNullOrEmpty(_topic))
                throw new ArgumentNullException(nameof(_topic));

            var config = new ProducerConfig { BootstrapServers = $"{_host}:{_port}" };

            using var producer = new ProducerBuilder<Null, T>(config).SetValueSerializer(new CustomValueSerializer<T>()).Build();

            await producer.ProduceAsync(_topic, new Message<Null, T> { Value = data });

            Console.WriteLine("Event has been sent");
        }
    }
}