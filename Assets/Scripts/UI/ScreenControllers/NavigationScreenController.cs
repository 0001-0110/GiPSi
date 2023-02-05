using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

// TODO modular screen controller
public class NavigationScreenController : ModularScreenController
{
    public NavMesh navMesh;
    private Queue<Connection> path;
    private Connection currentConnection;

    public Destination Destination { get; set; }

    public Image BackgroundImage;
    public Image DirectionImage;
    public TextController DirectionText;
    public AudioSource DirectionAudioSource;

    [Tooltip("The order of the sprites is important: must be in the same order as the directions. Must end with the one to display when the destination is reached")]
    [NonReorderable]
    public Sprite[] Sprites;
    [Tooltip("The order of the audios is important: must be in the same order as the directions. Must end with the one to play when the destination is reached")]
    [NonReorderable]
    public AudioClip[] AudioClips;

    public void OnDrawGizmos()
    {
        if (path == null)
            return;
        foreach (Connection connection in path)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(connection.Tail.Position, connection.Head.Position);
        }
        // TODO obsolete code, must redo with the conenctions
        /*if (currentConnection.Tail != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentNode.Position, 0.75f);
        }
        if (path.Count > 0)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(path.Peek().Position, 0.75f);
        }*/
    }

    private void NextStep()
    {
        if (path.Count <= 0)
        {
            // Destination reached
            DirectionImage.sprite = Sprites[Sprites.Length - 1];
            DirectionText.SetText("{Direction_DestinationReached}");
            SetMode(ScreenMode.DestinationReached);
            DirectionAudioSource.PlayOneShot(AudioClips[AudioClips.Length - 1]);
        }
        else
        {
            // This line is sus
            Direction nextDirection = PathFindingService.GetDirection(currentConnection, currentConnection = path.Dequeue());
            BackgroundImage.sprite = currentConnection.Sprite;
            DirectionImage.sprite = Sprites[(int)nextDirection];
            DirectionText.SetText($"{{Direction_{nextDirection}}}");
            DirectionAudioSource.PlayOneShot(AudioClips[(int)nextDirection]);
        }
    }

    public bool StartNavigation(Node startingNode, Node destinationNode)
    {
        Debug.Log($"DEBUG: NAVIGATION - going from {startingNode} to {destinationNode}");
        SetMode(ScreenMode.Navigating);

        path = navMesh.GetPath(startingNode, destinationNode);
        // Set the current connection
        currentConnection = path.Dequeue();
        NextStep();

        return true;
    }
}
