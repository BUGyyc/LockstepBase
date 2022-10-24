using DesperateDevs.Utils;

namespace Entitas
{
    public static class PublicMemberInfoEntityExtension
    {
        /// Adds copies of all specified components to the target entity.
        /// If replaceExisting is true it will replace exisintg components.
        public static void CopyTo(this IEntity entity, IEntity target, bool replaceExisting = false, params int[] indices)
        {
            int[] array = ((indices.Length == 0) ? entity.GetComponentIndices() : indices);
            foreach (int index in array)
            {
                IComponent component = entity.GetComponent(index);
                IComponent component2 = target.CreateComponent(index, component.GetType());
                PublicMemberInfoExtension.CopyPublicMemberValues((object)component, (object)component2);
                if (replaceExisting)
                {
                    target.ReplaceComponent(index, component2);
                }
                else
                {
                    target.AddComponent(index, component2);
                }
            }
        }
    }
}


