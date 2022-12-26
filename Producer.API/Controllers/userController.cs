using Microsoft.AspNetCore.Mvc;
using Producer.API.Kafka;

namespace Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BusProducer<UserNameChangedEvent> _busProducer;

        public UserController(BusProducer<UserNameChangedEvent> busProducer)
        {
            _busProducer = busProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent()
        {
            _busProducer.SetTopic(KafkaConst.UserNameChangeEventTopicName);

            await _busProducer.Produce(new UserNameChangedEvent() { Name = "Ahmet", UserId = 1 });
            return Ok();
        }
    }
}