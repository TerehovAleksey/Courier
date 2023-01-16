namespace Courier.Core.Database;

public interface IDbParams
{
    public string DatabasePath { get; }
    public SQLiteOpenFlags Flags { get; }
}
