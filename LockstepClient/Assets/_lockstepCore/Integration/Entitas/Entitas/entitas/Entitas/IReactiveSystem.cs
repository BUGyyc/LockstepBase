namespace Entitas
{
    public interface IReactiveSystem : IExecuteSystem, ISystem
    {
        void Activate();

        void Deactivate();

        void Clear();
    }
}
