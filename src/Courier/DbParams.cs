using Courier.Core.Database;

namespace Courier;

public class DbParams : IDbParams
{
    internal const string DatabaseFilename = "courier.db3";

    public string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public SQLiteOpenFlags Flags =>
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
}
