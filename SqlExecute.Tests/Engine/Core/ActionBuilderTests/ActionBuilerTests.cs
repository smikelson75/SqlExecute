using SqlExecute.Storage.Yaml.Models;
using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Actions.Impementations;

namespace SqlExecute.Tests.Engine.Core.ActionBuilderTests
{
    [CollectionDefinition("ActionBuilerTests")]
    public class ActionBuilerTests(ActionBuilderTestFixture fixture)
    {
        public Configuration Configuration { get; } = fixture.Configuration;
        public RepositoryCollection Collection { get; } = fixture.Collection;

        [Fact]
        public void Build_WhenConfigurationIsProvided_ShouldReturnExecutionAsync()
        {
            Assert.NotEmpty(Configuration.Actions);

            Assert.Collection(Configuration.Actions,
                action =>
                {
                    Assert.NotNull(action.Name);
                    Assert.NotEmpty(action.Parameters);
                });

            //var builder = new ActionBuilder();
            //Assert.IsType<NonQueryAction>(builder.Build()
        }
    }
}
