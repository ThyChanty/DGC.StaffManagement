using FluentValidation;
using MediatR;

using Exceptions_ValidationException = DGC.StaffManagement.Shared.Exceptions.ValidationException;
using ValidationException = DGC.StaffManagement.Shared.Exceptions.ValidationException;


namespace DGC.StaffManagement.Shared.Commons
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var errors = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(x => x.PropertyName)
                .Select(x => new Error()
                {
                    Field = x.Key,
                    ErrorMessages = x.Select(e => new ErrorMessage
                    {
                        EnMessage = e.ErrorMessage,
                        ErrorCode = e.ErrorCode
                    }).ToList()
                }).ToList();

            if (errors.Any())
            {
                throw new Exceptions_ValidationException(errors);
            }

            return next();
        }
    }
}
