using LibreBorr.Services.Interfaces;
using LibreBorr.Services.Interfaces;
using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreBorr.Services;

public class BookService : IBookService
{
    private readonly IDbContextFactory<LibreBorrDbContext> _dbContextFactory;

    public BookService(IDbContextFactory<LibreBorrDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Book>> GetBooks()
    {
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Books
            .Include(b => b.Tags)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Image) //.ThenInclude(b => b.Image)
            .ToListAsync();
    }

    public async Task<Book> CreateBook(Book? book)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();

        var bookImage = new BookImage
        {
            BookId = book.Id,
            Image = book.Image?.Image!,
        };

        context.BookImages.Add(bookImage);
        await context.SaveChangesAsync();

        book.BookImageId = bookImage.BookImageId;
        context.Update(book);

        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        return book;
    }

    public async Task<Book?> UpdateBook(Book bookRequest)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var bookToUpdate = context.Books.Include(book => book.Tags)!.ThenInclude(tag => tag.Books)
            .FirstOrDefault(b => b.Id == bookRequest.Id);
        if (bookToUpdate == null) return null;

        var tags = context.Tags.Where(tag => tag.Books.Any(b => b.Id == bookRequest.Id)).ToList();
        tags.ForEach(t => t.Books.Remove(bookRequest));

        var bookTags = new List<Book>();
        bookToUpdate.Tags?.ForEach(tag =>
        {
            tag.Books.ForEach(book =>
            {
                if (book.Id == bookRequest.Id)
                {
                    bookTags.Add(book);
                }
            });
        });

        bookTags.ForEach(bt => { bookToUpdate.Tags?.ForEach(tag => tag.Books.Remove(bt)); });


        bookToUpdate.Title = bookRequest.Title;
        bookToUpdate.Description = bookRequest.Description;
        bookToUpdate.Tags = bookRequest.Tags;

        context.Update(bookToUpdate);

        await context.SaveChangesAsync();
        
        return bookToUpdate;
    }

    public async Task<Book?> GetBook(int id)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        return await context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<int> DeleteBook(Book book)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();

        var imageToRemove = context.BookImages.FirstOrDefault(image => image.BookId == book.Id);
        if (imageToRemove != null)
        {
            context.BookImages.Remove(imageToRemove);
        }

        context.Books.Remove(book);


        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return book.Id;
    }

    public async Task AddToWishlist(Book book)
    {
        await using var context = _dbContextFactory.CreateDbContext();

        await context.WishlistItems.AddAsync(new WishlistItem { Book = book });
        await context.SaveChangesAsync();
    }

    public async Task<List<Book>> GetWishlist()
    {
        await using var context = _dbContextFactory.CreateDbContext();

        var books = context.Books.Join(
            context.WishlistItems,
            book => book.Id, wishlistItem => wishlistItem.Id,
            (book, wishlistItem) => book
        );

        return books.Any() ? await books.ToListAsync() : Enumerable.Empty<Book>().ToList();
    }
}