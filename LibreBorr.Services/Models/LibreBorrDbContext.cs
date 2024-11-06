namespace LibreBorr.Services.Models;

using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;

public class LibreBorrDbContext : DbContext
{
    public LibreBorrDbContext(DbContextOptions<LibreBorrDbContext> dbContextOptions) : base(dbContextOptions) { }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book?> Books { get; set; }
    public DbSet<BookImage> BookImages { get; set; }
    public DbSet<Friend?> Friends { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(b => {
            b.HasMany(b => b.Tags).WithMany(t => t.Books);
            b.HasMany(b => b.Authors).WithMany(a => a.Books);
            b.HasMany(b => b.Genres).WithMany(g => g.Books);
            b.HasOne(b => b.Image).WithOne(i => i.Book);
        });

        modelBuilder.Entity<BookImage>(bi => {
            bi.HasOne(bi => bi.Book).WithOne(b => b.Image);
        });
        // modelBuilder.Entity<Book>(b =>
        // {
        //     b.HasMany<Author>().WithMany(a => a.Books);
        //     b.HasMany<Genre>().WithMany(g => g.Books);
        //     b.HasMany<Tag>().WithMany(t => t.Books);
        //     b.HasOne<BookImage>().WithOne(bi => bi.Book);
        //     b.HasOne<Friend>().WithMany(f => f.Books);
        //     b.HasMany<Tag>().WithMany(t => t.Books);
        // });

        // modelBuilder.Entity<Author>(a => a.HasMany<Book>().WithMany(b => b.Authors));
        // modelBuilder.Entity<BookImage>(bi => bi.HasOne<Book>().WithOne(bi => bi.Image));
        // modelBuilder.Entity<Friend>(f => f.HasMany<Book>().WithOne(b => b.Friend));
        // modelBuilder.Entity<Genre>(g => g.HasMany<Book>().WithMany(b => b.Genres));
        // modelBuilder.Entity<Tag>(t => t.HasMany<Book>().WithMany(b => b.Tags));

        base.OnModelCreating(modelBuilder);
    }
}

