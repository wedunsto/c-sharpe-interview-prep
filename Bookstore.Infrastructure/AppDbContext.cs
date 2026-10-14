using Microsoft.EntityFrameworkCore;
using Bookstore.Domain;

namespace Bookstore.Infrastructure;

public class AppDbContext : DbContext
{
	public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }
	
	public DbSet<Book> Books => Set<Book>();
}