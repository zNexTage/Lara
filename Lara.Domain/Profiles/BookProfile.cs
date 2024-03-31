using AutoMapper;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;

namespace Lara.Domain.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookDto, Book>();
        CreateMap<Book, BookDto>();
    }
}