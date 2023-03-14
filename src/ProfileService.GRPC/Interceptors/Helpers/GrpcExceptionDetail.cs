namespace ProfileService.GRPC.Interceptors.Helpers
{
    public class GrpcExceptionDetail
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public GrpcExceptionDetail(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}