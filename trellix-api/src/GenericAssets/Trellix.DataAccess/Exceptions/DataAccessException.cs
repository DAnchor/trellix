namespace Trellix.DataAccess.Exceptions;

public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception ex) : base(message, ex) {}

    public DataAccessException(string message) : base(message) {}
}