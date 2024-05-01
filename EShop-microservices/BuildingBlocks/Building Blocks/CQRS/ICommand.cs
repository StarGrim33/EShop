using MediatR;

namespace Building_Blocks.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }

    public interface ICommand : ICommand<Unit>
    {

    }
}
