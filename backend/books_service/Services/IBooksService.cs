using System.Threading;
using System.Threading.Tasks;
using Books.Data;
using Books.Models;

namespace Books.Service
{
  /// <summary>
  ///     Сервис книги
  /// </summary>
  public interface IBooksService
  {
    /// <summary>
    /// Получение списка книг
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public PagedList<BooksEntity> GetBooks(ListBooksRequest query, CancellationToken cancellationToken);

    /// <summary>
    /// Получить книгу
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <param name="cancellationToken">ct</param>
    /// <returns></returns>
    public Task<BooksEntity> GetBookByIdAsync(long bookId, CancellationToken cancellationToken);

    /// <summary>
    /// Создание книги
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<BooksEntity> PostBookAsync(CreateBookRequest query, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение книгу
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<ErrorTuple<BooksEntity>> PutBookAsync(string Name, UpdateBookRequest query, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить книгу
    /// </summary>
    /// <param name="name">название книги</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<ErrorTuple<bool>> DeleteBookAsync(string name, CancellationToken cancellationToken);
  }
}