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
        nodes = new List<Node>();
        destinations = new Dictionary<Destination, List<Node>>();
        foreach (Destination destination in Enum.GetValues(typeof(Destination)))
            destinations[destination] = new List<Node>();
        // put all navNodes in the list
        foreach (Node node in GetComponentsInChildren<Node>())
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

    public Stack<Node> GetPath(Node startingNode, Node destinationNode)
    {
        return GetPath(startingNode, destinationNode, (node1, node2) => node1.Distance(node2));
    }

    /// <summary>
    /// Find the shortest path between the two given nodes
    /// </summary>
    /// <param name="startingNode">The node where the path start</param>
    /// <param name="destinationNode">The node we are seeking a path to</param>
    /// <param name="heuristic">The function used to approximate the distance between two nodes</param>
    /// <returns>
    /// A list of Nodes, where the first element is the starting node, and the last is the destination node <br/>
    /// null if no path is found
    /// </returns>
    /// <exception cref="ArgumentNullException">if one of the given node is null</exception>
    public Stack<Node> GetPath(Node startingNode, Node destinationNode, Func<Node, Node, float> heuristic)
    {
        // TODO some names could be improved
        // TODO some more comments would be nice

        if (startingNode == null)
            throw new ArgumentNullException("ERROR - GetPath | startingNode is null");
        if (destinationNode == null)
            throw new ArgumentNullException("ERROR - GetPath | destinationNode is null");

        //Init all nodes to erase previous pathfinding values
        InitPathfinding();
        // The list of all visible nodes
        List<Node> open = new List<Node>();
        // The list of all nodes already visited
        List<Node> closed = new List<Node>();
        // We start at the starting node
        Node currentNode = startingNode;
        // Not necessary, but makes more sense
        open.Add(currentNode);
        currentNode.g = 0;
        //currentNode.h = currentNode.Distance(destinationNode);
        currentNode.h = heuristic(currentNode, destinationNode);
        currentNode.parent = null;
        while (currentNode != destinationNode)
        {
            foreach (Node node in currentNode.neighbours)
            {
                if (!closed.Contains(node) && !open.Contains(node))
                {
                    open.Add(node);
                    //float g = currentNode.g + node.Distance(currentNode);
                    float g = currentNode.g + heuristic(node, currentNode);
                    //float h = node.Distance(destinationNode);
                    float h = heuristic(node, destinationNode);
                    float f = g + h;
                    if (f < node.f || node.f == 0)
                    {
                        node.g = g;
                        node.h = h;
                        node.parent = currentNode;
                    }
                }
            }
            // We no longer need to check this node, it's now closed
            open.Remove(currentNode);
            closed.Add(currentNode);
            // the next node to explore is the one with the smallest f value
            if (open.Count == 0)
                // No path to the destination exists (return null)
                return null;
            else
                currentNode = ListService.Min(open, node => node.f);
        }
        // once the destination reached, recursive call to find the path
        return currentNode.Path();
    }

    /// <summary>
    /// Simplify the path by removing all intermediary steps that are not changing the path
    /// </summary>
    /// <param name="startingNode">The node where the path start</param>
    /// <param name="destinationNode">The node we are seeking a path to</param>
    /// <returns></returns>
    /// <remarks>This method really needs some comments</remarks>
    public Stack<Node> GetSimplifiedPath(Node startingNode, Node destinationNode)
    {
        Stack<Node> path = GetPath(startingNode, destinationNode);
        Stack<Node> simplifiedPath = new Stack<Node>();
        Node previousNode = path.Pop();
        simplifiedPath.Push(previousNode);
        while (path.Count > 1)
        {
            Node currentNode = path.Pop();
            //Debug.Log($"DEBUG - 22 | {previousNode} => {currentNode}\nDirection: {PathFindingService.GetDirection(previousNode, currentNode, path.Peek())}");
            if (PathFindingService.GetDirection(previousNode, currentNode, path.Peek()) != Direction.Forward)
                simplifiedPath.Push(currentNode);
            previousNode = currentNode;
        }
        simplifiedPath.Push(path.Pop());
        return simplifiedPath;
    }
}
