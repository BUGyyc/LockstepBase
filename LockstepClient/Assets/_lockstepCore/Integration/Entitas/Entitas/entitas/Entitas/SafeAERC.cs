﻿using System.Collections.Generic;

namespace Entitas
{

    /// Automatic Entity Reference Counting (AERC)
    /// is used internally to prevent pooling retained entities.
    /// If you use retain manually you also have to
    /// release it manually at some point.
    /// SafeAERC checks if the entity has already been
    /// retained or released. It's slower, but you keep the information
    /// about the owners.
    public sealed class SafeAERC : IAERC
    {
        private readonly IEntity _entity;

        private readonly HashSet<object> _owners = new HashSet<object>();

        public int retainCount => _owners.Count;

        public HashSet<object> owners => _owners;

        public SafeAERC(IEntity entity)
        {
            _entity = entity;
        }

        public void Retain(object owner)
        {
            if (!owners.Add(owner))
            {
                throw new EntityIsAlreadyRetainedByOwnerException(_entity, owner);
            }
        }

        public void Release(object owner)
        {
            if (!owners.Remove(owner))
            {
                throw new EntityIsNotRetainedByOwnerException(_entity, owner);
            }
        }
    }
}