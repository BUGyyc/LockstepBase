// using System;
// using System.Collections.Generic;
// using BEPUutilities;
// using FixMath.NET;

// namespace Lockstep.Game.Features.Navigation.RVO.Algorithm
// {

//     internal class KdTree
//     {
//         private struct AgentTreeNode
//         {
//             internal int begin_;

//             internal int end_;

//             internal int left_;

//             internal int right_;

//             internal Fix64 maxX_;

//             internal Fix64 maxY_;

//             internal Fix64 minX_;

//             internal Fix64 minY_;
//         }

//         private struct Fix64Pair
//         {
//             private Fix64 a_;

//             private Fix64 b_;

//             internal Fix64Pair(Fix64 a, Fix64 b)
//             {
//                 a_ = a;
//                 b_ = b;
//             }

//             public static bool operator <(Fix64Pair pair1, Fix64Pair pair2)
//             {
//                 return pair1.a_ < pair2.a_ || (!(pair2.a_ < pair1.a_) && pair1.b_ < pair2.b_);
//             }

//             public static bool operator <=(Fix64Pair pair1, Fix64Pair pair2)
//             {
//                 return (pair1.a_ == pair2.a_ && pair1.b_ == pair2.b_) || pair1 < pair2;
//             }

//             public static bool operator >(Fix64Pair pair1, Fix64Pair pair2)
//             {
//                 return !(pair1 <= pair2);
//             }

//             public static bool operator >=(Fix64Pair pair1, Fix64Pair pair2)
//             {
//                 return !(pair1 < pair2);
//             }
//         }

//         private class ObstacleTreeNode
//         {
//             internal Obstacle obstacle_;

//             internal ObstacleTreeNode left_;

//             internal ObstacleTreeNode right_;
//         }

//         private const int MAX_LEAF_SIZE = 10;

//         private Agent[] agents_;

//         private AgentTreeNode[] agentTree_;

//         private ObstacleTreeNode obstacleTree_;

//         internal void buildAgentTree()
//         {
//             agents_ = new Agent[Simulator.Instance.agents_.Count];
//             uint num = 0u;
//             foreach (uint key in Simulator.Instance.agents_.Keys)
//             {
//                 agents_[num++] = Simulator.Instance.agents_[key];
//             }
//             agentTree_ = new AgentTreeNode[2 * agents_.Length];
//             for (int i = 0; i < agentTree_.Length; i++)
//             {
//                 agentTree_[i] = default(AgentTreeNode);
//             }
//             if (agents_.Length != 0)
//             {
//                 buildAgentTreeRecursive(0, agents_.Length, 0);
//             }
//         }

//         internal void buildObstacleTree()
//         {
//             obstacleTree_ = new ObstacleTreeNode();
//             IList<Obstacle> list = new List<Obstacle>(Simulator.Instance.obstacles_.Count);
//             for (int i = 0; i < Simulator.Instance.obstacles_.Count; i++)
//             {
//                 list.Add(Simulator.Instance.obstacles_[i]);
//             }
//             obstacleTree_ = buildObstacleTreeRecursive(list);
//         }

//         internal void computeAgentNeighbors(Agent agent, ref Fix64 rangeSq)
//         {
//             queryAgentTreeRecursive(agent, ref rangeSq, 0);
//         }

//         internal void computeObstacleNeighbors(Agent agent, Fix64 rangeSq)
//         {
//             queryObstacleTreeRecursive(agent, rangeSq, obstacleTree_);
//         }

//         internal bool queryVisibility(Vector2 q1, Vector2 q2, Fix64 radius)
//         {
//             return queryVisibilityRecursive(q1, q2, radius, obstacleTree_);
//         }

//         private void buildAgentTreeRecursive(int begin, int end, int node)
//         {
//             agentTree_[node].begin_ = begin;
//             agentTree_[node].end_ = end;
//             agentTree_[node].minX_ = (agentTree_[node].maxX_ = agents_[begin].Position.X);
//             agentTree_[node].minY_ = (agentTree_[node].maxY_ = agents_[begin].Position.Y);
//             for (int i = begin + 1; i < end; i++)
//             {
//                 agentTree_[node].maxX_ = RVOMath.Max(agentTree_[node].maxX_, agents_[i].Position.X);
//                 agentTree_[node].minX_ = RVOMath.Min(agentTree_[node].minX_, agents_[i].Position.X);
//                 agentTree_[node].maxY_ = RVOMath.Max(agentTree_[node].maxY_, agents_[i].Position.Y);
//                 agentTree_[node].minY_ = RVOMath.Min(agentTree_[node].minY_, agents_[i].Position.Y);
//             }
//             if (end - begin <= 10)
//             {
//                 return;
//             }
//             bool flag = agentTree_[node].maxX_ - agentTree_[node].minX_ > agentTree_[node].maxY_ - agentTree_[node].minY_;
//             Fix64 fix = 0.5m * (flag ? (agentTree_[node].maxX_ + agentTree_[node].minX_) : (agentTree_[node].maxY_ + agentTree_[node].minY_));
//             int j = begin;
//             int num = end;
//             while (j < num)
//             {
//                 for (; j < num && (flag ? agents_[j].Position.X : agents_[j].Position.Y) < fix; j++)
//                 {
//                 }
//                 while (num > j && (flag ? agents_[num - 1].Position.X : agents_[num - 1].Position.Y) >= fix)
//                 {
//                     num--;
//                 }
//                 if (j < num)
//                 {
//                     Agent agent = agents_[j];
//                     agents_[j] = agents_[num - 1];
//                     agents_[num - 1] = agent;
//                     j++;
//                     num--;
//                 }
//             }
//             int num2 = j - begin;
//             if (num2 == 0)
//             {
//                 num2++;
//                 j++;
//                 num++;
//             }
//             agentTree_[node].left_ = node + 1;
//             agentTree_[node].right_ = node + 2 * num2;
//             buildAgentTreeRecursive(begin, j, agentTree_[node].left_);
//             buildAgentTreeRecursive(j, end, agentTree_[node].right_);
//         }

//         private ObstacleTreeNode buildObstacleTreeRecursive(IList<Obstacle> obstacles)
//         {
//             if (obstacles.Count == 0)
//             {
//                 return null;
//             }
//             ObstacleTreeNode obstacleTreeNode = new ObstacleTreeNode();
//             int num = 0;
//             int num2 = obstacles.Count;
//             int num3 = obstacles.Count;
//             for (int i = 0; i < obstacles.Count; i++)
//             {
//                 int num4 = 0;
//                 int num5 = 0;
//                 Obstacle obstacle = obstacles[i];
//                 Obstacle next_ = obstacle.next_;
//                 for (int j = 0; j < obstacles.Count; j++)
//                 {
//                     if (i != j)
//                     {
//                         Obstacle obstacle2 = obstacles[j];
//                         Obstacle next_2 = obstacle2.next_;
//                         Fix64 fix = RVOMath.leftOf(obstacle.point_, next_.point_, obstacle2.point_);
//                         Fix64 fix2 = RVOMath.leftOf(obstacle.point_, next_.point_, next_2.point_);
//                         if (fix >= -RVOMath.RVO_EPSILON && fix2 >= -RVOMath.RVO_EPSILON)
//                         {
//                             num4++;
//                         }
//                         else if (fix <= RVOMath.RVO_EPSILON && fix2 <= RVOMath.RVO_EPSILON)
//                         {
//                             num5++;
//                         }
//                         else
//                         {
//                             num4++;
//                             num5++;
//                         }
//                         if (new Fix64Pair(Math.Max(num4, num5), Math.Min(num4, num5)) >= new Fix64Pair(Math.Max(num2, num3), Math.Min(num2, num3)))
//                         {
//                             break;
//                         }
//                     }
//                 }
//                 if (new Fix64Pair(Math.Max(num4, num5), Math.Min(num4, num5)) < new Fix64Pair(Math.Max(num2, num3), Math.Min(num2, num3)))
//                 {
//                     num2 = num4;
//                     num3 = num5;
//                     num = i;
//                 }
//             }
//             IList<Obstacle> list = new List<Obstacle>(num2);
//             for (int k = 0; k < num2; k++)
//             {
//                 list.Add(null);
//             }
//             IList<Obstacle> list2 = new List<Obstacle>(num3);
//             for (int l = 0; l < num3; l++)
//             {
//                 list2.Add(null);
//             }
//             int num6 = 0;
//             int num7 = 0;
//             int num8 = num;
//             Obstacle obstacle3 = obstacles[num8];
//             Obstacle next_3 = obstacle3.next_;
//             for (int m = 0; m < obstacles.Count; m++)
//             {
//                 if (num8 == m)
//                 {
//                     continue;
//                 }
//                 Obstacle obstacle4 = obstacles[m];
//                 Obstacle next_4 = obstacle4.next_;
//                 Fix64 fix3 = RVOMath.leftOf(obstacle3.point_, next_3.point_, obstacle4.point_);
//                 Fix64 fix4 = RVOMath.leftOf(obstacle3.point_, next_3.point_, next_4.point_);
//                 if (fix3 >= -RVOMath.RVO_EPSILON && fix4 >= -RVOMath.RVO_EPSILON)
//                 {
//                     list[num6++] = obstacles[m];
//                     continue;
//                 }
//                 if (fix3 <= RVOMath.RVO_EPSILON && fix4 <= RVOMath.RVO_EPSILON)
//                 {
//                     list2[num7++] = obstacles[m];
//                     continue;
//                 }
//                 Fix64 fix5 = RVOMath.det(next_3.point_ - obstacle3.point_, obstacle4.point_ - obstacle3.point_) / RVOMath.det(next_3.point_ - obstacle3.point_, obstacle4.point_ - next_4.point_);
//                 Vector2 point_ = obstacle4.point_ + fix5 * (next_4.point_ - obstacle4.point_);
//                 Obstacle obstacle5 = new Obstacle();
//                 obstacle5.point_ = point_;
//                 obstacle5.previous_ = obstacle4;
//                 obstacle5.next_ = next_4;
//                 obstacle5.convex_ = true;
//                 obstacle5.direction_ = obstacle4.direction_;
//                 obstacle5.id_ = Simulator.Instance.obstacles_.Count;
//                 Simulator.Instance.obstacles_.Add(obstacle5);
//                 obstacle4.next_ = obstacle5;
//                 next_4.previous_ = obstacle5;
//                 if (fix3 > Fix64.Zero)
//                 {
//                     list[num6++] = obstacle4;
//                     list2[num7++] = obstacle5;
//                 }
//                 else
//                 {
//                     list2[num7++] = obstacle4;
//                     list[num6++] = obstacle5;
//                 }
//             }
//             obstacleTreeNode.obstacle_ = obstacle3;
//             obstacleTreeNode.left_ = buildObstacleTreeRecursive(list);
//             obstacleTreeNode.right_ = buildObstacleTreeRecursive(list2);
//             return obstacleTreeNode;
//         }

//         private void queryAgentTreeRecursive(Agent agent, ref Fix64 rangeSq, int node)
//         {
//             if (agentTree_[node].end_ - agentTree_[node].begin_ <= 10)
//             {
//                 for (int i = agentTree_[node].begin_; i < agentTree_[node].end_; i++)
//                 {
//                     agent.insertAgentNeighbor(agents_[i], ref rangeSq);
//                 }
//                 return;
//             }
//             Fix64 fix = RVOMath.sqr(RVOMath.Max(Fix64.Zero, agentTree_[agentTree_[node].left_].minX_ - agent.Position.X)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agent.Position.X - agentTree_[agentTree_[node].left_].maxX_)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agentTree_[agentTree_[node].left_].minY_ - agent.Position.Y)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agent.Position.Y - agentTree_[agentTree_[node].left_].maxY_));
//             Fix64 fix2 = RVOMath.sqr(RVOMath.Max(Fix64.Zero, agentTree_[agentTree_[node].right_].minX_ - agent.Position.X)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agent.Position.X - agentTree_[agentTree_[node].right_].maxX_)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agentTree_[agentTree_[node].right_].minY_ - agent.Position.Y)) + RVOMath.sqr(RVOMath.Max(Fix64.Zero, agent.Position.Y - agentTree_[agentTree_[node].right_].maxY_));
//             if (fix < fix2)
//             {
//                 if (fix < rangeSq)
//                 {
//                     queryAgentTreeRecursive(agent, ref rangeSq, agentTree_[node].left_);
//                     if (fix2 < rangeSq)
//                     {
//                         queryAgentTreeRecursive(agent, ref rangeSq, agentTree_[node].right_);
//                     }
//                 }
//             }
//             else if (fix2 < rangeSq)
//             {
//                 queryAgentTreeRecursive(agent, ref rangeSq, agentTree_[node].right_);
//                 if (fix < rangeSq)
//                 {
//                     queryAgentTreeRecursive(agent, ref rangeSq, agentTree_[node].left_);
//                 }
//             }
//         }

//         private void queryObstacleTreeRecursive(Agent agent, Fix64 rangeSq, ObstacleTreeNode node)
//         {
//             if (node == null)
//             {
//                 return;
//             }
//             Obstacle obstacle_ = node.obstacle_;
//             Obstacle next_ = obstacle_.next_;
//             Fix64 fix = RVOMath.leftOf(obstacle_.point_, next_.point_, agent.Position);
//             queryObstacleTreeRecursive(agent, rangeSq, (fix >= Fix64.Zero) ? node.left_ : node.right_);
//             Fix64 fix2 = RVOMath.sqr(fix) / (next_.point_ - obstacle_.point_).LengthSquared();
//             if (fix2 < rangeSq)
//             {
//                 if (fix < Fix64.Zero)
//                 {
//                     agent.insertObstacleNeighbor(node.obstacle_, rangeSq);
//                 }
//                 queryObstacleTreeRecursive(agent, rangeSq, (fix >= Fix64.Zero) ? node.right_ : node.left_);
//             }
//         }

//         private bool queryVisibilityRecursive(Vector2 q1, Vector2 q2, Fix64 radius, ObstacleTreeNode node)
//         {
//             if (node == null)
//             {
//                 return true;
//             }
//             Obstacle obstacle_ = node.obstacle_;
//             Obstacle next_ = obstacle_.next_;
//             Fix64 fix = RVOMath.leftOf(obstacle_.point_, next_.point_, q1);
//             Fix64 fix2 = RVOMath.leftOf(obstacle_.point_, next_.point_, q2);
//             Fix64 fix3 = Fix64.One / (next_.point_ - obstacle_.point_).LengthSquared();
//             if (fix >= Fix64.Zero && fix2 >= Fix64.Zero)
//             {
//                 return queryVisibilityRecursive(q1, q2, radius, node.left_) && ((RVOMath.sqr(fix) * fix3 >= RVOMath.sqr(radius) && RVOMath.sqr(fix2) * fix3 >= RVOMath.sqr(radius)) || queryVisibilityRecursive(q1, q2, radius, node.right_));
//             }
//             if (fix <= Fix64.Zero && fix2 <= Fix64.Zero)
//             {
//                 return queryVisibilityRecursive(q1, q2, radius, node.right_) && ((RVOMath.sqr(fix) * fix3 >= RVOMath.sqr(radius) && RVOMath.sqr(fix2) * fix3 >= RVOMath.sqr(radius)) || queryVisibilityRecursive(q1, q2, radius, node.left_));
//             }
//             if (fix >= Fix64.Zero && fix2 <= Fix64.Zero)
//             {
//                 return queryVisibilityRecursive(q1, q2, radius, node.left_) && queryVisibilityRecursive(q1, q2, radius, node.right_);
//             }
//             Fix64 fix4 = RVOMath.leftOf(q1, q2, obstacle_.point_);
//             Fix64 fix5 = RVOMath.leftOf(q1, q2, next_.point_);
//             Fix64 fix6 = Fix64.One / (q2 - q1).LengthSquared();
//             return fix4 * fix5 >= Fix64.Zero && RVOMath.sqr(fix4) * fix6 > RVOMath.sqr(radius) && RVOMath.sqr(fix5) * fix6 > RVOMath.sqr(radius) && queryVisibilityRecursive(q1, q2, radius, node.left_) && queryVisibilityRecursive(q1, q2, radius, node.right_);
//         }
//     }
// }
