using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

// TODO modular screen controller
public class NavigationScreenController : ModularScreenController
{
    public NavMesh navMesh;
    private Stack<Node> path;
    private Node currentNode;

    public Image BackgroundImage;
    public Image DirectionImage;
    public TextController DirectionText;
    public AudioSource DirectionAudioSource;

    [Tooltip("The order of the sprite is important: must be in the same order as the directions, and end with the one to display when the destination is reached")]
    [NonReorderable]
    public Sprite[] Sprites;
    [Tooltip("The order of the audios is important: must be in the same order as the directions, and end with the one to play when the destination is reached")]
    [NonReorderable]
    public AudioClip[] AudioClips;

    public void OnDrawGizmos()
    {
        if (path == null)
            return;
        if (currentNode != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentNode.Position, 0.75f);
        }
        foreach (Node node in path)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(node.Position, 0.75f);
        }
    }

    public void NextStep()
    {
        if (path.Count < 2)
        {
            // Destination reached
            DirectionImage.sprite = Sprites[Sprites.Length - 1];
            DirectionText.SetText("Direction_DestinationReached");
            SetMode("DestinationReached");
        }
        else
        {
            // This line is sus
            Direction nextDirection = PathFindingService.GetDirection(currentNode, currentNode = path.Pop(), path.Peek());
            BackgroundImage.sprite = currentNode.Sprite;
            DirectionImage.sprite = Sprites[(int)nextDirection];
            DirectionText.SetText($"Direction_{nextDirection}");
            //DirectionAudioSource.PlayOneShot(AudioClips[(int)nextDirection]);
        }
    }

    public bool StartNavigation(Node startingNode, Node destinationNode)
    {
        Debug.Log($"DEBUG: NAVIGATION - goint from {startingNode} to {destinationNode}");
        SetMode("Navigating");
        path = navMesh.GetSimplifiedPath(startingNode, destinationNode);
        currentNode = path.Pop();
        NextStep();
        return true;
    }
}
