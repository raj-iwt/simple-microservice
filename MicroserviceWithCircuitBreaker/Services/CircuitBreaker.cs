using System;
using System.Threading.Tasks;
namespace MicroserviceWithCircuitBreaker.Services;
public class CircuitBreaker
{
    private int _failureCount = 0;
    private int _failureThreshold = 3;
    private TimeSpan _timeout = TimeSpan.FromMinutes(1);
    private DateTime _lastFailureTime;

    public async Task ExecuteAsync(Func<Task> action)
    {
        if (_failureCount >= _failureThreshold && (DateTime.Now - _lastFailureTime) < _timeout)
        {
            // Circuit is open
            throw new Exception("Circuit breaker is open");
        }

        try
        {
            await action();
            _failureCount = 0; // Reset on success
        }
        catch
        {
            _failureCount++;
            _lastFailureTime = DateTime.Now;
            if (_failureCount >= _failureThreshold)
            {
                // Open circuit
                throw new Exception("Circuit breaker is open");
            }
            throw;
        }
    }
}
