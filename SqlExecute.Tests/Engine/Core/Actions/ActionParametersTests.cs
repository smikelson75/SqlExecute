using SqlExecute.Engine.Actions;
using SqlExecute.Engine.Exceptions;
using SqlExecute.Storage.Yaml.Models;

namespace SqlExecute.Tests.Engine.Core.Actions
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

        [Fact]
        public void ThrowArgumentNullExceptionWhenActionParameterValueIsNull()
        {
            var actionParameter = new ActionParameters();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = Assert.Throws<ArgumentNullException>(() => actionParameter.AddParameter("something", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void ThrowArgumentNullExcetionWhenActionParameterKeyIsEmptyString(string key)
        {
            var actionParameter = new ActionParameters();

            _ = Assert.Throws<ArgumentException>(() => actionParameter.AddParameter(key, new() { }));
        }

        [Theory]
        [InlineData(null)]
        public void ThrowArgumentnullExceptionWhenActionParameterKeyIsNull(string key)
        {
            var actionParameter = new ActionParameters();

            _ = Assert.Throws<ArgumentNullException>(() => actionParameter.AddParameter(key, new() { }));
        }

        [Fact]
        public void ThrowActionParameterAlreadyExistsExceptionWhenParameterKeyHasAlreadyBeenAdded()
        {
            var actionParameter = new ActionParameters();

            actionParameter.AddParameter("something", new());

            _ = Assert.Throws<ActionParameterAlreadyExistsException>(() => actionParameter.AddParameter("something", new()));
        }

        [Fact]
        public void ThrowActionParameterInvalidRequestTypeExceptionWhenGetParameterTypeRequestedDoesNotMatchValueTypeReturned()
        {
            var actionParameter = new ActionParameters();

            actionParameter.AddParameter("something", 1234);

            _ = Assert.Throws<ActionParameterInvalidRequestTypeException>(() => actionParameter.GetParameter<string>("something"));
        }
    }
}
