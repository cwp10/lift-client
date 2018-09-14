namespace EventType
{
    public enum Global
    {
    }

    public enum Lobby
    {

    }
}

public class GlobalEventHandler : EventHandler<EventType.Global> { }
public class LobbyEventHandler : EventHandler<EventType.Lobby> {}
