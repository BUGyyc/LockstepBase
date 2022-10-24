﻿using System;

namespace Entitas.CodeGeneration.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true)]
    public class EventAttribute : Attribute
    {
        public readonly EventTarget eventTarget;

        public readonly EventType eventType;

        public readonly int priority;

        public EventAttribute(EventTarget eventTarget, EventType eventType = EventType.Added, int priority = 0)
        {
            this.eventTarget = eventTarget;
            this.eventType = eventType;
            this.priority = priority;
        }
    }
}
