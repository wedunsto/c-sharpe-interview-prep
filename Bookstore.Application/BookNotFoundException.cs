namespace Bookstore.Application;

public class BookNotFoundException : Exception
{
	public BookNotFoundException(long id)
		: base($"Book not found: {id}") { }
}