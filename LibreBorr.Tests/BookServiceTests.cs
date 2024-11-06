using LibreBorr.Services;
using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace LibreBorr.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task GetBooksAsync_ShouldReturnAllBooks()
    {
        var options = new DbContextOptionsBuilder<LibreBorrDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        // Insert seed data into the database using an instance of the context
        using (var context = new LibreBorrDbContext(options))
        {
            context.Books.Add(new Book { Id = 1, Title = "Title 1" });
            context.Books.Add(new Book { Id = 2, Title = "Title 2" });
            context.Books.Add(new Book { Id = 3, Title = "Title 3" });
            context.SaveChanges();
        }

        // Mock IDbContextFactory to return new DbContext with in-memory database
        var mockFactory = new Mock<IDbContextFactory<LibreBorrDbContext>>();
        mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new LibreBorrDbContext(options));

        // Create instance of a service with the mock factory
        var bookService = new BookService(mockFactory.Object);

        // Pozovi metodu i proveri očekivane rezultate
        var result = await bookService.GetBooks();

        // ASSERT

        Assert.Equal(3, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal(2, result[1].Id);
        Assert.Equal(3, result[2].Id);
    }

    [Fact]
    public async Task CreateBook_ShouldAddBookAndImageAndCommitTransaction()
    {
        // In-memory database options
        var options = new DbContextOptionsBuilder<LibreBorrDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_CreateBook")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        // Mock for IDbContextFactory that returns new instance of DbContext-a with in-memory db
        var mockFactory = new Mock<IDbContextFactory<LibreBorrDbContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new LibreBorrDbContext(options)));

        // Create mock BookService
        var bookService = new BookService(mockFactory.Object);

        // Create test Book
        var newBook = new Book
        {
            Title = "New Book",
            Image = new BookImage { Image = new byte[] { 1, 2, 3 } } // Image example
        };

        // Call CreateBook
        var createdBook = await bookService.CreateBook(newBook);

        // Create context again
        await using var context = new LibreBorrDbContext(options);

        // Check if book is saved in db
        var bookInDb = await context.Books.Include(b => b.Image).FirstOrDefaultAsync(b => b.Id == createdBook.Id);
        Assert.NotNull(bookInDb);
        Assert.Equal("New Book", bookInDb.Title);

        // Check if book image is saved and connected to a book
        var bookImageInDb = await context.BookImages.FirstOrDefaultAsync(bi => bi.BookId == createdBook.Id);
        Assert.NotNull(bookImageInDb);
        Assert.Equal(new byte[] { 1, 2, 3 }, bookImageInDb.Image);

        // Check if `BookImageId` is updated in Book
        Assert.Equal(bookImageInDb.BookImageId, createdBook.BookImageId);
    }

    [Fact]
    public async Task DeleteBook_ShouldDeleteBook()
    {
        var options = new DbContextOptionsBuilder<LibreBorrDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_DeleteBook")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var id = 0;
        // Insert seed data into the database using an instance of the context
        await using (var context = new LibreBorrDbContext(options))
        {
            context.Books.Add(new Book { Id = 1, Title = "Title 1" });
            context.Books.Add(new Book { Id = 2, Title = "Title 2" });
            context.Books.Add(new Book { Id = 3, Title = "Title 3" });
            await context.SaveChangesAsync();

            // Mock IDbContextFactory to return new DbContext with in-memory database
            var mockFactory = new Mock<IDbContextFactory<LibreBorrDbContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new LibreBorrDbContext(options));

            // Create instance of a service with the mock factory
            var bookService = new BookService(mockFactory.Object);

            var bookToDelete = new Book { Id = 1, Title = "Title 1" };
            id = await bookService.DeleteBook(bookToDelete);
        }

        Assert.Equal(1, id);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdateBook()
    {
        var options = new DbContextOptionsBuilder<LibreBorrDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_UpdateBook")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        // Insert seed data into the database using an instance of the context
        await using (var context = new LibreBorrDbContext(options))
        {
            context.Books.Add(new Book { Id = 1, Title = "Title 1" });
            context.Books.Add(new Book { Id = 2, Title = "Title 2" });
            context.Books.Add(new Book { Id = 3, Title = "Title 3" });
            await context.SaveChangesAsync();
        }

        // Mock IDbContextFactory to return a new DbContext with in-memory database
        var mockFactory = new Mock<IDbContextFactory<LibreBorrDbContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LibreBorrDbContext(options)); // Return correct DbContext

        // Create instance of a service with the mock factory
        var bookService = new BookService(mockFactory.Object);

        // Use the same context for the update operation
        await using (var context = new LibreBorrDbContext(options))
        {
            var bookToUpdate = new Book { Id = 1, Title = "Title 1_1" };
            var result = await bookService.UpdateBook(bookToUpdate);

            Assert.NotNull(result);
            Assert.Equal("Title 1_1", result.Title);
        
            // Verify if the changes are reflected in the context
            var updatedBook = await context.Books.FindAsync(1);
            Assert.NotNull(updatedBook);
            Assert.Equal("Title 1_1", updatedBook.Title);
        }
    }
}