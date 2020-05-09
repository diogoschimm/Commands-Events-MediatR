using MediatR;

namespace AppWithMediatR.ApplicationLayer.Commands
{
    public abstract class Command<TResult> : IRequest<TResult>
    {
    }
}
