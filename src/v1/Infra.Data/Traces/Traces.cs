using System.Diagnostics;

namespace Infra.Data.Traces;

public static class Traces
{
    private const string ServiceName = "CustomerAPI";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}
