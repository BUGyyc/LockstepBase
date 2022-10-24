using System;
using UnityEngine;

namespace Entitas.Unity
{

    public class EntityLink : MonoBehaviour
    {
        private IEntity _entity;

        private bool _applicationIsQuitting;

        public IEntity entity => _entity;

        public void Link(IEntity entity)
        {
            if (_entity != null)
            {
                throw new Exception(string.Concat("EntityLink is already linked to ", _entity, "!"));
            }
            _entity = entity;
            _entity.Retain(this);
        }

        public void Unlink()
        {
            if (_entity == null)
            {
                throw new Exception("EntityLink is already unlinked!");
            }
            _entity.Release(this);
            _entity = null;
        }

        private void OnDestroy()
        {
            if (!_applicationIsQuitting && _entity != null)
            {
                Debug.LogWarning((object)string.Concat("EntityLink got destroyed but is still linked to ", _entity, "!\nPlease call gameObject.Unlink() before it is destroyed."));
            }
        }

        private void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        public override string ToString()
        {
            //return "EntityLink(" + ((Object)((Component)this).get_gameObject()).get_name() + ")";

            return "EntityLink(" + ((Component)this).gameObject.name + ")";
        }
    }
}