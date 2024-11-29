using SqlExecute.Engine.Exceptions;
using System.Collections;

namespace SqlExecute.Engine.Actions
{
    /// <summary>
    /// Represents a collection of action parameters.
    /// </summary>
    /// <remarks>
    /// This class provides methods to add, remove, and retrieve parameters by key. 
    /// It also supports case-insensitive key comparisons.
    /// </remarks>
    /// <example>
    /// <code>
    /// var actionParameters = new ActionParameters();
    /// actionParameters.AddParameter("connection", "local");
    /// actionParameters.AddParameter("queries", new List<object> { "SELECT * FROM Users", "SELECT * FROM Orders" });
    /// 
    /// string connection = actionParameters.GetParameter<string>("connection");
    /// List<object> queries = actionParameters.GetParameter<List<object>>("queries");
    /// </code>
    /// </example>
    public class ActionParameters : IEnumerable<object>
    {
        /// <summary>
        /// Gets the dictionary of parameters.
        /// </summary>
        public Dictionary<string, object> Parameters { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Adds a parameter to the collection.
        /// </summary>
        /// <param name="key">The key of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown when the key or value is null.</exception>
        /// <exception cref="ActionParameterAlreadyExistsException">Thrown when a parameter with the same key already exists.</exception>
        public void AddParameter(string key, object value)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);

            if (Parameters.ContainsKey(key))
                throw new ActionParameterAlreadyExistsException(key);

            Parameters.Add(key, value);
        }

        /// <summary>
        /// Determines whether the collection contains a parameter with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the collection.</param>
        /// <returns>true if the collection contains a parameter with the key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            return Parameters.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the collection contains a parameter with the specified key and type.
        /// </summary>
        /// <param name="key">The key to locate in the collection.</param>
        /// <param name="expectedType">The expected type of the parameter value.</param>
        /// <returns>true if the collection contains a parameter with the key and type; otherwise, false.</returns>
        public bool ContainsKey(string key, Type expected)
        {
            if (!Parameters.TryGetValue(key, out object? value))
                return false;

            if (IsGenericList(expected))
            {
                var expectedElementType = GetGenericType(expected);
                var valueType = value.GetType();

                if (IsGenericList(valueType) && IsGenericArgumentObject(valueType))
                {
                    var listObjects = (List<object>)value;
                    return listObjects.TrueForAll(x => x.GetType() == expectedElementType);
                }
            }

            return expected == value.GetType();
        }

        private static Type? GetGenericType(Type expected)
        {
            return expected.GetGenericArguments().FirstOrDefault();
        }

        private static bool IsGenericList(Type expected)
        {
            return expected.IsGenericType && expected.GetGenericTypeDefinition() == typeof(List<>);
        }

        private static bool IsGenericArgumentObject(Type valueType)
        {
            return valueType.GetGenericArguments().Length == 1 && valueType.GetGenericArguments().FirstOrDefault() == typeof(object);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<object> GetEnumerator()
        {
            return Parameters.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets the parameter value associated with the specified key.
        /// </summary>
        /// <typeparam name="TReturn">The type of the parameter value.</typeparam>
        /// <param name="key">The key of the parameter to get.</param>
        /// <returns>The parameter value associated with the specified key.</returns>
        /// <exception cref="ActionParameterNotFoundException">Thrown when the key is not found in the collection.</exception>
        /// <exception cref="ActionParameterInvalidRequestTypeException">Thrown when the type of the parameter value does not match the requested type.</exception>
        public TReturn GetParameter<TReturn>(string key)
        {
            if (!Parameters.TryGetValue(key, out object? value))
            {
                throw new ActionParameterNotFoundException(key);
            }

            if (value is TReturn @return)
            {
                return @return;
            }

            throw new ActionParameterInvalidRequestTypeException(typeof(TReturn), value.GetType());
        }

        /// <summary>
        /// Removes the parameter with the specified key from the collection.
        /// </summary>
        /// <param name="key">The key of the parameter to remove.</param>
        /// <returns>true if the parameter is successfully removed; otherwise, false.</returns>
        public bool RemoveParameter(string key)
        {
            return Parameters.Remove(key);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of parameters in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return Parameters.Count;
            }
        }

        /// <summary>
        /// Adds a range of parameters to the collection.
        /// </summary>
        /// <param name="parameters">The parameters to add.</param>
        public void AddParameterRange(IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                AddParameter(parameter.Key, parameter.Value);
            }
        }
    }
}
