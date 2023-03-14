using FluentValidation;
using Grpc.Core;
using Newtonsoft.Json;

namespace ProfileService.GRPC.Interceptors.Helpers
{
    public static class ExceptionHelpers
    {
        public static RpcException Handle<T>(this Exception exception, ServerCallContext context, ILogger<T> logger)
        {
            return exception switch
            {
                TimeoutException => HandleTimeoutException((TimeoutException)exception, context, logger),
                RpcException => HandleRpcException((RpcException)exception, logger),
                ValidationException => HandleValidationException((ValidationException)exception, logger),
                _ => HandleDefault(exception, context, logger)
            };
        }

        private static RpcException HandleTimeoutException<T>(TimeoutException exception, ServerCallContext context, ILogger<T> logger)
        {
            logger.LogError(exception, $"An error occurred");
            var status = new Status(StatusCode.Internal, "An external resource did not answer within the time limit");
            return new RpcException(status);
        }

        private static RpcException HandleValidationException<T>(ValidationException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An ValidationException occurred");
            var details = exception.Errors
                .Select(x => new GrpcExceptionDetail(x.PropertyName, x.ErrorMessage));
            var statusMessage = new StatusMessage()
            {
                IsSuccessful = false,
                Reason = exception.GetType().Name,
                Details = details
            };
            var exeptionMessageString = JsonConvert.SerializeObject(statusMessage);
            var status = new Status(StatusCode.Internal, exeptionMessageString);
            return new RpcException(status);
        }

        private static RpcException HandleRpcException<T>(RpcException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An error occurred");
            return new RpcException(new Status(exception.StatusCode, exception.Message));
        }

        private static RpcException HandleDefault<T>(Exception exception, ServerCallContext context, ILogger<T> logger)
        {
            logger.LogError(exception, $"An error occurred");
            return new RpcException(new Status(StatusCode.Internal, exception.Message));
        }
    }
}