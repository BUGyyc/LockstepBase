using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{
    public class ContextObserver
    {
        private readonly IContext _context;

        private readonly List<IGroup> _groups;

        private readonly GameObject _gameObject;

        private readonly Stack<EntityBehaviour> _entityBehaviourPool = new Stack<EntityBehaviour>();

        private StringBuilder _toStringBuilder = new StringBuilder();

        public IContext context => _context;

        public IGroup[] groups => _groups.ToArray();

        public GameObject gameObject => _gameObject;

        public ContextObserver(IContext context)
        {
            //IL_002f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0039: Expected O, but got Unknown
            _context = context;
            _groups = new List<IGroup>();
            _gameObject = new GameObject();
            _gameObject.AddComponent<ContextObserverBehaviour>().Init(this);
            _context.OnEntityCreated += onEntityCreated;
            _context.OnGroupCreated += onGroupCreated;
        }

        public void Deactivate()
        {
            _context.OnEntityCreated -= onEntityCreated;
            _context.OnGroupCreated -= onGroupCreated;
        }

        private void onEntityCreated(IContext context, IEntity entity)
        {
            //IL_000e: Unknown result type (might be due to invalid IL or missing references)
            EntityBehaviour obj = ((_entityBehaviourPool.Count > 0) ? _entityBehaviourPool.Pop() : new GameObject().AddComponent<EntityBehaviour>());
            obj.Init(context, entity, _entityBehaviourPool);
            //((Component)obj).get_transform().SetParent(_gameObject.get_transform(), false);

            ((Component)obj).transform.SetParent(_gameObject.transform, false);
        }

        private void onGroupCreated(IContext context, IGroup group)
        {
            _groups.Add(group);
        }

        public override string ToString()
        {
            _toStringBuilder.Length = 0;
            _toStringBuilder.Append(_context.contextInfo.name).Append(" (").Append(_context.count)
                .Append(" entities, ")
                .Append(_context.reusableEntitiesCount)
                .Append(" reusable, ");
            if (_context.retainedEntitiesCount != 0)
            {
                _toStringBuilder.Append(_context.retainedEntitiesCount).Append(" retained, ");
            }
            _toStringBuilder.Append(_groups.Count).Append(" groups)");
            string text = _toStringBuilder.ToString();
            //((Object)_gameObject).set_name(text);
            ((Object)_gameObject).name = (text);
            return text;
        }
    }
}