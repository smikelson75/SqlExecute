using Executor = SqlExecute.Engine.Engine;

namespace SqlExecute.Tests.Engine.Core;

[Collection("EngineTests")]
public class EngineTests(EngineTestFixture fixture)
{
    [Fact]
    public void SetupEngineForExecution()
    {
        var filePath = "config.yaml";

        var engine = Executor.Create(filePath);

        Assert.NotNull(engine);
        //Assert.NotNull(engine.Connections);
        //Assert.NotEmpty(engine.Connections);
    }
}
