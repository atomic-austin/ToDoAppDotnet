namespace ToDoAppBackend
{
    public class FileSaverException : Exception
    {
        public FileSaverException() { }
        
        public FileSaverException(string message) : base(message) { }
        
        public FileSaverException(string message, Exception inner) : base(message, inner) { }
    }
}
