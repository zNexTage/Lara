using FluentValidation;
using Lara.Domain.DataTransferObjects;

namespace Lara.Service.Validators;

public class BorrowedValidator : AbstractValidator<CreateBorrowedDto>
{
    public BorrowedValidator()
    {
        RuleFor(dto => dto.BookId)
            .GreaterThan(0)
            .WithMessage("Livro inválido");
    }
}