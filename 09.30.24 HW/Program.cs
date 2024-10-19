using System;
using System.Collections.Generic;


//Task 1
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
}

public class BookManager
{
    private LinkedList<Book> books = new LinkedList<Book>();

    public LinkedList<Book> Books => books;

    public void AddBook(Book book)
    {
        books.AddLast(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public void UpdateBook(Book oldBook, Book newBook)
    {
        LinkedListNode<Book> node = books.Find(oldBook);
        if (node != null)
        {
            node.Value = newBook;
        }
    }

    public Book SearchBook(Func<Book, bool> predicate)
    {
        foreach (var book in books)
        {
            if (predicate(book))
                return book;
        }
        return null;
    }

    public void InsertAtStart(Book book)
    {
        books.AddFirst(book);
    }

    public void InsertAtEnd(Book book)
    {
        books.AddLast(book);
    }

    public void InsertAtPosition(Book book, int position)
    {
        if (position < 0 || position > books.Count)
            throw new ArgumentOutOfRangeException();

        LinkedListNode<Book> current = books.First;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        books.AddBefore(current, book);
    }

    public void RemoveFromStart()
    {
        if (books.First != null)
            books.RemoveFirst();
    }

    public void RemoveFromEnd()
    {
        if (books.Last != null)
            books.RemoveLast();
    }

    public void RemoveFromPosition(int position)
    {
        if (position < 0 || position >= books.Count)
            throw new ArgumentOutOfRangeException();

        LinkedListNode<Book> current = books.First;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        books.Remove(current);
    }
}


//Task 2
public class DictionaryManager
{
    private Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

    public void AddWord(string english, List<string> french)
    {
        dictionary[english] = french;
    }

    public void RemoveWord(string english)
    {
        dictionary.Remove(english);
    }

    public void RemoveTranslation(string english, string french)
    {
        if (dictionary.ContainsKey(english))
        {
            dictionary[english].Remove(french);
        }
    }

    public void UpdateWord(string oldEnglish, string newEnglish)
    {
        if (dictionary.ContainsKey(oldEnglish))
        {
            var translations = dictionary[oldEnglish];
            dictionary.Remove(oldEnglish);
            dictionary[newEnglish] = translations;
        }
    }

    public void UpdateTranslation(string english, string oldFrench, string newFrench)
    {
        if (dictionary.ContainsKey(english))
        {
            var translations = dictionary[english];
            int index = translations.IndexOf(oldFrench);
            if (index != -1)
            {
                translations[index] = newFrench;
            }
        }
    }

    public List<string> SearchTranslation(string english)
    {
        if (dictionary.ContainsKey(english))
        {
            return dictionary[english];
        }
        return null;
    }
}


public class Program
{
    public static void Main()
    {
        //Task 1
        BookManager bookManager = new BookManager();

        Book book1 = new Book { Title = "He comes at night", Author = "Scoot Lee", Genre = "Action", Year = 2000 };
        Book book2 = new Book { Title = "Woodlands", Author = "Gamry Roodland", Genre = "Detective", Year = 2005 };
        Book book3 = new Book { Title = "Shine", Author = "Steven King", Genre = "Horror", Year = 1977 };

        bookManager.AddBook(book1);
        bookManager.AddBook(book2);
        bookManager.InsertAtStart(book3);

        Console.WriteLine("Books in the list:");
        foreach (var book in bookManager.Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Year: {book.Year}");
        }

        bookManager.RemoveFromEnd();

        Console.WriteLine("\nBooks in the list after removal from end:");
        foreach (var book in bookManager.Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Year: {book.Year}");
        }

        Book updatedBook = new Book { Title = "Updated Book", Author = "Updated Author", Genre = "Updated Genre", Year = 2023 };
        bookManager.UpdateBook(book1, updatedBook);

        Console.WriteLine("\nBooks in the list after update:");
        foreach (var book in bookManager.Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Year: {book.Year}");
        }


        //Task 2
        DictionaryManager dictionaryManager = new DictionaryManager();

        dictionaryManager.AddWord("hello", new List<string> { "bonjour"});
        dictionaryManager.AddWord("goodbye", new List<string> { "au revoir"});

        Console.WriteLine("Translation for 'hello':");
        foreach (var translation in dictionaryManager.SearchTranslation("hello"))
        {
            Console.WriteLine(translation);
        }

        dictionaryManager.RemoveTranslation("hello", "salut");

        Console.WriteLine("\nTranslations for 'hello' after removal:");
        foreach (var translation in dictionaryManager.SearchTranslation("hello"))
        {
            Console.WriteLine(translation);
        }

        dictionaryManager.UpdateWord("goodbye", "farewell");

        Console.WriteLine("\nTranslations for 'farewell':");
        foreach (var translation in dictionaryManager.SearchTranslation("farewell"))
        {
            Console.WriteLine(translation);
        }
    }
}