using FluentValidation;
using WebApi.Application.AuthorOperations.UpdateAuthor;
using System;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(4).When(x => x.Model.Surname.Trim() != string.Empty);
            RuleFor(command => command.Model.Birthdate).LessThan(DateTime.Now.Date);
        }
    }
    
}