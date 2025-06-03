using Microsoft.Extensions.Logging;
using Domain;

namespace Services
{
    public class LogService<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly CorrelationId _correlationId;

        public LogService(ILogger<T> logger, CorrelationId correlationId)
        {
            _logger = logger;
            _correlationId = correlationId;
        }

        public virtual void LogInformation(string message)
        {
            _logger.LogInformation($"[CorrelationId: {_correlationId.Get()}] {message}");
        }

        public virtual void LogError(string message)
        {
            _logger.LogError($"[CorrelationId: {_correlationId.Get()}] {message}");
        }

        public virtual void LogWarning(string message)
        {
            _logger.LogWarning($"[CorrelationId: {_correlationId.Get()}] {message}");
        }
    }
}
