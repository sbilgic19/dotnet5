using AutoMapper;
using WebApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Entities;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                                  .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FullName));



            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FullName));



            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();


            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();


            CreateMap<CreateUserModel, User>();
        }

    }

}