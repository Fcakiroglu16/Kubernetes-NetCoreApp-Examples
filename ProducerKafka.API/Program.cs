using MassTransit;
using SharedLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
    // x.AddConsumer<HelloWorldConsumer>();
    x.AddRider(rider =>
    {
        rider.AddProducer<KafkaMessage>("hello_world");
        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");
            // k.TopicEndpoint<KafkaMessage>("hello_world", "consumer-group-a", e =>
            // {
            //     e.ConfigureConsumer<HelloWorldConsumer>(context);
            // });
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // var producer = scope.ServiceProvider.GetRequiredService<ITopicProducer<KafkaMessage>>();
    //
    // foreach (var i in Enumerable.Range(1,10).ToList())
    // {
    //     await producer.Produce(new KafkaMessage() { Message = $"text-{i}" });
    // }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();