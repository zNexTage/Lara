using AutoMapper;
using FluentValidation;
using Lara.Domain.Contracts;
using Lara.Domain.Entities;

namespace Lara.Service.Service;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper; 

    public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public virtual TEntity Add<TValidator, TEntityDto>(TEntityDto obj) where TValidator : AbstractValidator<TEntityDto>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());

        var entity = _mapper.Map<TEntity>(obj);
        
        _repository.Insert(entity);
        
        return entity;
    }
    

    public virtual void Delete(int id)
    {
        _repository.Delete(id);
    }

    public virtual IList<TEntity> Get()
    {
        return _repository.Select();
    }

    public virtual TEntity GetById(int id)
    {
        return _repository.Select(id);
    }

    public virtual TEntity Update<TValidator, TEntityDto>(int id, TEntityDto obj) where TValidator : AbstractValidator<TEntityDto>
    {
        Validate(obj, Activator.CreateInstance<TValidator>());
        var entity = _mapper.Map<TEntity>(obj);
        entity.Id = id;
        
        _repository.Update(entity);

        return entity;
    }

    public TEntity Update(TEntity obj)
    {
        _repository.Update(obj);

        return obj;
    }

    protected void Validate<TEntityDto>(TEntityDto obj, AbstractValidator<TEntityDto> validator)
    {
        if (obj == null)
            throw new Exception("Registros não detectados!");

        validator.ValidateAndThrow(obj);
    }
}