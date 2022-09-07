using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Data
{
  /// <summary>
  /// Книги
  /// </summary>
  [PublicAPI]
  [Table("books")]
  public class BooksEntity
  {
    /// <summary>
    ///     ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Жанр
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    ///  Год
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    ///   Автор
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Настройка
    /// </summary>
    /// <param name="builder"></param>
    internal static void Setup(EntityTypeBuilder<BooksEntity> builder)
    {
      // колонки обязательные
      builder.Property(_ => _.Id).IsRequired();
      builder.Property(_ => _.Name).IsRequired();
      builder.Property(_ => _.Genre).IsRequired();
      builder.Property(_ => _.Year).IsRequired();
      builder.Property(_ => _.Author).IsRequired();
      builder.HasIndex(_ => _.Id).IsUnique();
    }
  }
}
