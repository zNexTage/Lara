using FluentValidation;
using Lara.Domain.Entities;

namespace Lara.Domain.Contracts;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    TEntity Add<TValidator, TEntityDto>(TEntityDto obj) where TValidator : AbstractValidator<TEntityDto>;

    void Delete(int id);

    IList<TEntity> Get();

    TEntity GetById(int id);

    TEntity Update<TValidator, TEntityDto>(TEntityDto obj) where TValidator : AbstractValidator<TEntityDto>;
}