using Microsoft.Data.SqlClient;
using System;
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
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            string selectAuthorSql =
                @"
                SELECT id, first_name, last_name, birth_date
                FROM author
                WHERE id=@id;
                ";

            command.CommandText = selectAuthorSql;
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            Author author = null;

            if (reader.Read())
            {
                string firstName = (string)reader["first_name"];
                string lastName = (string)reader["last_name"];
                DateTime birthDate = Convert.ToDateTime(reader["birth_date"]);

                author = new Author(firstName, lastName, birthDate);
                author.Id = id;

            }

            return author;
        }

        catch (SqlException exception)
        {
            // tutaj mógłbym dodać logowanie

            throw;
        }
    }

    public List<Author> GetAll()
    {
        try
        {
            var authors = new List<Author>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            string selectAuthorsSql =
                @"
SELECT id, first_name, last_name, birth_date
FROM author
";

            command.CommandText = selectAuthorsSql;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                string firstName = (string)reader["first_name"];
                string lastName = (string)reader["last_name"];
                DateTime birthDate = Convert.ToDateTime(reader["birth_date"]);

                var author = new Author(firstName, lastName, birthDate);
                author.Id = id;

                authors.Add(author);
            }

            return authors;
        }
        catch (SqlException exception)
        {
            // tutaj mógłbym dodać logowanie    

            throw;
        }
    }
}