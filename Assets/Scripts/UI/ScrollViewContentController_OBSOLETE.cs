using System.Collections.Generic;
using UnityEngine;

public class ScrollViewContentController_OBSOLETE : MonoBehaviour
{
    public float Spacing;

    private List<GameObject> content;

    void Start()
    {
        // get all child of this object
        foreach (Transform childTransform in transform)
            content.Add(childTransform.gameObject);
        DisplayContent();
    }

    void Update()
    {
        
    }

    public void DisplayContent()
    {
        for (int i = 0; i < content.Count; i++)
        {
            //content[i].transform.position = 
        }
    }
}
