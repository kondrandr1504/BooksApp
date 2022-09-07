using System;

namespace Books.Service
{
  /// <summary>
  ///     Класс исключения в случае отсутствия книги в бд
  /// </summary>
  public class BooksNotFoundException : ApplicationException
  {
    public BooksNotFoundException() { }

    public BooksNotFoundException(string message) : base(message) { }

    public BooksNotFoundException(string message, Exception inner) : base(message, inner) { }
  }
}
