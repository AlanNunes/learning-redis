using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace RedisWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index(
            [FromServices]IDistributedCache cache)
        {
            string nome = cache.GetString("nome");
            if (string.IsNullOrEmpty(nome))
            {
                DistributedCacheEntryOptions opcoesCache = new();
                opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                cache.SetString("nome", "Alan Nunes da Silva", opcoesCache);
            }

            return Ok(nome);
        }
    }
}
