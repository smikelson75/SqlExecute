using SqlExecute.Engine.Actions;
using SqlExecute.Engine.Actions.Impementations;
using SqlExecute.Engine.Repositories;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.Actions
{
    [Collection("ActionBuilderTests")]
    public class ActionBuilderTests
    {
        private readonly Configuration _configuration;
        private readonly RepositoryCollection _collection;
        private readonly ActionParameters _parameters;

        public ActionBuilderTests(ActionBuilderTestFixture fixture)
        {
            _configuration = fixture.Configuration;
            _collection = fixture.Collection;
            _parameters = fixture.Parameters;
        }

        [Fact]
        public void Build_WhenConfigurationIsProvided_ShouldReturnExecutionAsync()
        {
            var builder = new ActionFactory(_collection);
            Assert.NotEmpty(_configuration.Actions);

            Assert.Collection(_configuration.Actions,
                action =>
                {
                    Assert.NotNull(action.Name);
                    Assert.NotEmpty(action.Parameters);
                    _ = Assert.IsType<NonQueryAction>(builder.Build(action.Name, action.Type, _parameters));
                });
        }
    }
}
