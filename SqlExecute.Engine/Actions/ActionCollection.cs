using SqlExecute.Engine.Actions.Abstractions;
using SqlExecute.Engine.Exceptions;
using System.Collections;
using System.Collections.Immutable;

namespace SqlExecute.Engine.Actions
{
    public class ActionCollection : IEnumerable<IAction>
    {
        private readonly List<IAction> _actions = [];
        private readonly HashSet<string> _names = new();

        public IReadOnlyList<IAction> Actions => _actions.ToImmutableArray();

        public void Add(IAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            if (!_names.Add(action.Name))
                throw new ActionAlreadyExistsException(action.Name);

            _actions.Add(action);
        }


        public IEnumerator<IAction> GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _actions.GetEnumerator();
        }
    }
}
