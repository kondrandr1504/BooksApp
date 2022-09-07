using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Books.Data;
using Books.Models;

namespace Books.Service
{
  /// <summary>
  ///     Сервис книги
  /// </summary>
  public class BooksService : IBooksService
  {
    private readonly BooksDatabaseContext _db;
    private readonly ILogger<BooksService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    ///     .ctor
    /// </summary>
    public BooksService(
      BooksDatabaseContext db,
      ILogger<BooksService> logger,
      IMapper mapper
    )
    {
      _db = db;
      _logger = logger;
      _mapper = mapper;
    }

    #region public Methods

    /// <summary>
    /// Получение списка книг
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public PagedList<BooksEntity> GetBooks(ListBooksRequest query, CancellationToken cancellationToken)
    {
      var books = _db.Books.AsQueryable();

      var skip = query.Skip ?? 0;
      var max = query.Max ?? books.Count();
      var pagedResult = books.Skip(skip).Take(max);

      var result = new PagedList<BooksEntity>
      {
        Items = pagedResult.ToArray(),
        TotalCount = books.Count(),
        Pager = new PagerInfo
        {
          Skip = skip,
          Max = max
        }
      };

      return result;
    }

    /// <summary>
    ///      Получить книгу по id
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <param name="cancellationToken">ct</param>
    /// <returns></returns>
    public async Task<BooksEntity> GetBookByIdAsync(long bookId, CancellationToken cancellationToken)
    {
      var books = await _db.Books.FirstOrDefaultAsync(_ => _.Id == bookId, cancellationToken);
      return books;
    }

    /// <summary>
    /// Создание книги
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<BooksEntity> PostBookAsync(CreateBookRequest query, CancellationToken cancellationToken)
    {
      try
      {
        var newBook = _mapper.Map<BooksEntity>(query);

        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);

        var book = await _db.Books.AddAsync(newBook, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await t.CommitAsync(cancellationToken);
        return book.Entity;
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"PostBookAsync ended with error for {JsonConvert.SerializeObject(query)}");
        throw new Exception("", e);
      }
    }

    /// <summary>
    /// Изменение книгу
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<ErrorTuple<BooksEntity>> PutBookAsync(string Name, UpdateBookRequest query, CancellationToken cancellationToken)
    {
      try
      {
        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);

        var bookExist = await _db.Books.FirstOrDefaultAsync(e => e.Name == Name, cancellationToken);
        if (bookExist == null)
        {
          return new Error { Message = "Книги с таким названием не существует" };
        }

        var newBook = _mapper.Map<BooksEntity>(query);

        bookExist.Name = newBook.Name ?? Name;
        bookExist.Genre = newBook.Genre ?? "";
        bookExist.Author = newBook.Author ?? "";
        bookExist.Year = newBook.Year;

        var result = _db.Update(bookExist);
        await _db.SaveChangesAsync(cancellationToken);

        await t.CommitAsync(cancellationToken);
        return result.Entity;
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"PutBookAsync ended with error for {JsonConvert.SerializeObject(query)}");
        throw new Exception("", e);
      }
    }

    /// <summary>
    ///     Удалить книгу
    /// </summary>
    /// <param name="name">название книги</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<ErrorTuple<bool>> DeleteBookAsync(string name, CancellationToken cancellationToken)
    {
      try
      {
        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);
        var bookForDelete = await _db.Books.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        if (bookForDelete == null)
        {
          return new Error { Message = "Книги с таким именем не существует" };
        }
        _db.Books.Remove(bookForDelete);
        await _db.SaveChangesAsync(cancellationToken);
        await t.CommitAsync(cancellationToken);
      }
      catch (Exception e)
      {
        _logger.LogInformation("DeleteEmployeeAsync ended with error");
        throw new Exception("DeleteEmployeeAsync ended with error", e);
      }

      return true;
    }

    #endregion
  }
}