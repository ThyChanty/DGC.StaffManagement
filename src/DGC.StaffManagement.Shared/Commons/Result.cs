using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DGC.StaffManagement.Shared.Commons
{
    [Serializable]
    public class Result : IResult
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,  new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public Result()
        {
        }
        [DataMember(Order = 1)]
        public string Message { get; set; }
        [DataMember(Order = 3)]
        public List<Error>? Errors { get; set; }

        [DataMember(Order = 4)]
        public int StatusCode { get; set; } = (int) HttpStatusCode.OK;
        [DataMember(Order = 2)]
        public bool Succeeded { get; set; }
        [DataMember(Order = 5)]
        public string CorrelationId { get; set; }

        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }

        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Message = message };
        }
        
        public static IResult Fail(string message, string correlationId)
        {
            return new Result { Succeeded = false, Message = message, CorrelationId = correlationId};
        }

        public static IResult Fail(int statusCode, string message, string correlationId)
        {
            return new Result { Succeeded = false, Message = message, StatusCode = statusCode, CorrelationId = correlationId };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }
        
        public static Task<IResult> FailAsync(string message, string correlationId)
        {
            return Task.FromResult(Fail(message, correlationId));
        }

        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Message = message };
        }
        
        public static IResult Success(string message, string correlationId)
        {
            return new Result { Succeeded = true, Message = message, CorrelationId = correlationId};
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
        
        public static Task<IResult> SuccessAsync(string message, string correlationId)
        {
            return Task.FromResult(Success(message, correlationId));
        }
    }

    [Serializable]
    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }
        [DataMember(Order = 4)]
        public T Data { get; set; }

        public new static Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }

        public new static Result<T> Fail(string message)
        {
            return new Result<T> { Succeeded = false, Message = message };
        }
        
        public new static Result<T> Fail(string message, string correlationId)
        {
            return new Result<T> { Succeeded = false, Message = message, CorrelationId = correlationId};
        }

        public new static Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }
        
        public new static Task<Result<T>> FailAsync(string message, string correlationId)
        {
            return Task.FromResult(Fail(message, correlationId));
        }

        public new static Result<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }

        public new static Result<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Message =message };
        }
        
        public new static Result<T> Success(string message, string correlationId)
        {
            return new Result<T> { Succeeded = true, Message = message, CorrelationId = correlationId};
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }
        
        public static Result<T> Success(T data, string correlationId)
        {
            return new Result<T> { Succeeded = true, Data = data, CorrelationId = correlationId};
        }


        public static Result<T> Success(T data, string message, string correlationId)
        {
            return new Result<T> { Succeeded = true, Data = data, Message =message, CorrelationId = correlationId};
        }


        public new static Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public new static Task<Result<T>> SuccessAsync(string message, string correlationId)
        {
            return Task.FromResult(Success(message, correlationId));
        }
        
        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<Result<T>> SuccessAsync(T data, string correlationId)
        {
            return Task.FromResult(Success(data, correlationId));
        }
        
        public static Task<Result<T>> SuccessAsync(T data, string message, string correlationId)
        {
            return Task.FromResult(Success(data, message, correlationId));
        }
    }
}