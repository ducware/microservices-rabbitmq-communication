using MediatR;

namespace MSE.User.Domain.Common.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
