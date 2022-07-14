using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

public class NavigationScreenController : ScreenController
{
    public NavMesh navMesh;
    private Stack<Node> path;

    public Image BackgroundImage;
    public Image DirectionImage;
    public TextController DirectionText;
    public AudioSource DirectionAudioSource;

    private enum Direction
    {
        Right,
        Up,
        Left,
        Down,
        None,
    }

    /// <summary>
    /// The order of the spite is important, must be in the same order as the enum
    /// </summary>
    [NonReorderable]
    public Sprite[] Sprites;
    ////// <summary>
    /// The order of the audio clips is important, must be in the same order as the enum
    /// </summary>
    [NonReorderable]
    public AudioClip[] AudioClips;

    public void OnDrawGizmos()
    {
        if (path == null)
            return;
        foreach (Node node in path)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(node.Position, 0.75f);
        }
    }

    public override void Start()
    {
        Node startingNode = navMesh.GetNode("101");
        path = navMesh.GetPath(startingNode, navMesh.GetNode(startingNode, Destination.Room207));
        SetDirection(Direction.Left);
    }

    private void SetDirection(Direction direction)
    {
        DirectionImage.sprite = Sprites[(int)direction];
        DirectionText.SetText($"Direction_{direction}");
    }

    private Direction GetNextDirection()
    {
        // TODO
        return Direction.Up;
    }

    public void NextStep()
    {
        // TODO
        path.Pop();
        Direction nextDirection = GetNextDirection();
        DirectionAudioSource.PlayOneShot(AudioClips[(int)nextDirection]);
    }

    public void StartGuidance(string position, Destination destination)
    {
        Node startingNode = navMesh.GetNode(position);
        Node destinationNode = navMesh.GetNode(startingNode, destination);
        path = navMesh.GetPath(startingNode, destinationNode);
        NextStep();
    }
}
