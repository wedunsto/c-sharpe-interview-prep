using Bookstore.Domain;

namespace Bookstore.Application;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(long id);
    void Add(Book book);
    void Remove(Book book);
    Task SaveChangesAsync();
}