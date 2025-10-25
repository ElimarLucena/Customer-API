using System.Data;

namespace Infra.Data.DbContext
{
    public interface ISqlServerDataBaseContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
