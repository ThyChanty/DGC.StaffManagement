using Newtonsoft.Json;

namespace DGC.StaffManagement.Shared.Commons
{
    public interface IResult
    {
        [JsonProperty("success", Order = 0)]
        bool Succeeded { get; set; }
        [JsonProperty("message", Order = 1)]
        string Message { get; set; }
        [JsonProperty("Errors", Order = 3)]
        List<Error>? Errors { get; set; }
        [JsonProperty("statusCode", Order = 2)]
        int StatusCode { get; set; }
        [JsonProperty("correlationId", Order = 4)]
        string CorrelationId { get; set; }

    }

    public interface IResult<out T> : IResult
    {
        [JsonProperty("data", Order = 5)]
        T Data { get; }
    }
    
    
    public class Error
    {
        public string? Field { get; set; }
        public List<ErrorMessage>? ErrorMessages { get; set; }
    }

    public class ErrorMessage
    {
        public string? EnMessage { get; set; }
        public string? KhMessage { get; set; }
        public string? ErrorCode { get; set; }
    }
}