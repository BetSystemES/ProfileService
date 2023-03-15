using Grpc.Core;
using Grpc.Core.Interceptors;
using ProfileService.GRPC.Interceptors.Helpers;

namespace ProfileService.GRPC.Interceptors
{
    /// <summary>
    /// Middleware for grpc which handle exceptions
    /// </summary>
    public class ErrorHandlingInterceptor : Interceptor
    {
        private readonly ILogger<ErrorHandlingInterceptor> _logger;

        public ErrorHandlingInterceptor(ILogger<ErrorHandlingInterceptor> logger)
        {
            _logger = logger;
        }

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
        /// <exception cref="RpcException"></exception>
        /// <exception cref="Status"></exception>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                throw ex.Handle(context, _logger);
            }
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
        /// <exception cref="RpcException"></exception>
        /// <exception cref="Status"></exception>
        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(requestStream, context);
            }
            catch (Exception ex)
            {
                throw ex.Handle(context, _logger);
            }
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
        /// <exception cref="RpcException"></exception>
        /// <exception cref="Status"></exception>
        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation(request, responseStream, context);
            }
            catch (Exception ex)
            {
                throw ex.Handle(context, _logger);
            }
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
        /// <exception cref="RpcException"></exception>
        /// <exception cref="Status"></exception>
        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation(requestStream, responseStream, context);
            }
            catch (Exception ex)
            {
                throw ex.Handle(context, _logger);
            }
        }
    }
}