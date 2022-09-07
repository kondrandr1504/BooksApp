using Newtonsoft.Json;

namespace Books.Models
{
  /// <summary>
  /// Книги
  /// </summary>
  public sealed class BooksResp
  {
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Жанр
    /// </summary>
    [JsonProperty("genre")]
    public string Genre { get; set; }

    /// <summary>
    /// Год
    /// </summary>
    [JsonProperty("year")]
    public int Year { get; set; }

    /// <summary>
    /// Автор
    /// </summary>
    [JsonProperty("author")]
    public string Author { get; set; }

  }
}
