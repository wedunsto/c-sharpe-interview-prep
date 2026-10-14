namespace Bookstore.Domain;

public class Book
{
    public long Id { get; private set; }
    public string Author { get; set; }
    public string Title { get; set; }

    private Book() {}

    public Book(string author, string title)
    {
        Author = author;
        Title = title;
    }
}