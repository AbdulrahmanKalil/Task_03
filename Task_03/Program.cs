
namespace T_03

{

    public class Book
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public Book(string title, string author, string iSBN, bool isAvailable)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            ISBN = iSBN;
            IsAvailable = isAvailable;
        }

     
    }

    public class Library
    {
        private List<Book> _books;

        public Library()
        {
            _books = new List<Book>();
        }
       
        public void AddBook(Book book)
        {
            _books.Add(book);
            Console.WriteLine($"Added book: {book.Title} by {book.Author}");
        }

        public void SearchBook(string query)
        {
            var results = _books.FindAll(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || book.Author.Contains(query, StringComparison.OrdinalIgnoreCase));

            if (results.Count > 0)
            {
                Console.WriteLine("Search results:");
                foreach (var book in results)
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine("No books found matching the search criteria.");
            }
        }

        public void BorrowBook(string isbn)
        {
            var book = _books.Find(b => b.ISBN == isbn);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                Console.WriteLine($"Book {book.Title} by {book.Author} has been borrowed.");
            }
            else
            {
                Console.WriteLine("Book is not available for borrowing.");
            }
        }

        public void ReturnBook(string isbn)
        {
            var book = _books.Find(b => b.ISBN == isbn);
            if (book != null && !book.IsAvailable)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Book {book.Title} by {book.Author} has been returned.");
            }
            else
            {
                Console.WriteLine("Book is not found or was not borrowed.");
            }
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                Library library = new Library();

                // Adding books to the library
                library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
                library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
                library.AddBook(new Book("1984", "George Orwell", "9780451524935"));

                // Searching and borrowing books
                Console.WriteLine("Searching and borrowing books...");
                library.BorrowBook("Gatsby");
                library.BorrowBook("1984");
                library.BorrowBook("Harry Potter"); // This book is not in the library

                // Returning books
                Console.WriteLine("\nReturning books...");
                library.ReturnBook("Gatsby");
                library.ReturnBook("Harry Potter"); // This book is not borrowed

                Console.ReadLine();

            }
        }
    }

}
