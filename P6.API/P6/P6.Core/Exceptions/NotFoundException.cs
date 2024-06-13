
namespace P6.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; set; }
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
