
namespace P6.Core.Exceptions
{
    public class BadRequestException: Exception
    {
        public int ErrorCode { get; set; }
        public BadRequestException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
