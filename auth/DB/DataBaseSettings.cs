using System;
namespace auth.database
{
    public interface IDataBaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class DatabaseSettings : IDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

