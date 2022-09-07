using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;

namespace Books.Data
{
  /// <summary>
  ///     Контекст БД службы пользователей
  /// </summary>
  [PublicAPI]
  public sealed class BooksDatabaseContext : DbContext
  {
    /// <summary>
    ///     .ctor
    /// </summary>
    public BooksDatabaseContext(DbContextOptions<BooksDatabaseContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Таблица книг
    /// </summary>
    public DbSet<BooksEntity> Books { get; set; }

    /// <summary>
    /// Настроить параметры контекста БД
    /// </summary>
    public static void Configure(DbContextOptionsBuilder options, string connectionString)
    {
      // NpgsqlDiagnostics.OnConfiguring(connectionString);
      options.UseNpgsql(
          connectionString,
          opts => opts.MigrationsHistoryTable("__schema_migrations")
      );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var mapper = new NpgsqlSnakeCaseNameTranslator();
      foreach (var entity in modelBuilder.Model.GetEntityTypes())
      {
        var storeObjectId = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
        foreach (var property in entity.GetProperties())
        {
          // Проставляем имя поля по умолчанию (snake_case)
          property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(storeObjectId)));
        }
      }

      BooksEntity.Setup(modelBuilder.Entity<BooksEntity>());
    }
  }
}
