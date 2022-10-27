using BEPUutilities;

namespace Lockstep.Game.Features.Navigation.RVO.Algorithm
{
    internal class Obstacle
    {
        internal Obstacle next_;

        internal Obstacle previous_;

        internal Vector2 direction_;

        internal Vector2 point_;

        internal int id_;

        internal bool convex_;
    }
}


