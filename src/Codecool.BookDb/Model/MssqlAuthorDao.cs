using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Codecool.BookDb.Model;

public class MssqlAuthorDao : IAuthorDao
{
    private readonly string _connectionString;

    public MssqlAuthorDao(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(Author author)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            string insertAuthorSql =
                @"
INSERT INTO author (first_name, last_name, birth_date)
VALUES (@FirstName, @LastName, @BirthDate);

SELECT SCOPE_IDENTITY();
";

            command.CommandText = insertAuthorSql;
            command.Parameters.AddWithValue("@FirstName", author.FirstName);
            command.Parameters.AddWithValue("@LastName", author.LastName);
            command.Parameters.AddWithValue("@BirthDate", author.BirthDate);

            int authorId = Convert.ToInt32(command.ExecuteScalar());
            author.Id = authorId;
        }
        catch (SqlException exception)
        {
            // tutaj mógłbym dodać logowanie    

            throw;
        }
    }

    public void Update(Author author)
    {
        throw new System.NotImplementedException();
    }

    public Author Get(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Author> GetAll()
    {
        throw new System.NotImplementedException();
    }
}