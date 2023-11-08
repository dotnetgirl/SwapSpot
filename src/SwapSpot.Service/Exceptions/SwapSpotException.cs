namespace SwapSpot.Service.Exceptions;

public class SwapSpotException : Exception
{
    public int StatusCode { get; set; }

    public SwapSpotException(int code, string message) : base(message)
    {
        StatusCode = code;
    }
}
