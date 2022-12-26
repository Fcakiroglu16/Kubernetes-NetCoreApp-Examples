using System.Text.Json;

namespace Consumer.API.Kafka
{
    public record UserNameChangedEvent
    {
        public int UserId { get; init; }
        public string Name { get; init; } = null!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}