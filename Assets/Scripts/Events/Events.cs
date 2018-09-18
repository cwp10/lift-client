namespace EventType
{
    public enum Global
    {
    }

    public enum Lobby
    {
        MOVE_ELEVATOR,
        BEGIN_ELEVATOR,
        END_ELEVATOR,
    }
}

public class GlobalEventHandler : EventHandler<EventType.Global> { }
public class LobbyEventHandler : EventHandler<EventType.Lobby> {}
