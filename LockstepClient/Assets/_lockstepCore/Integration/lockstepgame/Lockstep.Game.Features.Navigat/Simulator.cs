using System.Collections.Generic;
using System.Threading.Tasks;
using BEPUutilities;
using FixMath.NET;

namespace Lockstep.Game.Features.Navigation.RVO.Algorithm
{

    internal class Simulator
    {
        internal IDictionary<uint, Agent> agents_;

        internal IList<Obstacle> obstacles_;

        internal KdTree kdTree_;

        internal Fix64 timeStep_;

        private Agent defaultAgent_;

        private Fix64 globalTime_;

        internal static Simulator Instance { get; } = new Simulator();


        internal void addAgent(uint id, Vector2 position, Vector2 destination)
        {
            if (defaultAgent_ != null)
            {
                Agent value = new Agent
                {
                    maxNeighbors_ = defaultAgent_.maxNeighbors_,
                    MaxSpeed = defaultAgent_.MaxSpeed,
                    neighborDist_ = defaultAgent_.neighborDist_,
                    Position = position,
                    Destination = destination,
                    radius_ = defaultAgent_.radius_,
                    timeHorizon_ = defaultAgent_.timeHorizon_,
                    timeHorizonObst_ = defaultAgent_.timeHorizonObst_,
                    Velocity = defaultAgent_.Velocity
                };
                agents_.Add(id, value);
            }
        }

        internal void removeAgent(uint id)
        {
            agents_.Remove(id);
        }

        internal int addObstacle(IList<Vector2> vertices)
        {
            if (vertices.Count < 2)
            {
                return -1;
            }
            int count = obstacles_.Count;
            for (int i = 0; i < vertices.Count; i++)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.point_ = vertices[i];
                if (i != 0)
                {
                    obstacle.previous_ = obstacles_[obstacles_.Count - 1];
                    obstacle.previous_.next_ = obstacle;
                }
                if (i == vertices.Count - 1)
                {
                    obstacle.next_ = obstacles_[count];
                    obstacle.next_.previous_ = obstacle;
                }
                obstacle.direction_ = Vector2.Normalize(vertices[(i != vertices.Count - 1) ? (i + 1) : 0] - vertices[i]);
                if (vertices.Count == 2)
                {
                    obstacle.convex_ = true;
                }
                else
                {
                    obstacle.convex_ = RVOMath.leftOf(vertices[(i == 0) ? (vertices.Count - 1) : (i - 1)], vertices[i], vertices[(i != vertices.Count - 1) ? (i + 1) : 0]) >= Fix64.Zero;
                }
                obstacle.id_ = obstacles_.Count;
                obstacles_.Add(obstacle);
            }
            return count;
        }

        internal void Clear()
        {
            agents_ = new Dictionary<uint, Agent>();
            defaultAgent_ = null;
            kdTree_ = new KdTree();
            obstacles_ = new List<Obstacle>();
            globalTime_ = Fix64.Zero;
            timeStep_ = 0.1m;
        }

        internal Fix64 doStep()
        {
            kdTree_.buildAgentTree();
            Parallel.ForEach(agents_.Values, delegate (Agent agent)
            {
                agent.computeNeighbors();
                agent.computeNewVelocity();
            });
            Parallel.ForEach(agents_.Values, delegate (Agent agent)
            {
                agent.update();
            });
            globalTime_ += timeStep_;
            return globalTime_;
        }

        internal Vector2 getObstacleVertex(int vertexNo)
        {
            return obstacles_[vertexNo].point_;
        }

        internal int getNextObstacleVertexNo(int vertexNo)
        {
            return obstacles_[vertexNo].next_.id_;
        }

        internal int getPrevObstacleVertexNo(int vertexNo)
        {
            return obstacles_[vertexNo].previous_.id_;
        }

        internal Fix64 getTimeStep()
        {
            return timeStep_;
        }

        internal void processObstacles()
        {
            kdTree_.buildObstacleTree();
        }

        internal bool queryVisibility(Vector2 point1, Vector2 point2, Fix64 radius)
        {
            return kdTree_.queryVisibility(point1, point2, radius);
        }

        internal void setAgentDefaults(Fix64 neighborDist, int maxNeighbors, Fix64 timeHorizon, Fix64 timeHorizonObst, Fix64 radius, Fix64 maxSpeed)
        {
            if (defaultAgent_ == null)
            {
                defaultAgent_ = new Agent();
            }
            defaultAgent_.maxNeighbors_ = maxNeighbors;
            defaultAgent_.MaxSpeed = maxSpeed;
            defaultAgent_.neighborDist_ = neighborDist;
            defaultAgent_.radius_ = radius;
            defaultAgent_.timeHorizon_ = timeHorizon;
            defaultAgent_.timeHorizonObst_ = timeHorizonObst;
        }

        internal void setGlobalTime(Fix64 globalTime)
        {
            globalTime_ = globalTime;
        }

        internal void setTimeStep(Fix64 timeStep)
        {
            timeStep_ = timeStep;
        }

        private Simulator()
        {
            Clear();
        }
    }
}
