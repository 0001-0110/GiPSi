using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

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
    public Destination Destination;

    [Tooltip("All the nodes accessible from this one")]
    public List<Connection> Connections;

    // these attributes are used for A*
    /// <summary>g is the distance from the starting point</summary>
    [HideInInspector]
    public float CurrentCost;
    /// <summary>h is the heuristic distance from the destination</summary>
    [HideInInspector]
    public float EstimatedCost;
    /// <summary>f is the sum of g and h, giving the score of the node</summary>
    [HideInInspector]
    public float TotalEstimatedCost => CurrentCost + EstimatedCost;
    /// <summary></summary>
    [HideInInspector]
    public Connection PreviousConnection;

    public void OnDrawGizmos()
    {
        // TODO making multiple conditions would improve readability
        // If the node is connected to an empty connection or to itself, make it Red
        // If this node is not a destination, make it yellow
        // If it is, make it green
        Gizmos.color = Connections.Contains(null) || Connections.Any(connection => connection.Head == this) ? Color.red : Connections.Any(connection => connection.Sprite == null) ? Color.yellow : Destination == Destination.None ? Color.blue : Color.green;
        // If the destination is set to an invalid value, make the node red
        if (!Enum.IsDefined(typeof(Destination), Destination))
            Gizmos.color = Color.red;
        Gizmos.DrawSphere(Position, 0.5f);
        foreach (Connection connection in Connections)
        {
            // While working in the editor, Start hasn't been called yet, meaning that the connections are missing their tail (this node)
            connection.Tail = this;

            // Ignore connections that aren't set yet
            if (connection.Head != null)
            {
                // Red if the connection is only going in one direction
                // TODO Red if multiple similar connection exist
                // Blue if the connection is valid in both ways
                Gizmos.color = connection.Head.Connections.Any(connection => connection.Head == this) ? Color.blue : Color.red;
                Gizmos.DrawLine(Position, connection.Head.Position);

                // If there is no image on this connection, display a small red warning icon on the connection
                // This is probably not the best way to do it, but as long as it works...
                if (connection.Sprite == null)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 connectionCenter = new Vector3((connection.Tail.Position.x + connection.Head.Position.x) / 2, (connection.Tail.Position.y + connection.Head.Position.y) / 2);
                    Gizmos.DrawWireCube(connectionCenter, new Vector3(1, 1));
                    Gizmos.DrawCube(new Vector3(connectionCenter.x, connectionCenter.y + 0.1f), new Vector3(0.1f, 0.5f));
                    Gizmos.DrawCube(new Vector3(connectionCenter.x, connectionCenter.y - 0.25f), new Vector3(0.1f, 0.1f));
                    //Gizmos.DrawLine(connection.Tail.Position, connectionCenter);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, 0.75f);
    }

    public void Start()
    {
        if (Destination != Destination.None)
            if (!gameObject.name.EndsWith(Destination.ToString()))
                // This warning can be ignored if you know what you are doing
                Debug.LogWarning($"Warning: {this} name does not match its content, there may be an error");

        foreach (Connection connection in Connections)
        {
            connection.Tail = this;

            if (connection.Sprite == null)
                Debug.LogWarning($"Missing Sprite for the connection {connection}");
        }

        InitPathfinding();
    }

    public void InitPathfinding()
    {
        CurrentCost = 0;
        EstimatedCost = 0;
        PreviousConnection = null;
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
