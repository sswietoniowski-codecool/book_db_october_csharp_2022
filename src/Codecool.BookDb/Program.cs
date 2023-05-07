using System;
using Codecool.BookDb.Manager;

namespace Codecool.BookDb
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BookDbManager manager = new BookDbManager();
            manager.Connect();
        }
    }
}
