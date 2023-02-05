using System;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using System.Linq;

[Serializable]
public class Connection
{
    [HideInInspector]
    /// <summary>The node this arc is coming from</summary>
    public Node Tail;
    /// <summary>The node this arc is going to</summary>
    public Node Head;
    public Sprite Sprite;

    public override string ToString()
    {
        return $"{Tail} => {Head}";
    }
}

public class Node : MonoBehaviour
{
    public Vector3 Position => transform.position;

    public string Name;
    public Sprite Sprite;
    public Destination Destination;

    [SerializeField]
    [Tooltip("OBSOLETE")]
    private List<Node> neighbours;

    [Tooltip("All the nodes accessible from this one")]
    public List<Connection> Connections;

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
    /// <summary></summary>
    [HideInInspector]
    public Connection PreviousConnection;

    public void OnDrawGizmos()
    {
        // If the node is connected to an empty connection or to itself, make it Red
        // If this node is not a destination, make it yellow
        // If it is, make it green
        Gizmos.color = Connections.Contains(null) || Connections.Any(connection => connection.Head == this) ? Color.red : Destination == Destination.None ? Color.yellow : Color.green;
        Gizmos.DrawSphere(Position, 0.5f);
        foreach (Connection connection in Connections)
        {
            if (connection != null)
            {
                // Red if the connection is only going in one direction
                // Blue if the connection is valid in both ways
                Gizmos.color = connection.Head.Connections.Any(connection => connection.Head == this) ? Color.blue : Color.red;
                Gizmos.DrawLine(Position, connection.Head.Position);
            }
        }
    }

    public void Start()
    {
        if (Destination != Destination.None)
            if (gameObject.name != $"Node_{Destination}")
                Debug.LogWarning($"Warning: {this} name does not match its content, there may be an error");

        foreach (Connection connection in Connections)
            connection.Tail = this;

        InitPathfinding();
    }

    public void InitPathfinding()
    {
        g = 0;
        h = 0;
        PreviousConnection = null;
    }

    public IEnumerable<Node> GetNeighbours()
    {
        // TODO
        foreach (Node node in neighbours)
            yield return node;

        /*foreach (Connection connection in connections)
            yield return connection.Node;*/
    }

    public float Distance(Vector3 position)
    {
        return Vector3.Distance(Position, position);
    }

    public float Distance(Node node)
    {
        return Vector3.Distance(Position, node.Position);
    }

    public Queue<Connection> Path()
    {
        // If the node isd the first of the path, return an empty queue
        if (PreviousConnection == null)
            return new Queue<Connection>();

        // Else, add the connection from the previous node to this one in the queue
        Queue<Connection> path = PreviousConnection.Tail.Path();
        path.Enqueue(PreviousConnection);
        return path;
    }
}
