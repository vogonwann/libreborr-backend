using AutoMapper;
using LibreBorr.BL.GraphQl.Inputs;
using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Friend = LibreBorr.BL.Models.Friend;

namespace LibreBorr.BL.Mappers;

public class LibreBorrAutoMapperProfile: Profile
{
    public LibreBorrAutoMapperProfile() 
    {
        CreateMap<Tag, Models.Tag>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name));

        CreateMap<Services.Models.Book, Models.Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.Image))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.FullName).ToArray())))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => string.Join('/', src.Genres.Select(g => g.Name).ToArray())))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn));

        CreateMap<Models.Tag, Tag>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name));

        CreateMap<Models.Book, Services.Models.Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForPath(dest => dest.Image.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Authors, opt => 
                opt.MapFrom(src => src.Authors
                        .Split(',', StringSplitOptions.TrimEntries)
                        .Select(a => new Author {FullName = a})
                        .ToList()))
            .ForMember(dest => dest.Genres, opt => 
                opt.MapFrom(src => src.Genres
                        .Split('/', StringSplitOptions.TrimEntries)
                        .Select(g => new Genre { Name = g })
                        .ToList()))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));

        CreateMap<BookInput, Models.Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Split(',', StringSplitOptions.TrimEntries).Select(t => new Models.Tag { Name = t})));

        CreateMap<Friend, Services.Models.Friend>();
        CreateMap<Services.Models.Friend, Friend>();
        CreateMap<FriendInput, Friend>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
    }
}