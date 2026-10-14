using Bookstore.Domain;

namespace Bookstore.Application;

public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Book>> GetAllAsync()
        => _repository.GetAllAsync();

    public async Task<Book> GetByIdAsync(long id)
    {
        var book = await _repository.GetByIdAsync(id);
        return book ?? throw new BookNotFoundException(id);
    }

    public async Task<Book> CreateAsync(string author, string title)
    {
        var book = new Book(title, author);
        _repository.Add(book);
        await _repository.SaveChangesAsync();
        return book;
    }
    
    public async Task<Book> UpdateAuthor(long id, string author)
    {
	    var book = await GetByIdAsync(id);
	    book.Author = author;
	    await _repository.SaveChangesAsync();
	    return book;
    }
    
    public async Task<Book> UpdateTitle(long id, string title)
    {
	    var book = await GetByIdAsync(id);
	    book.Title = title;
	    await _repository.SaveChangesAsync();
	    return book;
    }

    public async Task<Book> DeleteAsync(long id)
    {
        var book = await GetByIdAsync(id);
        _repository.Remove(book);
        await _repository.SaveChangesAsync();
        return book;
    }
}