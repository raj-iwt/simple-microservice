using MicroserviceWithCircuitBreaker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroserviceWithCircuitBreaker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JokeController(JokeService jokeService) : ControllerBase
{
    private readonly JokeService _jokeService = jokeService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var joke = await _jokeService.GetRandomJokeAsync();
        return Ok(joke);
    }
}
