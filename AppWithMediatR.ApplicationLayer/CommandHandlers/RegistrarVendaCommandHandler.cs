using AppWithMediatR.ApplicationLayer.Commands;
using AppWithMediatR.ApplicationLayer.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppWithMediatR.ApplicationLayer.CommandHandlers
{
    public class RegistrarVendaCommandHandler : IRequestHandler<RegistrarVendaCommand, bool>
    {
        private readonly IMediator _mediator;

        public RegistrarVendaCommandHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public Task<bool> Handle(RegistrarVendaCommand request, CancellationToken cancellationToken)
        {
            if (VendaSalva())
            {
                var idVenda = new Random().Next(1000000);
                PublicarEventoSucesso(idVenda, request);
            }
            else
            {
                PublicarEventoErro("Banco de dados indisponível");
            }

            return Task.FromResult(true);
        }

        private void PublicarEventoSucesso(int idVenda, RegistrarVendaCommand command)
        {
            var vendaComSucesso = new VendaRealizadaComSucessoEvent
            {
                IdVenda = idVenda,
                RegistrarVendaCommand = command 
            };
            _mediator.Publish(vendaComSucesso);
        }

        public void PublicarEventoErro(string erro)
        {
            var vendaComErro = new VendaRealizadaComErroEvent
            {
                Error = erro
            }; 
            _mediator.Publish(vendaComErro);
        }

        private bool VendaSalva()
        {
            return new Random().Next(2) == 1;
        }
    }
}
