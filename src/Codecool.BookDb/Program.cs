using System;
using System.Collections.Generic;
using Codecool.BookDb.Manager;
using Codecool.BookDb.Model;

namespace Codecool.BookDb;

public static class Program
{
    public static void Main(string[] args)
    {
        BookDbManager manager = new BookDbManager();
        manager.Connect();

        IAuthorDao authorDao = new MssqlAuthorDao(manager.ConnectionString);

        //AddAuthor(authorDao);
        PrintAuthors(authorDao);
    }

    public static void AddAuthor(IAuthorDao authorDao)
    {
        Author author = new Author("Jan", "Kowalski", new DateTime(1990, 1, 1));

        Console.WriteLine($"Author before add: {author}");
        authorDao.Add(author);
        Console.WriteLine($"Author after add: {author}");
    }

    public static void PrintAuthors(IAuthorDao authorDao)
    {
        List<Author> authors = authorDao.GetAll();

        foreach (Author author in authors)
        {
            Console.WriteLine(author);
        }
    }
}