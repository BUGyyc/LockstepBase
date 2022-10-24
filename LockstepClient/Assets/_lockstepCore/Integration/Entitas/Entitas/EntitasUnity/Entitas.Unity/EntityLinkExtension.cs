using UnityEngine;

namespace Entitas.Unity
{

    public static class EntityLinkExtension
    {
        public static EntityLink GetEntityLink(this GameObject gameObject)
        {
            return gameObject.GetComponent<EntityLink>();
        }

        public static EntityLink Link(this GameObject gameObject, IEntity entity)
        {
            EntityLink entityLink = gameObject.GetEntityLink();
            if ((Object)(object)entityLink == (Object)null)
            {
                entityLink = gameObject.AddComponent<EntityLink>();
            }
            entityLink.Link(entity);
            return entityLink;
        }

        public static void Unlink(this GameObject gameObject)
        {
            gameObject.GetEntityLink().Unlink();
        }
    }
}
