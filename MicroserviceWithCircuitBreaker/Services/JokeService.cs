using System.Net.Http;
using System.Threading.Tasks;

namespace MicroserviceWithCircuitBreaker.Services;

public class JokeService
{
    private readonly HttpClient _httpClient;
    private readonly CircuitBreaker _circuitBreaker;

    public JokeService(HttpClient httpClient, CircuitBreaker circuitBreaker)
    {
        _httpClient = httpClient;
        _circuitBreaker = circuitBreaker;
    }

    public async Task<string?> GetRandomJokeAsync()
    {
        string? joke = null;
        await _circuitBreaker.ExecuteAsync(async () =>
        {
            joke = await _httpClient.GetStringAsync("https://official-joke-api.appspot.com/random_joke");
        });
        return joke;
    }
}
