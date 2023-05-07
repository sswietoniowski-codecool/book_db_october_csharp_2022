using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Codecool.BookDb.Manager;

public class BookDbManager
{
    public string ConnectionString => ConfigurationManager.AppSettings["connectionString"];

    public void Connect()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        Console.WriteLine($"Connected...{connection.ServerVersion}");
    }
}