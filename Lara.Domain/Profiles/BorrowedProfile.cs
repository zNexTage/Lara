using AutoMapper;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;

namespace Lara.Domain.Profiles;

public class BorrowedProfile : Profile
{
    public BorrowedProfile()
    {
        CreateMap<CreateBorrowedDto, BorrowedBook>();
        CreateMap<BorrowedBook, ReadBorrowedDto>();
    }
}