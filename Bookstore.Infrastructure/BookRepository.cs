using Microsoft.EntityFrameworkCore;
using Bookstore.Application;
using Bookstore.Domain;

namespace Bookstore.Infrastructure;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Book>> GetAllAsync()
        => _context.Books.AsNoTracking().ToListAsync();

    public Task<Book?> GetByIdAsync(long id)
        => _context.Books.FirstOrDefaultAsync(t => t.Id == id);

    public void Add(Book book)
        => _context.Books.Add(book);
        
    public void Remove(Book book) => _context.Books.Remove(book);

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}