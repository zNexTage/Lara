using FluentValidation;
using Lara.Domain.DataTransferObjects;

namespace Lara.Service.Validators;

public class BorrowedValidator : AbstractValidator<CreateBorrowedDto>
{
    public BorrowedValidator()
    {
        RuleFor(dto => dto.Quantity)
            .Equal(1)
            .WithMessage("Só é possível pegar emprestado um livro com o mesmo título uma vez.");
        
        RuleFor(dto => dto.BookId)
            .GreaterThan(0)
            .WithMessage("Livro inválido");
    }
}