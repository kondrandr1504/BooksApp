using AutoMapper;
using Books.Data;
using Books.Models;

namespace Books.Service
{
  /// <summary>
  ///     Профиль маппинга для службы Книги
  /// </summary>
  public class BooksMappingProfile : Profile
  {
    /// <summary>
    /// .ctor
    /// </summary>
    public BooksMappingProfile()
    {
      CreateMap<BooksEntity, BooksResp>();
      CreateMap<CreateBookRequest, BooksEntity>();
      CreateMap<UpdateBookRequest, BooksEntity>();
      CreateMap<PagedList<BooksEntity>, PagedList<BooksResp>>();
    }
  }
}
