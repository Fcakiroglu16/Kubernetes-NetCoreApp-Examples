// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

Console.WriteLine("Hello, World!");


var config = new ProducerConfig { BootstrapServers = "localhost:9098" };

using var producer = new ProducerBuilder<string, string>(config).Build();

var deliveryReport = await producer.ProduceAsync(
    "hello_world", new Message<string, string> { Key = "name", Value = "asp.net mvc" });

Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");