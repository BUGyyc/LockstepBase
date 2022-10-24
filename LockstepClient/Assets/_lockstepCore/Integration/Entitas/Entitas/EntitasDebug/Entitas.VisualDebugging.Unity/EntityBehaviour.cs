using System.Collections.Generic;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{

    [ExecuteInEditMode]
    public class EntityBehaviour : MonoBehaviour
    {
        private IContext _context;

        private IEntity _entity;

        private Stack<EntityBehaviour> _entityBehaviourPool;

        private string _cachedName;

        public IContext context => _context;

        public IEntity entity => _entity;

        public void Init(IContext context, IEntity entity, Stack<EntityBehaviour> entityBehaviourPool)
        {
            _context = context;
            _entity = entity;
            _entityBehaviourPool = entityBehaviourPool;
            _entity.OnEntityReleased += onEntityReleased;



            //((Object)((Component)this).get_gameObject()).set_hideFlags((HideFlags)0);
            //((Component)this).get_gameObject().SetActive(true);

            ((Component)this).gameObject.hideFlags = (HideFlags)0;
            ((Component)this).gameObject.SetActive(true);
            Update();
        }

        private void onEntityReleased(IEntity e)
        {
            _entity.OnEntityReleased -= onEntityReleased;
            //((Component)this).get_gameObject().SetActive(false);
            //((Object)((Component)this).get_gameObject()).set_hideFlags((HideFlags)1);

            ((Component)this).gameObject.SetActive(false);
            ((Component)this).gameObject.hideFlags = (HideFlags)1;

            _entityBehaviourPool.Push(this);
            _cachedName = null;
            //((Object)this).set_name(string.Empty);
            ((Object)this).name = string.Empty;
        }

        private void Update()
        {
            if (_entity != null && _cachedName != _entity.ToString())
            {
                //((Object)this).set_name(_cachedName = _entity.ToString());

                ((Object)this).name = (_cachedName = entity.ToString());
            }
        }

        private void OnDestroy()
        {
            if (_entity != null)
            {
                _entity.OnEntityReleased -= onEntityReleased;
            }
        }
    }
}