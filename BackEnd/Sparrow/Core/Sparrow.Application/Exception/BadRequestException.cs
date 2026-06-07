namespace Sparrow.Application.Exception
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message) { }

    }
}
