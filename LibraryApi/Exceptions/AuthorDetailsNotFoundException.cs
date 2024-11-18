namespace LibraryApi.Exceptions
{
    public class AuthorDetailsNotFoundException:Exception
    {
        public AuthorDetailsNotFoundException(string message) : base(message){}
    }
}