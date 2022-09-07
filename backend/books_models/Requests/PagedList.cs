using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Books.Models
{
  /// <summary>
  ///     Список с пагинацией
  /// </summary>
  [PublicAPI]
  public class PagedList<T>
  {
    /// <summary>
    ///     Информация о пагинации
    /// </summary>
    [
        JsonProperty("pager", NullValueHandling = NullValueHandling.Ignore, Order = 1),
        CanBeNull
    ]
    public PagerInfo Pager { get; set; }

    /// <summary>
    ///     Общее количество элементов
    /// </summary>
    [JsonProperty("total_count", Order = 2)]
    public long TotalCount { get; set; }

    /// <summary>
    ///     Элементы на странице
    /// </summary>
    [JsonProperty("items", Order = 3)]
    public T[] Items { get; set; }
  }
}
