namespace Core.Models.Responses
{
    public class FailureResponse
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }

        public FailureResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
