using Grpc.Net.Client;

namespace ProfileService.FunctionalTests
{
    public class TestServerFixture : IDisposable
    {
        private readonly GrpcAppFactory _appFactory;

        public TestServerFixture()
        {
            _appFactory = new GrpcAppFactory();
            var client = _appFactory.CreateDefaultClient(new ResponseVersionHandler());

            GrpcChannel = GrpcChannel.ForAddress(client.BaseAddress!, new GrpcChannelOptions
            {
                HttpClient = client
            });
        }

        public GrpcChannel GrpcChannel { get; }

        public void Dispose()
        {
            _appFactory.Dispose();
        }

        private class ResponseVersionHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var response = await base.SendAsync(request, cancellationToken);
                response.Version = request.Version;
                return response;
            }
        }
    }
}