using AppWithMediatR.ApplicationLayer.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AppWithMediatR.ApplicationLayer.CommandHandlers
{
    public class RegistrarPagamentoCommandHandler : IRequestHandler<RegistrarPagamentoCommand, bool>
    {
        public Task<bool> Handle(RegistrarPagamentoCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
