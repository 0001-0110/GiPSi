using System;
using UnityEngine;

namespace Pathfinding
{
    public enum Destination
    {
        // TODO might not be the smartest way to number these
        // Don't forget to change the values in unity if you change them here
        None = 0,
        Cafeteria = -1,
        CPE = -2,
        Infirmary = -3,
        Library = -4,
        Toilets = -5,
        Room001 = 1,
        Room002,
        Room101 = 101,
        Room102,
        Room103,
        Room104,
        Room105,
        Room106,
        Room107,
        Room108,
        Room109,
        Room110,
        Room111,
        Room112,
        Room113,
        Room114,
        Room201 = 201,
        Room202,
        Room203,
        Room204,
        Room205,
        Room206,
        Room207,
        Room208,
        Room209,
        Room210,
        Room211,
        Room212,
        Room213,
        Room214,
    }

    public enum Direction
    {
        Right,
        Forward,
        Left,
        Backward,
        Up,
        Down,
    }

    public static class PathFindingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <returns></returns>
        private static Vector3 GetVector(Node fromNode, Node toNode)
        {
            return toNode.Position - fromNode.Position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns>The angle between the two vectors, in degree</returns>
        private static float GetAngle(Vector3 vector1, Vector3 vector2)
        {
            return Vector2.SignedAngle(vector1, vector2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        private static bool IsAlmostEqual(float value, float target, float margin = 45)
        {
            return value <= target + margin && value >= target - margin;
        }

        /// <summary>
        /// UNUSED
        /// </summary>
        /// <param name="previousNode"></param>
        /// <param name="currentNode"></param>
        /// <param name="nextNode"></param>
        /// <returns></returns>
        public static Direction GetDirection(Node previousNode, Node currentNode, Node nextNode)
        {
            if (nextNode.Position.z > currentNode.Position.z)
                return Direction.Up;
            else if (nextNode.Position.z < currentNode.Position.z)
                return Direction.Down;
            else
            {
                // the angle between the two vectors, in degree
                float angle = GetAngle(GetVector(previousNode, currentNode), GetVector(currentNode, nextNode));
                return
                    IsAlmostEqual(angle, 0) ? Direction.Forward :
                    IsAlmostEqual(angle, 90) ? Direction.Left :
                    IsAlmostEqual(angle, -90) ? Direction.Right :
                    IsAlmostEqual(angle, 180) || IsAlmostEqual(angle, -180) ? Direction.Backward :
                    throw new Exception("And then she cartwheel-ed away");
            }
        }

        public static Direction GetDirection(Connection previousConnection, Connection nextConnection)
        {
            // If the next node is above the current one
            if (nextConnection.Head.Position.z > previousConnection.Head.Position.z)
                return Direction.Up;
            // If the next node is below the current one
            else if (nextConnection.Head.Position.z < previousConnection.Head.Position.z)
                return Direction.Down;
            else
            {
                // the angle between the two vectors, in degree
                float angle = GetAngle(GetVector(previousConnection.Tail, previousConnection.Head), GetVector(nextConnection.Tail, nextConnection.Head));
                return
                    IsAlmostEqual(angle, 0) ? Direction.Forward :
                    IsAlmostEqual(angle, 90) ? Direction.Left :
                    IsAlmostEqual(angle, -90) ? Direction.Right :
                    IsAlmostEqual(angle, 180) || IsAlmostEqual(angle, -180) ? Direction.Backward :
                    // If you ever end up getting this error, good luck
                    throw new Exception("And then she cartwheel-ed away");
            }
        }
    }
}
