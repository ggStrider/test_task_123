using Internal.Scripts.Installers.Signals;
using Zenject;

namespace Internal.Scripts.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerLoseSignal>()
                .OptionalSubscriber();
        }
    }
}