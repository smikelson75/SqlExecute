using SqlExecute.Engine.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Actions
{
    public class ActionParameters : IEnumerable<object>
    {
        public Dictionary<string, object> Parameters { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        public void AddParameter(string key, object value)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);

            if (Parameters.ContainsKey(key))
                throw new ActionParameterAlreadyExistsException(key);

            Parameters.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return Parameters.ContainsKey(key);
        }

        public bool ContainsKey(string key, Type expectedType)
        {
            if (!Parameters.TryGetValue(key, out object? value))
                return false;

            return value.GetType() == expectedType;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return Parameters.Values.GetEnumerator();
        }

        public TReturn GetParameter<TReturn>(string key)
        {
            if (Parameters.TryGetValue(key, out object? value))
                return (TReturn)value;

            throw new ActionParameterNotFoundException(key);
        }

        public bool RemoveParameter(string key)
        {
            return Parameters.Remove(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return Parameters.Count;
            }
        }

        public void AddParameterRange(IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                AddParameter(parameter.Key, parameter.Value);
            }
        }
    }
}
