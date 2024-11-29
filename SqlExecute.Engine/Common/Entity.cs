namespace SqlExecute.Engine.Common
{
    /// <summary>
    /// Represents a base class for domain entities with a unique identity.
    /// </summary>
    /// <typeparam name="TIdentity">
    /// The type of the entity's unique identifier. 
    /// Must be a non-nullable type that can be used for equality comparisons.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// Entities are domain objects that have a distinct identity. Unlike Value Objects, 
    /// entities are defined by their unique identifier rather than their attributes.
    /// </para>
    /// <para>
    /// Key characteristics:
    /// - Equality is based solely on the entity's unique identifier
    /// - Supports type-safe equality comparisons
    /// - Provides standard equality and inequality operators
    /// </para>
    /// <example>
    /// <code>
    /// // Example usage
    /// var user1 = new User(1, "John Doe");
    /// var user2 = new User(1, "Jane Doe");
    /// 
    /// // These are considered equal because they have the same Id
    /// bool areEqual = user1 == user2; // true
    /// </code>
    /// </example>
    /// </remarks>
    public sealed class Entity<TIdentity>(TIdentity id) : IEquatable<Entity<TIdentity>> where TIdentity : notnull
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        /// <value>The unique identifier that distinguishes this entity from others.</value>
        public TIdentity Id { get; private set; } = id;

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to comapre with the current instance</param>
        /// <returns><c>true</c> if the specified object is an <see cref="Entity{TIdentity}"/> with the same identifier as the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Entity<TIdentity> entity && Id.Equals(entity.Id);
        }

        /// <summary>
        /// Implements the equality operator for <see cref="Entity{TIdentity}"/> instances.
        /// </summary>
        /// <param name="other">The <see cref="Entity{TIdentity}"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="Entity{TIdentity}"/> instance have different identitifiers; otherwise, <c>false</c>.</returns>
        public bool Equals(Entity<TIdentity>? other)
        {
            return Equals((object?)other);
        }

        /// <summary>
        /// Implements the inequality operator for <see cref="Entity{TIdentity}"/> instances.
        /// </summary>
        /// <param name="left">The first <see cref="Entity{TIdentity}"/> to compare.</param>
        /// <param name="right">The second <see cref="Entity{TIdentity}"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="Entity{TIdentity}"/> instance have different identitifiers; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implements the equality operator for <see cref="Entity{TIdentity}"/> instances.
        /// </summary>
        /// <param name="left">The first <see cref="Entity{TIdentity}"/> to compare.</param>
        /// <param name="right">The second <see cref="Entity{TIdentity}"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="Entity{TIdentity}"/> instance have the same identitifiers; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Entity<TIdentity> left, Entity<TIdentity> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Returns a hash code for the current instance based on the identitifer.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
