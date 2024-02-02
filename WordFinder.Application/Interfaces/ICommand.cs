using MediatR;

namespace WordFinder.Application.Interfaces
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
