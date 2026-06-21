namespace FitnesCenter.Shared.Exceptions;

/// <summary>
/// Исключение при ошибке валидации (HTTP 400)
/// </summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}