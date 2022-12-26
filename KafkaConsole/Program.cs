// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Confluent.Kafka.Admin;

Console.WriteLine("Hello, World!");

using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = "localhost:9092" }).Build())
{
    try
    {
        await adminClient.CreateTopicsAsync(new TopicSpecification[] {
            new TopicSpecification { Name = "mytopic", ReplicationFactor = 1, NumPartitions = 1 } });
    }
    catch (CreateTopicsException e)
    {
        Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
    }
}

//var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

//using var producer = new ProducerBuilder<string, string>(config).Build();

//var deliveryReport = await producer.ProduceAsync(
//    "hello_world", new Message<string, string> { Key = "name", Value = "asp.net mvc" });

//Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");