using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Books.Models
{
  /// <summary>
  ///     Параметры  запроса для создания книги
  /// </summary>
  public class CreateBookRequest
  {
    /// <summary>
    /// Название книги
    /// </summary>
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

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