using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;

namespace RedisWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly ILogger<RedisController> _logger;
        public RedisController(ILogger<RedisController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(
            [FromServices]IDistributedCache cache)
        {
            string nome = cache.GetString("nome");
            if (string.IsNullOrEmpty(nome))
            {
                _logger.LogInformation("nome added to cache");
                DistributedCacheEntryOptions opcoesCache = new();
                opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                cache.SetString("nome", "Alan Nunes da Silva", opcoesCache);
            }
            else
            {
                _logger.LogInformation("nome is already in cache");
            }

            return Ok(nome);
        }
    }
}
