using SqlExecute.Engine.Actions;
using SqlExecute.Storage.Yaml.Models;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Reflection.PortableExecutable;
using SqlExecute.Engine.Exceptions;
using Xunit.Abstractions;

namespace SqlExecute.Tests.Engine.Core.ActionParametersTests
{
    [Collection("ActionParametersTests")]
    public class ActionParametersTests(ActionParametersTestFixture fixture)
    {
        private readonly Configuration _configuration = fixture.Configuration;

        [Fact]
        public void Create_WhenConfigurationIsProvided_ShouldCreateParameters()
        {
            foreach (var action in _configuration.Actions)
            {
                var actionParameter = new ActionParameters();
                foreach (var parameter in action.Parameters)
                {
                    actionParameter.AddParameter(parameter.Key, parameter.Value);

                }

                Assert.Equal(2, actionParameter.Count);

                Assert.Equal("local", 
                    Assert.IsType<string>(actionParameter.GetParameter<string>("connection")));

                Assert.IsType<List<object>>(actionParameter.GetParameter<List<object>>("queries"));

            }
        }
    }
}
