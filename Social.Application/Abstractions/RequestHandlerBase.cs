using MediatR;
using Social.Application.Enums;
using Social.Application.Models;
using Social.Dal;

namespace Social.Application.Abstractions
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, OperationResult<TResponse>>
        where TRequest : IRequest<OperationResult<TResponse>>
    {
        protected readonly DataContext _dataContext;
        protected readonly OperationResult<TResponse> _operationResult;

        public RequestHandlerBase(DataContext dataContext)
        {
            _dataContext = dataContext;
            _operationResult = new OperationResult<TResponse>();
        }

        public async Task<OperationResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await ExecuteRequestAsync(request);
            }
            catch(SocialAppException e)
            {
                _operationResult.SetError(e.ErrorCode, e.Message);
            }
            catch (Exception e)
            {
                _operationResult.SetError(ErrorCode.Unknown, e.Message);
            }

            return _operationResult;
        }

        protected abstract Task ExecuteRequestAsync(TRequest request);
    }
}
