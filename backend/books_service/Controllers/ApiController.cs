using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Books.Service
{
  [ApiController]
  [Route("[controller]")]
  public class ApiController : ControllerBase
  {
    private readonly BooksDatabaseContext _db;
    private readonly IMapper _mapper;
    private readonly IBooksService _booksService;
    private readonly ILogger<ApiController> _logger;

    /// <summary>
    ///     .ctor
    /// </summary>
    public ApiController(
        BooksDatabaseContext db,
        IMapper mapper,
        IBooksService booksService,
        ILogger<ApiController> logger)
    {
      _mapper = mapper;
      _db = db;
      _booksService = booksService;
      _logger = logger;
    }

    #region GET /books/

    /// <summary>
    /// Получение списка книг
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpGet("~/books/")]
    [ProducesResponseType(typeof(BooksResp[]), 200)]
    [ProducesResponseType(400)]
    public IActionResult GetBooks([FromQuery] ListBooksRequest query, CancellationToken cancellationToken)
    {
      var books = _booksService.GetBooks(query, cancellationToken);
      var result = _mapper.Map<PagedList<BooksEntity>, PagedList<BooksResp>>(books);
      return Ok(result);
    }

    #endregion

    #region GET /books/{id}
    /// <summary>
    /// Получение книги по id
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpGet("~/books/{id}")]
    [ProducesResponseType(typeof(BooksResp), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetBooksById([FromRoute(Name = "id")] long bookId, CancellationToken cancellationToken)
    {
      var bookById = await _booksService.GetBookByIdAsync(bookId, cancellationToken);
      var result = _mapper.Map<BooksEntity, BooksResp>(bookById);
      return Ok(result);
    }

    #endregion

    #region POST /books/

    /// <summary>
    /// Создание книги
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpPost("~/books")]
    [ProducesResponseType(typeof(BooksResp), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostBooks([FromBody] CreateBookRequest query, CancellationToken cancellationToken)
    {
      var books = await _booksService.PostBookAsync(query, cancellationToken);
      var result = _mapper.Map<BooksResp>(books);

      return Ok(result);
    }

    #endregion

    #region PUT /books/{id}

    /// <summary>
    /// Изменение книги
    /// </summary>
    /// <param name="name"></param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpPut("~/books/{name}")]
    [ProducesResponseType(typeof(BooksResp), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutBooks([FromRoute(Name = "name")] string name, [FromBody] UpdateBookRequest query, CancellationToken cancellationToken)
    {
      var (book, error) = await _booksService.PutBookAsync(name, query, cancellationToken);
      if (error != null)
      {
        return Conflict(error.Message);
      }
      var result = _mapper.Map<BooksResp>(book);
      return Ok(result);
    }

    #endregion

    #region DELETE /books/{name}

    /// <summary>
    ///     Удалить книгу
    /// </summary>
    /// <param name="name">Название книги</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpDelete("~/books/{name}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> BookDelete([FromRoute(Name = "name")] string name, CancellationToken cancellationToken)
    {
      var (result, error) = await _booksService.DeleteBookAsync(name, cancellationToken);
      if (error != null)
      {
        return Conflict(error.Message);
      }
      return Ok(result);
    }

    #endregion
  }
}
