// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;

Console.WriteLine("Message is reading");

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "mygroup-l",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = false
};

var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("mytopic");

while (true)
{
    var consumeResult = consumer.Consume();

    Console.WriteLine(consumeResult.Message.Value);

    //“at least once” delivery semantics
    try
    {
        consumer.Commit(consumeResult);
    }
    catch (KafkaException e)
    {
        Console.WriteLine($"Commit error: {e.Error.Reason}");
    }
}

//consumer.Close();