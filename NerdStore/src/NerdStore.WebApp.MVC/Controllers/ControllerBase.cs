using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid ClienteId = Guid.Parse("96C13CE1-1094-45AE-9895-E7B40EFA53F1");

        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        public ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida() => !_notifications.ExisteNotificacao();

        protected IEnumerable<string> ObterMensagensErro() => _notifications.ObterNotificacoes().Select(c => c.Value).ToList();

        protected void NotificarErro(string codigo, string mensagem) => _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}
