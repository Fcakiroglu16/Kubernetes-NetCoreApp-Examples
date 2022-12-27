using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Producer.API.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(BusProducer<>));

var app = builder.Build();

#region Check Topic Status

var host = builder.Configuration.GetSection("KafkaSettings")["Host"];
var port = builder.Configuration.GetSection("KafkaSettings")["Port"];

using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = $"{host}:{port}" }).Build();
try
{
    await adminClient.CreateTopicsAsync(new[] {
        new TopicSpecification { Name = KafkaConst.UserNameChangeEventTopicName, ReplicationFactor = 1, NumPartitions = 3 } });
    Console.WriteLine($"Topic({KafkaConst.UserNameChangeEventTopicName}) has created ");
}
catch (CreateTopicsException e)
{
    Console.WriteLine(e.Message);
}

#endregion Check Topic Status

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();