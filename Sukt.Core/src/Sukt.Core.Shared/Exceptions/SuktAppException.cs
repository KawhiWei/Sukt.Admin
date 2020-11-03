namespace Sukt.Core.Shared.Exceptions
{
    public class SuktAppException : System.Exception
    {
        public SuktAppException()
        {
        }

        public SuktAppException(string message) : base(message)
        {
        }

        public SuktAppException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}