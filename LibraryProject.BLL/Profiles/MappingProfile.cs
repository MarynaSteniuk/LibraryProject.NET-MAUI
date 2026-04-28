using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using LibraryProject.DAL.Entities;
using LibraryProject.BLL.DTOs;

namespace LibraryProject.BLL.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();

        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
    }
}
