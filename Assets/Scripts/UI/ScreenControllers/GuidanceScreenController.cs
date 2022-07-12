using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

public class GuidanceScreenController : ScreenController
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

    public override void Start()
    {
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
