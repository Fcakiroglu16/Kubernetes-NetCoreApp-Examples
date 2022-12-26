// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Confluent.Kafka.Admin;

Console.WriteLine("Hello, World!");

await CreateTopic();

await SendMessage();
async Task CreateTopic()
{
    using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = "localhost:9092" }).Build();
    try
    {
        await adminClient.CreateTopicsAsync(new[] {
            new TopicSpecification { Name = "mytopic", ReplicationFactor = 1, NumPartitions = 1 } });
    }
    catch (CreateTopicsException e)
    {
        Console.WriteLine(e.Message);
    }
}

async Task SendMessage()
{
    var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

    using var producer = new ProducerBuilder<Null, string>(config).Build();

    foreach (var item in Enumerable.Range(1, 10).ToList())
    {
        var result = await producer.ProduceAsync("mytopic", new Message<Null, string> { Value = $"a log message {item}", Timestamp = Timestamp.Default });
        foreach (var propertyInfo in result.GetType().GetProperties())
        {
            Console.WriteLine($"{propertyInfo.Name} = {propertyInfo.GetValue(result)}");
        }
        Console.WriteLine("---------------");
        await Task.Delay(500);
    }
}