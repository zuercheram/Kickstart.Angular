using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Kickstart.Angular.Services.Tests.Helper;

public class XunitLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper;

    public XunitLoggerProvider(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XunitLogger(_testOutputHelper, categoryName);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
