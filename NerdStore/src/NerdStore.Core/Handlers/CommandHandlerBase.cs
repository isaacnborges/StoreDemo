using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Handlers
{
    public abstract class CommandHandlerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        protected CommandHandlerBase(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidarComando(Command request)
        {
            if (request.EhValido())
                return true;

            foreach (var error in request.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
