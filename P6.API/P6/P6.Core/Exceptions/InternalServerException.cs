
namespace P6.Core.Exceptions
{
    public class InternalServerException : Exception
    {
        public int ErrorCode { get; set; }
        public InternalServerException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public InternalServerException(string message) : base(message) { }
        public InternalServerException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
