namespace ProfileService.GRPC.Interceptors.Helpers
{
    public class StatusMessage
    {
        public bool IsSuccessful { get; set; }
        public string? Reason { get; set; }
        public IEnumerable<GrpcExceptionDetail> Details { get; set; }
    }
}