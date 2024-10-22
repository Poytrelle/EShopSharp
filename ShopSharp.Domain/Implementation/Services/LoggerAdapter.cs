using Microsoft.Extensions.Logging;
using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class LoggerAdapter<T>(ILoggerFactory _loggerFactory) : IAppLogger<T>
{
    private readonly ILogger<T> _logger = _loggerFactory.CreateLogger<T>();

    public void LogWarning(string message, params object[] args) =>
        _logger.LogWarning(message, args);

    public void LogInformation(string message, params object[] args) =>
        _logger.LogInformation(message, args);
}
