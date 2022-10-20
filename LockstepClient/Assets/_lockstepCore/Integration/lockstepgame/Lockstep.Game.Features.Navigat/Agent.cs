using System.Collections.Generic;
using BEPUutilities;
using FixMath.NET;

namespace Lockstep.Game.Features.Navigation.RVO.Algorithm
{

    internal class Agent
    {
        internal readonly IList<KeyValuePair<Fix64, Agent>> AgentNeighbors = new List<KeyValuePair<Fix64, Agent>>();

        internal readonly IList<KeyValuePair<Fix64, Obstacle>> ObstacleNeighbors = new List<KeyValuePair<Fix64, Obstacle>>();

        internal readonly IList<Line> OrcaLines = new List<Line>();

        internal Vector2 Position;

        internal Vector2 PrefVelocity;

        internal Vector2 Destination;

        internal Vector2 Velocity;

        internal int maxNeighbors_ = 0;

        internal Fix64 MaxSpeed = Fix64.Zero;

        internal Fix64 neighborDist_ = Fix64.Zero;

        internal Fix64 radius_ = Fix64.Zero;

        internal Fix64 timeHorizon_ = Fix64.Zero;

        internal Fix64 timeHorizonObst_ = Fix64.Zero;

        private Vector2 newVelocity_;

        internal void CalculatePrefVelocity()
        {
            Vector2 vector = Destination - Position;
            if (vector.LengthSquared() > Fix64.One)
            {
                vector = Vector2.Normalize(vector);
            }
            PrefVelocity = vector;
        }

        internal void computeNeighbors()
        {
            ObstacleNeighbors.Clear();
            Fix64 rangeSq = RVOMath.sqr(timeHorizonObst_ * MaxSpeed + radius_);
            Simulator.Instance.kdTree_.computeObstacleNeighbors(this, rangeSq);
            AgentNeighbors.Clear();
            if (maxNeighbors_ > 0)
            {
                rangeSq = RVOMath.sqr(neighborDist_);
                Simulator.Instance.kdTree_.computeAgentNeighbors(this, ref rangeSq);
            }
        }

        internal void computeNewVelocity()
        {
            OrcaLines.Clear();
            Fix64 fix = Fix64.One / timeHorizonObst_;
            Line item = default(Line);
            for (int i = 0; i < ObstacleNeighbors.Count; i++)
            {
                Obstacle obstacle = ObstacleNeighbors[i].Value;
                Obstacle obstacle2 = obstacle.next_;
                Vector2 vector = obstacle.point_ - Position;
                Vector2 vector2 = obstacle2.point_ - Position;
                bool flag = false;
                for (int j = 0; j < OrcaLines.Count; j++)
                {
                    if (RVOMath.det(fix * vector - OrcaLines[j].point, OrcaLines[j].direction) - fix * radius_ >= -RVOMath.RVO_EPSILON && RVOMath.det(fix * vector2 - OrcaLines[j].point, OrcaLines[j].direction) - fix * radius_ >= -RVOMath.RVO_EPSILON)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }
                Fix64 fix2 = vector.LengthSquared();
                Fix64 fix3 = vector2.LengthSquared();
                Fix64 fix4 = RVOMath.sqr(radius_);
                Vector2 vector3 = obstacle2.point_ - obstacle.point_;
                Fix64 fix5 = Vector2.Dot(-vector, vector3) / vector3.LengthSquared();
                Fix64 fix6 = (-vector - fix5 * vector3).LengthSquared();
                if (fix5 < Fix64.Zero && fix2 <= fix4)
                {
                    if (obstacle.convex_)
                    {
                        item.point = new Vector2(Fix64.Zero, Fix64.Zero);
                        item.direction = Vector2.Normalize(new Vector2(-vector.Y, vector.X));
                        OrcaLines.Add(item);
                    }
                    continue;
                }
                if (fix5 > Fix64.One && fix3 <= fix4)
                {
                    if (obstacle2.convex_ && RVOMath.det(vector2, obstacle2.direction_) >= Fix64.Zero)
                    {
                        item.point = new Vector2(Fix64.Zero, Fix64.Zero);
                        item.direction = Vector2.Normalize(new Vector2(-vector2.Y, vector2.X));
                        OrcaLines.Add(item);
                    }
                    continue;
                }
                if (fix5 >= Fix64.Zero && fix5 < Fix64.One && fix6 <= fix4)
                {
                    item.point = new Vector2(Fix64.Zero, Fix64.Zero);
                    item.direction = -obstacle.direction_;
                    OrcaLines.Add(item);
                    continue;
                }
                Vector2 vector4;
                Vector2 vector5;
                if (fix5 < Fix64.Zero && fix6 <= fix4)
                {
                    if (!obstacle.convex_)
                    {
                        continue;
                    }
                    obstacle2 = obstacle;
                    Fix64 fix7 = Fix64.Sqrt(fix2 - fix4);
                    vector4 = new Vector2(vector.X * fix7 - vector.Y * radius_, vector.X * radius_ + vector.Y * fix7) / fix2;
                    vector5 = new Vector2(vector.X * fix7 + vector.Y * radius_, -vector.X * radius_ + vector.Y * fix7) / fix2;
                }
                else if (fix5 > Fix64.One && fix6 <= fix4)
                {
                    if (!obstacle2.convex_)
                    {
                        continue;
                    }
                    obstacle = obstacle2;
                    Fix64 fix8 = Fix64.Sqrt(fix3 - fix4);
                    vector4 = new Vector2(vector2.X * fix8 - vector2.Y * radius_, vector2.X * radius_ + vector2.Y * fix8) / fix3;
                    vector5 = new Vector2(vector2.X * fix8 + vector2.Y * radius_, -vector2.X * radius_ + vector2.Y * fix8) / fix3;
                }
                else
                {
                    if (obstacle.convex_)
                    {
                        Fix64 fix9 = Fix64.Sqrt(fix2 - fix4);
                        vector4 = new Vector2(vector.X * fix9 - vector.Y * radius_, vector.X * radius_ + vector.Y * fix9) / fix2;
                    }
                    else
                    {
                        vector4 = -obstacle.direction_;
                    }
                    if (obstacle2.convex_)
                    {
                        Fix64 fix10 = Fix64.Sqrt(fix3 - fix4);
                        vector5 = new Vector2(vector2.X * fix10 + vector2.Y * radius_, -vector2.X * radius_ + vector2.Y * fix10) / fix3;
                    }
                    else
                    {
                        vector5 = obstacle.direction_;
                    }
                }
                Obstacle previous_ = obstacle.previous_;
                bool flag2 = false;
                bool flag3 = false;
                if (obstacle.convex_ && RVOMath.det(vector4, -previous_.direction_) >= Fix64.Zero)
                {
                    vector4 = -previous_.direction_;
                    flag2 = true;
                }
                if (obstacle2.convex_ && RVOMath.det(vector5, obstacle2.direction_) <= Fix64.Zero)
                {
                    vector5 = obstacle2.direction_;
                    flag3 = true;
                }
                Vector2 vector6 = fix * (obstacle.point_ - Position);
                Vector2 vector7 = fix * (obstacle2.point_ - Position);
                Vector2 vector8 = vector7 - vector6;
                Fix64 fix11 = ((obstacle == obstacle2) ? ((Fix64)0.5m) : (Vector2.Dot(Velocity - vector6, vector8) / vector8.LengthSquared()));
                Fix64 fix12 = Vector2.Dot(Velocity - vector6, vector4);
                Fix64 fix13 = Vector2.Dot(Velocity - vector7, vector5);
                if ((fix11 < Fix64.Zero && fix12 < Fix64.Zero) || (obstacle == obstacle2 && fix12 < Fix64.Zero && fix13 < Fix64.Zero))
                {
                    Vector2 vector9 = Vector2.Normalize(Velocity - vector6);
                    item.direction = new Vector2(vector9.Y, -vector9.X);
                    item.point = vector6 + radius_ * fix * vector9;
                    OrcaLines.Add(item);
                    continue;
                }
                if (fix11 > Fix64.One && fix13 < Fix64.Zero)
                {
                    Vector2 vector10 = Vector2.Normalize(Velocity - vector7);
                    item.direction = new Vector2(vector10.Y, -vector10.X);
                    item.point = vector7 + radius_ * fix * vector10;
                    OrcaLines.Add(item);
                    continue;
                }
                Fix64 fix14 = ((fix11 < Fix64.Zero || fix11 > Fix64.One || obstacle == obstacle2) ? Fix64.MaxValue : (Velocity - (vector6 + fix11 * vector8)).LengthSquared());
                Fix64 fix15 = ((fix12 < Fix64.Zero) ? Fix64.MaxValue : (Velocity - (vector6 + fix12 * vector4)).LengthSquared());
                Fix64 fix16 = ((fix13 < Fix64.Zero) ? Fix64.MaxValue : (Velocity - (vector7 + fix13 * vector5)).LengthSquared());
                if (fix14 <= fix15 && fix14 <= fix16)
                {
                    item.direction = -obstacle.direction_;
                    item.point = vector6 + radius_ * fix * new Vector2(-item.direction.Y, item.direction.X);
                    OrcaLines.Add(item);
                }
                else if (fix15 <= fix16)
                {
                    if (!flag2)
                    {
                        item.direction = vector4;
                        item.point = vector6 + radius_ * fix * new Vector2(-item.direction.Y, item.direction.X);
                        OrcaLines.Add(item);
                    }
                }
                else if (!flag3)
                {
                    item.direction = -vector5;
                    item.point = vector7 + radius_ * fix * new Vector2(-item.direction.Y, item.direction.X);
                    OrcaLines.Add(item);
                }
            }
            int count = OrcaLines.Count;
            Fix64 fix17 = Fix64.One / timeHorizon_;
            Line item2 = default(Line);
            for (int k = 0; k < AgentNeighbors.Count; k++)
            {
                Agent value = AgentNeighbors[k].Value;
                Vector2 vector11 = value.Position - Position;
                Vector2 vector12 = Velocity - value.Velocity;
                Fix64 fix18 = vector11.LengthSquared();
                Fix64 fix19 = radius_ + value.radius_;
                Fix64 fix20 = RVOMath.sqr(fix19);
                Vector2 vector15;
                if (fix18 > fix20)
                {
                    Vector2 vector13 = vector12 - fix17 * vector11;
                    Fix64 fix21 = vector13.LengthSquared();
                    Fix64 fix22 = Vector2.Dot(vector13, vector11);
                    if (fix22 < Fix64.Zero && RVOMath.sqr(fix22) > fix20 * fix21)
                    {
                        Fix64 fix23 = Fix64.Sqrt(fix21);
                        Vector2 vector14 = vector13 / fix23;
                        item2.direction = new Vector2(vector14.Y, -vector14.X);
                        vector15 = (fix19 * fix17 - fix23) * vector14;
                    }
                    else
                    {
                        Fix64 fix24 = Fix64.Sqrt(fix18 - fix20);
                        if (RVOMath.det(vector11, vector13) > Fix64.Zero)
                        {
                            item2.direction = new Vector2(vector11.X * fix24 - vector11.Y * fix19, vector11.X * fix19 + vector11.Y * fix24) / fix18;
                        }
                        else
                        {
                            item2.direction = -new Vector2(vector11.X * fix24 + vector11.Y * fix19, -vector11.X * fix19 + vector11.Y * fix24) / fix18;
                        }
                        Fix64 fix25 = Vector2.Dot(vector12, item2.direction);
                        vector15 = fix25 * item2.direction - vector12;
                    }
                }
                else
                {
                    Fix64 fix26 = Fix64.One / Simulator.Instance.timeStep_;
                    Vector2 vector16 = vector12 - fix26 * vector11;
                    Fix64 fix27 = vector16.Length();
                    Vector2 vector17 = vector16 / fix27;
                    item2.direction = new Vector2(vector17.Y, -vector17.X);
                    vector15 = (fix19 * fix26 - fix27) * vector17;
                }
                item2.point = Velocity + 0.5m * vector15;
                OrcaLines.Add(item2);
            }
            int num = linearProgram2(OrcaLines, MaxSpeed, PrefVelocity, directionOpt: false, ref newVelocity_);
            if (num < OrcaLines.Count)
            {
                linearProgram3(OrcaLines, count, num, MaxSpeed, ref newVelocity_);
            }
        }

        internal void insertAgentNeighbor(Agent agent, ref Fix64 rangeSq)
        {
            if (this == agent)
            {
                return;
            }
            Fix64 fix = (Position - agent.Position).LengthSquared();
            if (fix < rangeSq)
            {
                if (AgentNeighbors.Count < maxNeighbors_)
                {
                    AgentNeighbors.Add(new KeyValuePair<Fix64, Agent>(fix, agent));
                }
                int num = AgentNeighbors.Count - 1;
                while (num != 0 && fix < AgentNeighbors[num - 1].Key)
                {
                    AgentNeighbors[num] = AgentNeighbors[num - 1];
                    num--;
                }
                AgentNeighbors[num] = new KeyValuePair<Fix64, Agent>(fix, agent);
                if (AgentNeighbors.Count == maxNeighbors_)
                {
                    rangeSq = AgentNeighbors[AgentNeighbors.Count - 1].Key;
                }
            }
        }

        internal void insertObstacleNeighbor(Obstacle obstacle, Fix64 rangeSq)
        {
            Obstacle next_ = obstacle.next_;
            Fix64 fix = RVOMath.DistSqPointLineSegment(obstacle.point_, next_.point_, Position);
            if (fix < rangeSq)
            {
                ObstacleNeighbors.Add(new KeyValuePair<Fix64, Obstacle>(fix, obstacle));
                int num = ObstacleNeighbors.Count - 1;
                while (num != 0 && fix < ObstacleNeighbors[num - 1].Key)
                {
                    ObstacleNeighbors[num] = ObstacleNeighbors[num - 1];
                    num--;
                }
                ObstacleNeighbors[num] = new KeyValuePair<Fix64, Obstacle>(fix, obstacle);
            }
        }

        internal void update()
        {
            Velocity = newVelocity_;
        }

        private bool linearProgram1(IList<Line> lines, int lineNo, Fix64 radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
        {
            Fix64 fix = Vector2.Dot(lines[lineNo].point, lines[lineNo].direction);
            Fix64 fix2 = RVOMath.sqr(fix) + RVOMath.sqr(radius) - lines[lineNo].point.LengthSquared();
            if (fix2 < Fix64.Zero)
            {
                return false;
            }
            Fix64 fix3 = Fix64.Sqrt(fix2);
            Fix64 fix4 = -fix - fix3;
            Fix64 fix5 = -fix + fix3;
            for (int i = 0; i < lineNo; i++)
            {
                Fix64 fix6 = RVOMath.det(lines[lineNo].direction, lines[i].direction);
                Fix64 fix7 = RVOMath.det(lines[i].direction, lines[lineNo].point - lines[i].point);
                if (Fix64.Abs(fix6) <= RVOMath.RVO_EPSILON)
                {
                    if (fix7 < Fix64.Zero)
                    {
                        return false;
                    }
                    continue;
                }
                Fix64 b = fix7 / fix6;
                if (fix6 >= Fix64.Zero)
                {
                    fix5 = RVOMath.Min(fix5, b);
                }
                else
                {
                    fix4 = RVOMath.Max(fix4, b);
                }
                if (fix4 > fix5)
                {
                    return false;
                }
            }
            if (directionOpt)
            {
                if (Vector2.Dot(optVelocity, lines[lineNo].direction) > Fix64.Zero)
                {
                    result = lines[lineNo].point + fix5 * lines[lineNo].direction;
                }
                else
                {
                    result = lines[lineNo].point + fix4 * lines[lineNo].direction;
                }
            }
            else
            {
                Fix64 fix8 = Vector2.Dot(lines[lineNo].direction, optVelocity - lines[lineNo].point);
                if (fix8 < fix4)
                {
                    result = lines[lineNo].point + fix4 * lines[lineNo].direction;
                }
                else if (fix8 > fix5)
                {
                    result = lines[lineNo].point + fix5 * lines[lineNo].direction;
                }
                else
                {
                    result = lines[lineNo].point + fix8 * lines[lineNo].direction;
                }
            }
            return true;
        }

        private int linearProgram2(IList<Line> lines, Fix64 radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
        {
            if (directionOpt)
            {
                result = optVelocity * radius;
            }
            else if (optVelocity.LengthSquared() > RVOMath.sqr(radius))
            {
                result = Vector2.Normalize(optVelocity) * radius;
            }
            else
            {
                result = optVelocity;
            }
            for (int i = 0; i < lines.Count; i++)
            {
                if (RVOMath.det(lines[i].direction, lines[i].point - result) > Fix64.Zero)
                {
                    Vector2 vector = result;
                    if (!linearProgram1(lines, i, radius, optVelocity, directionOpt, ref result))
                    {
                        result = vector;
                        return i;
                    }
                }
            }
            return lines.Count;
        }

        private void linearProgram3(IList<Line> lines, int numObstLines, int beginLine, Fix64 radius, ref Vector2 result)
        {
            Fix64 fix = Fix64.Zero;
            Line item = default(Line);
            for (int i = beginLine; i < lines.Count; i++)
            {
                if (!(RVOMath.det(lines[i].direction, lines[i].point - result) > fix))
                {
                    continue;
                }
                IList<Line> list = new List<Line>();
                for (int j = 0; j < numObstLines; j++)
                {
                    list.Add(lines[j]);
                }
                for (int k = numObstLines; k < i; k++)
                {
                    Fix64 fix2 = RVOMath.det(lines[i].direction, lines[k].direction);
                    if (Fix64.Abs(fix2) <= RVOMath.RVO_EPSILON)
                    {
                        if (Vector2.Dot(lines[i].direction, lines[k].direction) > Fix64.Zero)
                        {
                            continue;
                        }
                        item.point = 0.5m * (lines[i].point + lines[k].point);
                    }
                    else
                    {
                        item.point = lines[i].point + RVOMath.det(lines[k].direction, lines[i].point - lines[k].point) / fix2 * lines[i].direction;
                    }
                    item.direction = Vector2.Normalize(lines[k].direction - lines[i].direction);
                    list.Add(item);
                }
                Vector2 vector = result;
                if (linearProgram2(list, radius, new Vector2(-lines[i].direction.Y, lines[i].direction.X), directionOpt: true, ref result) < list.Count)
                {
                    result = vector;
                }
                fix = RVOMath.det(lines[i].direction, lines[i].point - result);
            }
        }
    }

}
