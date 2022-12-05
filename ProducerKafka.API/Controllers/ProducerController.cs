using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace ProducerKafka.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    private readonly ITopicProducer<KafkaMessage> _producer;

    public ProducerController(ITopicProducer<KafkaMessage> producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        const int i = 0;
        await _producer.Produce(new KafkaMessage { Message = $"text-{i}" });

        return Ok();
    }
}