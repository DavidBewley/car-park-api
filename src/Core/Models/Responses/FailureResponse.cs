namespace Core.Models.Responses
{
    public class FailureResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public FailureResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
