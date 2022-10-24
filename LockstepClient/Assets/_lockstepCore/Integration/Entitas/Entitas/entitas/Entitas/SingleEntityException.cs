namespace Entitas
{
    public class SingleEntityException : EntitasException
    {
        public SingleEntityException(int count)
            : base("Expected exactly one entity in collection but found " + count + "!", "Use collection.SingleEntity() only when you are sure that there is exactly one entity.")
        {
        }
    }
}