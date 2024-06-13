    
namespace P6.Core.Exceptions
{   
    public class ConflictException : Exception
    {
        public int ErrorCode { get; set; }
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
