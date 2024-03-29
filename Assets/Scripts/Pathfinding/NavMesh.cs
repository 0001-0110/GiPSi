using System;
using System.Collections.Generic;
using UnityEngine;

using Services;
using Pathfinding;

public class NavMesh : MonoBehaviour
{
    private List<Node> nodes;

    //public DestinationNode[] Destinations;

    private Dictionary<Destination, List<Node>> destinations;

    public void Start()
    {
        foreach (Transform floor in transform)
            if (!floor.gameObject.activeSelf)
                Debug.LogWarning($"{floor.name} is inactive, this might cause pathfinding issues");

        nodes = new List<Node>();
        destinations = new Dictionary<Destination, List<Node>>();
        foreach (Destination destination in Enum.GetValues(typeof(Destination)))
            destinations[destination] = new List<Node>();
        // Put all active navNodes in the list
        // includeInactive is only here for the sake of clarity
        foreach (Node node in GetComponentsInChildren<Node>(includeInactive:false))
        {
            nodes.Add(node);
            destinations[node.Destination].Add(node);
        }
    }

    public void InitPathfinding()
    {
        foreach (Node node in nodes)
            node.InitPathfinding();
    }

    public Node GetNode(string nodeName)
    {
        return nodes.Find(node => node.Name == nodeName);
    }

    /// <summary>
    /// get the node the has the correct destination and closest from the given node
    /// </summary>
    /// <param name="startingNode"></param>
    /// <param name="destination"></param>
    /// <returns>null if no node is going to the right destination</returns>
    public Node GetNode(Node startingNode, Destination destination)
    {
        if (startingNode == null || destinations[destination].Count == 0)
            return null;
        return ListService.Min(destinations[destination], node => node.Distance(startingNode));
    }

    public Queue<Connection> GetPath(Node startNode, Node endNode, Func<Node, Node, float> heuristic)
    {
        if (startNode == null)
            throw new ArgumentNullException("ERROR - GetPath | startNode is null");
        if (endNode == null)
            throw new ArgumentNullException("ERROR - GetPath | endNode is null");

        // Init all nodes to erase previous pathfinding values
        InitPathfinding();
        // The list of all visible nodes
        List<Node> open = new List<Node>();
        // The list of all nodes already visited
        List<Node> closed = new List<Node>();
        // We start at the starting node
        Node currentNode = startNode;
        // Not necessary, but makes more sense
        open.Add(currentNode);
        currentNode.CurrentCost = 0;
        //currentNode.h = currentNode.Distance(destinationNode);
        currentNode.EstimatedCost = heuristic(currentNode, endNode);
        currentNode.PreviousConnection = null;
        while (currentNode != endNode)
        {
            foreach (Connection connection in currentNode.Connections)
            {
                if (!closed.Contains(connection.Head) && !open.Contains(connection.Head))
                {
                    open.Add(connection.Head);
                    //float g = currentNode.g + node.Distance(currentNode);
                    float newCurrentCost = currentNode.CurrentCost + heuristic(connection.Tail, connection.Head);
                    float newEstimatedCost = heuristic(connection.Tail, endNode);
                    float newTotalEstimatedCost = newCurrentCost + newEstimatedCost;
                    if (newTotalEstimatedCost < connection.Head.TotalEstimatedCost || connection.Head.TotalEstimatedCost == 0)
                    {
                        connection.Head.CurrentCost = newCurrentCost;
                        connection.Head.EstimatedCost = newEstimatedCost;
                        // If this node is the shortest path that led here, set the previous connection
                        connection.Head.PreviousConnection = connection;
                    }
                }
            }
            // We no longer need to check this node, it's now closed
            open.Remove(currentNode);
            closed.Add(currentNode);
            // the next node to explore is the one with the smallest f value
            if (open.Count == 0)
            {
                // No path to the destination exists (return null)
                Debug.Log("No path to the destination exists, make sure that all nodes are either connected or disabled");
                return null;
            }
            else
                currentNode = ListService.Min(open, node => node.TotalEstimatedCost);
        }
        // Once the destination reached, recursive call to find the path
        return currentNode.Path();
    }

    public Queue<Connection> GetPath(Node startingNode, Node destinationNode)
    {
        return GetPath(startingNode, destinationNode, (node1, node2) => node1.Distance(node2));
    }
}
