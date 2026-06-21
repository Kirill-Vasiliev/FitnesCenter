namespace FitnesCenter.Shared.Exceptions;

/// <summary>
/// Исключение, когда ресурс не найден (HTTP 404)
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, object id)
        : base($"Сущность '{entityName}' с ID '{id}' не найдена") { }
}