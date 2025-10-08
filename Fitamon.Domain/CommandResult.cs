using System.Text.Json.Serialization;

namespace Seyat.Shared.Domain.Dtos;

public class CommandResult
{
    public CommandResult() { }
    public CommandResult(bool succeed, string message)
    {
        Succeed = succeed;
        Message = message;
    }

    public CommandResult(bool succeed, string message, dynamic? data)
    {
        Succeed = succeed;
        Message = message;
        Data = data;
    }
    public CommandResult(
        bool succeed,
        string message,
        int errorCode,
        dynamic? data)
    {
        Succeed = succeed;
        Message = message;
        ErrorCode = errorCode;
        Data = data;
    }
    public bool Succeed { get; set; }
    public string Message { get; set; } = default!;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Data { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ErrorCode { get; set; }
}