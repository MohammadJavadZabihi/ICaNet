namespace ICaNet.ApplicationCore.Exceptions
{
    public class CustomeException : Exception
    {
        public CustomeException() { }

        public CustomeException(string errorMessage) : base(errorMessage) { }

        public CustomeException(string errorMessage, Exception innerException) : 
            base(errorMessage, innerException) { }
    }
}
