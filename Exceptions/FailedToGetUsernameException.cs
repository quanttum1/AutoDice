namespace AutoDice.Exceptions;

public class FailedToGetUsernameException : System.Exception
{
    public FailedToGetUsernameException() { }
    public FailedToGetUsernameException(string message) : base(message) { }
    public FailedToGetUsernameException(string message, System.Exception inner) : base(message, inner) { }
}
