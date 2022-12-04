using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly RedisService _redisService;

        public RedisController(RedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpPost]
        public IActionResult Set()
        {
            _redisService.GetDb(0).StringSet("name", "asp.net core redis");
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_redisService.GetDb(0).StringGet("name").ToString());
        }
    }
}
