using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace NerdStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarEvento<T>(T evento) where T : Event;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
