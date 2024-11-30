using SqlExecute.Engine.Actions;
using SqlExecute.Engine.Actions.Implementations;
using SqlExecute.Engine.Repositories;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.Actions
{
    [Collection("ActionBuilderTests")]
    public class ActionBuilderTests
    {
        private readonly Configuration _configuration;
        private readonly RepositoryCollection _collection;

        public ActionBuilderTests(ActionBuilderTestFixture fixture)
        {
            _configuration = fixture.Configuration;
            _collection = fixture.Collection;
        }

        [Fact]
        public void NewNonQueryActionShouldBeReturned()
        {
            var builder = new NonQueryActionBuilder(_collection);

            Assert.Single(_configuration.Actions);

            var config = _configuration.Actions[0];

            var action = builder.SetName(config.Name)
                .AddParametersRange(config.Parameters)
                .Build();

            Assert.NotNull(action);
        }
    }
}
