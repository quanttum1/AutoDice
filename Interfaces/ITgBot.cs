namespace AutoDice.Interfaces;

public interface ITgBot
{
    public bool IsRunning { get; }
    public bool TryStart();
    public string Username { get; }
}
