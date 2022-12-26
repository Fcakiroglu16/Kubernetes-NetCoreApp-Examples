using Consumer.API.Kafka;

namespace Consumer.API.BackgroundServices
{
    public class UserNameChangedEventConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserNameChangedEventConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var busConsumer = scope.ServiceProvider.GetRequiredService<BusConsumer<UserNameChangedEvent>>();
            busConsumer.SetTopic(KafkaConst.UserNameChangeEventTopicName);
            await busConsumer.Consume(stoppingToken);
        }
    }
}