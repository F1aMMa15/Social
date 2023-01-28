using FluentValidation;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Domain.Validators
{
    internal class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(post => post.TextContent)
                .NotEmpty().WithMessage("Text Content is required. It is currently empty")
                .MinimumLength(3).WithMessage("Text Content must be at least 3 characters long")
                .MaximumLength(500).WithMessage("Text Content can contain at most 500 characters long");
        }
    }
}
