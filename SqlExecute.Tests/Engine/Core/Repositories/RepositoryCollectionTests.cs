using SqlExecute.Engine.Exceptions;
using SqlExecute.Engine.Repositories;
using SqlExecute.Engine.Repositories.Abstractions;

namespace SqlExecute.Tests.Engine.Core.Repositories
{
    [Collection("RepositoryCollectionTests")]
    public class RepositoryCollectionTests
    {
        private readonly RepositoryCollection _collection;
        private readonly IRepositoryAsync _repository;

        public RepositoryCollectionTests(RepositoryCollectionTestFixture fixture)
        {
            _collection = fixture.Collection;
            _repository = fixture.Repository;
        }

        [Fact]
        public void RetrieveSqlRepositoryWhenRegisteredToTheCollection()
        {
            _collection.Add("sql", _repository);

            Assert.NotEmpty(_collection);
            Assert.NotNull(_collection.Get("sql"));
            Assert.True(_collection.Remove("sql"));
            Assert.Empty(_collection);
        }

        [Fact]
        public void ThrowRepositoryAlreadyExistsExceptionWhenAddingNewRepositoryUnderSameKey()
        {
            _collection.Add("newRepo", _repository);
            _ = Assert.Throws<RepositoryAlreadyExistsException>(() => _collection.Add("newRepo", _repository));
            _collection.Remove("newRepo");
        }

        [Theory]
        [InlineData(null)]
        public void ThrowsArgumentNullExceptionWhenRepositoryIsNullAndAdded(IRepositoryAsync value)
        {
            _ = Assert.Throws<ArgumentNullException>(() => _collection.Add("anotherRepo", value));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ThrowsArgumentExceptionWhenKeyIsInvalid(string key)
        {
            Assert.Throws<ArgumentException>(() => _collection.Add(key, _repository));
        }

        [Theory]
        [InlineData(null)]
        public void ThrowsArgumentNullExceptionWhenKeyIsNull(string key)
        {
            Assert.Throws<ArgumentNullException>(() => _collection.Add(key, _repository));
        }
    }
}
