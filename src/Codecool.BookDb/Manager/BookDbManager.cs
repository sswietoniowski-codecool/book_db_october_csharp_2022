using System.Configuration;

namespace Codecool.BookDb.Manager
{
    public class BookDbManager
    {
        public string ConnectionString => ConfigurationManager.AppSettings["connectionString"];
    }
}
