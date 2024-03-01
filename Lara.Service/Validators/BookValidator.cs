using FluentValidation;
using Lara.Domain.DataTransferObjects;

namespace Lara.Service.Validators;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Informe o título do livro")
            .NotNull().WithMessage("Informe o título do livro")
            .MaximumLength(100).WithMessage("O título deverá ter no máximo 100 caracteres");
        
        RuleFor(book => book.Publisher)
            .NotEmpty().WithMessage("Informe a editora")
            .NotNull().WithMessage("Informe a editora")
            .MaximumLength(80).WithMessage("A editora deverá ter no máximo 80 caracteres");

        RuleFor(book => book.Image)
            .NotEmpty().WithMessage("Informe a foto do livro")
            .NotNull().WithMessage("Informe a foto do livro");

        RuleForEach(book => book.Authors)
            .NotEmpty().WithMessage("Informe o autor do livro")
            .NotNull().WithMessage("Informe o autor do livro");
        
        RuleFor(book => book.Authors)
            .NotEmpty().WithMessage("Informe o(s) autor(es) do livro")
            .NotNull().WithMessage("Informe o(s) autor(es) do livro");
        
        RuleForEach(book => book.Authors)
            .ChildRules(val => 
            val.RuleFor(author => author)
                .NotNull()
                .WithMessage("Informe o autor")
                .NotEmpty()
                .WithMessage("Informe o autor")
                .MaximumLength(50)
                .WithMessage("O nome do autor deve ter no máximo 50 caracteres")
            );
    }
}