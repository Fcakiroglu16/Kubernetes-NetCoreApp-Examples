using MassTransit;
using SharedLibrary;

namespace ProducerKafka.API.Consumers;

public class HelloWorldConsumer : IConsumer<KafkaMessage>
{
    private readonly ILogger<HelloWorldConsumer> _logger;

    public HelloWorldConsumer(ILogger<HelloWorldConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<KafkaMessage> context)
    {
        _logger.LogInformation("message comming from kafka : {Message}", context.Message.Message);

        return Task.CompletedTask;
    }
}