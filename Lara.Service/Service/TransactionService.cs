using AutoMapper;
using Lara.Domain.Contracts;
using Lara.Domain.Entities;

namespace Lara.Service.Service;

public class TransactionService : BaseService<BaseTransaction>
{
    public TransactionService(IBaseRepository<BaseTransaction> repository, IMapper mapper) : base(repository, mapper)
    {
    }

}