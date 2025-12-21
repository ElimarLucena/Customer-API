using System.Diagnostics;

namespace Infra.Data.Traces;

public static class Traces
{
    public const string ServiceName = "CustomerAPI";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}
