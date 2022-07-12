using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class Node : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public string Name;
    public Destination Destination;
    [Tooltip("All the nodes accessible from this one")]
    public List<Node> neighbours;

    // these attributes are used for A*
    /// <summary>g is the distance from the starting point</summary>
    [HideInInspector]
    public float g;
    /// <summary>h is the heuristic distance from the destination</summary>
    [HideInInspector]
    public float h;
    /// <summary>f is the sum of g and h, giving the score of the node</summary>
    [HideInInspector]
    public float f => g + h;
    /// <summary>parent is the node that led to this one with the shortest path</summary>
    [HideInInspector]
    public Node parent;

    public void OnDrawGizmos()
    {
        Gizmos.color = neighbours.Contains(null) || neighbours.Contains(this) ? Color.red : Destination == Destination.None ? Color.blue : Color.green;
        Gizmos.DrawSphere(Position, 10);
        foreach (Node neighbour in neighbours)
        {
            Gizmos.color = neighbour.neighbours.Contains(this) ? Color.blue : Color.red;
            Gizmos.DrawLine(Position, neighbour.Position);
        }
    }

    public void Start()
    {
        if (Destination != Destination.None)
            if (gameObject.name != $"Node_{Destination}")
                Debug.LogWarning($"Warning: {this} name does not match its content, there may be an error");

        InitPathfinding();
    }

    public void InitPathfinding()
    {
        g = 0;
        h = 0;
        parent = null;
    }

    public float Distance(Vector3 position)
    {
        return Vector3.Distance(Position, position);
    }

    public float Distance(Node node)
    {
        return Vector3.Distance(Position, node.Position);
    }

    public Stack<Node> Path()
    {
        Stack<Node> path = (parent == null ? new Stack<Node>() : parent.Path());
        path.Push(this);
        return path;
    }
}
