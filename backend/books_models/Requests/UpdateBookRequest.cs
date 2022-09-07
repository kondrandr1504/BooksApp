using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Books.Models
{
  /// <summary>
  ///     Параметры  запроса для обновления полей книги
  /// </summary>
  public class UpdateBookRequest
  {
    /// <summary>
    /// Жанр
    /// </summary>
    [Required]
    [JsonProperty("genre")]
    public string Genre { get; set; }

    /// <summary>
    ///  Год
    /// </summary>
    [Required]
    [JsonProperty("year")]
    public int Year { get; set; }

    /// <summary>
    ///   Автор
    /// </summary>
    [Required]
    [JsonProperty("author")]
    public string Author { get; set; }

  }
}