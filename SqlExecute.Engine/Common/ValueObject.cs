using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Common
{
    /// <summary>
    /// Represents an abstract base class for Value Objects in Domain-Driven Design.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Value Objects are immutable objects that are defined by their structural equality 
    /// rather than their identity. They are characterized by:
    /// - Immutability: Their state cannot be modified after creation
    /// - Equality based on composition: Objects are equal if all their components are equal
    /// - No distinct identity: Two Value Objects with the same components are considered equivalent
    /// </para>
    /// 
    /// <para>
    /// Key Design Principles:
    /// - Encapsulates complex logic and validation within the object
    /// - Ensures objects remain in a valid state throughout their lifetime
    /// - Provides type-safe equality comparisons
    /// - Supports deep comparison and hashing based on internal components
    /// </para>
    /// 
    /// <example>
    /// <code>
    /// public class Address : ValueObject
    /// {
    ///     public string Street { get; }
    ///     public string City { get; }
    ///     public string PostalCode { get; }
    ///     
    ///     public Address(string street, string city, string postalCode)
    ///     {
    ///         Street = street ?? throw new ArgumentNullException(nameof(street));
    ///         City = city ?? throw new ArgumentNullException(nameof(city));
    ///         PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
    ///     }
    ///     
    ///     protected override IEnumerable<object> GetEqualityComponents()
    ///     {
    ///         yield return Street;
    ///         yield return City;
    ///         yield return PostalCode;
    ///     }
    ///     
    ///     public override object Clone()
    ///     {
    ///         return new Address(Street, City, PostalCode);
    ///     }
    /// }
    /// 
    /// // Usage
    /// var address1 = new Address("123 Main St", "New York", "10001");
    /// var address2 = new Address("123 Main St", "New York", "10001");
    /// var address3 = new Address("456 Elm St", "Chicago", "60601");
    /// 
    /// Console.WriteLine(address1 == address2);  // true
    /// Console.WriteLine(address1 == address3);  // false
    /// </code>
    /// </example>
    /// 
    /// <seealso cref="IEquatable{T}"/>
    /// <seealso cref="ICloneable"/>
    /// </remarks>
    /// <typeparam name="TValueObject">The type of the concrete Value Object implementation.</typeparam>
    /// <typeparam name="TValueObject">The type of the concrete Value Object implementation.</typeparam>
    public abstract class ValueObject : IEquatable<ValueObject>, ICloneable
    {
        /// <summary>
        /// Retrieves the components used to determine object equality.
        /// Derived classes must implement this method to specify which properties 
        /// should be compared when checking for equality.
        /// </summary>
        /// <returns>An enumerable collection of objects used for equality comparison.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Creates a deep copy of the current Value Object.
        /// Must be implemented by derived classes to provide proper cloning mechanism.
        /// </summary>
        /// <returns>A deep copy of the current Value Object.</returns>
        public abstract object Clone();

        /// <summary>
        /// Determines whether the current Value Object is equal to another Value Object.
        /// This method provides a type-safe equality comparison.
        /// </summary>
        /// <param name="other">The Value Object to compare with the current object.</param>
        /// <returns>True if the Value Objects are equal; otherwise, false.</returns>
        public bool Equals(ValueObject? other)
        {
            if (other is null)
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Determines whether the current Value Object is equal to another object.
        /// This method provides a type-agnostic equality comparison.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Generates a hash code for the current Value Object based on its equality components.
        /// Ensures that objects with the same components generate the same hash code.
        /// </summary>
        /// <returns>A hash code for the current Value Object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Provides a protected method for implementing equality comparison between two Value Objects.
        /// </summary>
        /// <param name="left">The first Value Object to compare.</param>
        /// <param name="right">The second Value Object to compare.</param>
        /// <returns>True if the Value Objects are considered equal; otherwise, false.</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null || right is null)
                return false;

            return ReferenceEquals(left, right) || left.Equals(right);
        }

        /// <summary>
        /// Provides a protected method for implementing inequality comparison between two Value Objects.
        /// </summary>
        /// <param name="left">The first Value Object to compare.</param>
        /// <param name="right">The second Value Object to compare.</param>
        /// <returns>True if the Value Objects are not equal; otherwise, false.</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        /// <summary>
        /// Defines the equality operator for Value Objects.
        /// Allows using the == operator to compare Value Objects.
        /// </summary>
        /// <param name="one">The first Value Object to compare.</param>
        /// <param name="two">The second Value Object to compare.</param>
        /// <returns>True if the Value Objects are equal; otherwise, false.</returns>
        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return EqualOperator(one, two);
        }

        /// <summary>
        /// Defines the inequality operator for Value Objects.
        /// Allows using the != operator to compare Value Objects.
        /// </summary>
        /// <param name="one">The first Value Object to compare.</param>
        /// <param name="two">The second Value Object to compare.</param>
        /// <returns>True if the Value Objects are not equal; otherwise, false.</returns>
        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return NotEqualOperator(one, two);
        }
    }
}
