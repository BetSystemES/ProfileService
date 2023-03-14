using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace ProfileService.GRPC.Interceptors
{
    /// <summary>
    /// Middleware for grpc which validates request models
    /// </summary>
    /// <seealso cref="Grpc.Core.Interceptors.Interceptor" />
    public class ValidationInterceptor : Interceptor
    {
        /// <summary>
        /// Server-side handler for intercepting and incoming unary call.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this method.</typeparam>
        /// <typeparam name="TResponse">Response message type for this method.</typeparam>
        /// <param name="request">The request value of the incoming invocation.</param>
        /// <param name="context">An instance of <see cref="T:Grpc.Core.ServerCallContext" /> representing
        /// the context of the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling
        /// the next interceptor in the chain, or the service request handler,
        /// in case of the last interceptor and return the response value of
        /// the RPC. The interceptor can choose to call it zero or more times
        /// at its discretion.</param>
        /// <returns>
        /// A future representing the response value of the RPC. The interceptor
        /// can simply return the return value from the continuation intact,
        /// or an arbitrary response value as it sees fit.
        /// </returns>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            await CheckRequestMessage(request, context);
            return await continuation(request, context);
        }

        /// <summary>
        /// Server-side handler for intercepting client streaming call.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this method.</typeparam>
        /// <typeparam name="TResponse">Response message type for this method.</typeparam>
        /// <param name="requestStream">The request stream of the incoming invocation.</param>
        /// <param name="context">An instance of <see cref="T:Grpc.Core.ServerCallContext" /> representing
        /// the context of the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling
        /// the next interceptor in the chain, or the service request handler,
        /// in case of the last interceptor and return the response value of
        /// the RPC. The interceptor can choose to call it zero or more times
        /// at its discretion.</param>
        /// <returns>
        /// A future representing the response value of the RPC. The interceptor
        /// can simply return the return value from the continuation intact,
        /// or an arbitrary response value as it sees fit. The interceptor has
        /// the ability to wrap or substitute the request stream when calling
        /// the continuation.
        /// </returns>
        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            await CheckRequestStream(requestStream, context);
            return await continuation(requestStream, context);
        }

        /// <summary>
        /// Server-side handler for intercepting server streaming call.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this method.</typeparam>
        /// <typeparam name="TResponse">Response message type for this method.</typeparam>
        /// <param name="request">The request value of the incoming invocation.</param>
        /// <param name="responseStream">The response stream of the incoming invocation.</param>
        /// <param name="context">An instance of <see cref="T:Grpc.Core.ServerCallContext" /> representing
        /// the context of the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling
        /// the next interceptor in the chain, or the service request handler,
        /// in case of the last interceptor and the interceptor can choose to
        /// call it zero or more times at its discretion. The interceptor has
        /// the ability to wrap or substitute the request value and the response stream
        /// when calling the continuation.</param>
        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            await CheckRequestMessage(request, context);
            await continuation(request, responseStream, context);
        }

        /// <summary>
        /// Server-side handler for intercepting bidirectional streaming calls.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this method.</typeparam>
        /// <typeparam name="TResponse">Response message type for this method.</typeparam>
        /// <param name="requestStream">The request stream of the incoming invocation.</param>
        /// <param name="responseStream">The response stream of the incoming invocation.</param>
        /// <param name="context">An instance of <see cref="T:Grpc.Core.ServerCallContext" /> representing
        /// the context of the invocation.</param>
        /// <param name="continuation">A delegate that asynchronously proceeds with the invocation, calling
        /// the next interceptor in the chain, or the service request handler,
        /// in case of the last interceptor and the interceptor can choose to
        /// call it zero or more times at its discretion. The interceptor has
        /// the ability to wrap or substitute the request and response streams
        /// when calling the continuation.</param>
        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            await CheckRequestStream(requestStream, context);
            await continuation(requestStream, responseStream, context);
        }

        /// <summary>
        /// Validates the request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="token">The token.</param>
        /// <exception cref="Grpc.Core.RpcException"></exception>
        /// <exception cref="Grpc.Core.Status"></exception>
        private async Task ValidateRequest<TRequest>(
            TRequest request,
            IValidator<TRequest> validator,
            CancellationToken token)
        {
            var validationResult = await validator.ValidateAsync(request, token);
            if (!validationResult.IsValid)
            {
                var errors = validationResult
                    .Errors
                    .Select(f => new ValidationFailure(f.PropertyName, f.ErrorMessage))
                    .ToList();

                throw new ValidationException(errors);
            }
        }

        /// <summary>
        /// Checks the request message.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="callContext">The call context.</param>
        private async Task CheckRequestMessage<TRequest>(TRequest request, ServerCallContext callContext)
            where TRequest : class
        {
            var httpContext = callContext.GetHttpContext();
            var cancellationToken = callContext.CancellationToken;
            var validator = httpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                await ValidateRequest(request, validator, cancellationToken);
            }
        }

        /// <summary>
        /// Checks the request stream.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="requestStream">The request stream.</param>
        /// <param name="callContext">The call context.</param>
        private async Task CheckRequestStream<TRequest>(IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext callContext) where TRequest : class
        {
            var httpContext = callContext.GetHttpContext();
            var cancellationToken = callContext.CancellationToken;
            var validator = httpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                do
                {
                    await ValidateRequest(requestStream.Current, validator, cancellationToken);
                } while (await requestStream.MoveNext());
            }
        }
    }
}

