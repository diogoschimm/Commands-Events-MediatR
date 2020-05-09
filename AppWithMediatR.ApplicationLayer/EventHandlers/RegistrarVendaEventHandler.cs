using AppWithMediatR.ApplicationLayer.Commands;
using AppWithMediatR.ApplicationLayer.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AppWithMediatR.ApplicationLayer.EventHandlers
{
    public class RegistrarVendaEventHandler :
        INotificationHandler<VendaRealizadaComSucessoEvent>,
        INotificationHandler<VendaRealizadaComErroEvent>
    {

        private readonly IMediator _mediator;

        public RegistrarVendaEventHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public Task Handle(VendaRealizadaComSucessoEvent notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                 
                var registrarPagamento = new RegistrarPagamentoCommand
                {
                    IdVenda = notification.IdVenda,
                    DataPagamento = notification.RegistrarVendaCommand.DataPagamento,
                    ValorPago = notification.RegistrarVendaCommand.ValorPago
                };

                _mediator.Send(registrarPagamento);
            });
        }

        public Task Handle(VendaRealizadaComErroEvent notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => {

                var error = notification.Error;

            });
        }
    }
}
