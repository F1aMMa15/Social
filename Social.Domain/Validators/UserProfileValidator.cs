using FluentValidation;
using FluentValidation.Results;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Domain.Validators
{
    internal class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public UserProfileValidator()
        {

        }
    }
}
