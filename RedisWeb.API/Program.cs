using RedisWeb.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(_ =>
{
    var redisService = new RedisService(builder.Configuration);
    redisService.Connect();
    return redisService;
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHealthChecks("/healthz");
app.UseAuthorization();

app.MapControllers();

app.Run();