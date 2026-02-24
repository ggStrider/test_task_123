namespace Internal.Scripts.Installers.Signals
{
    /// <summary>
    /// Signal which firing in <see cref="Internal.Scripts.Gameplay.Observers.PlayerFallChecker"/>
    /// via zenject SignalBus, when player is lower than the bottom of the screen
    /// </summary>
    public struct PlayerLoseSignal
    {
    }
}