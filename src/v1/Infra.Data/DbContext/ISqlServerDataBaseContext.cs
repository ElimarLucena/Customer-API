using System.Data;

namespace Infra.Data.DbContext
{
    public interface ISqlServerDataBaseContext
    {
        IDbConnection Connection { get; }
    }
}
