using System;
using UnityEngine;

public class ModularScreenController : ScreenController
{
    [Serializable]
    public class ScreenMode
    {
        public string mode;
        public GameObject[] gameObjects;
    }

    public ScreenMode[] screenModes;

    public void SetMode(string mode)
    {
        // Firts begins by deactivating all other modes
        foreach (ScreenMode screenMode in screenModes)
        {
            if (screenMode.mode != mode)
            {
                foreach (GameObject gameObject in screenMode.gameObjects)
                    gameObject.SetActive(false);
            }
        }
        // Then activate the correct mode
        foreach (ScreenMode screenMode in screenModes)
        {
            if (screenMode.mode == mode)
            {
                foreach (GameObject gameObject in screenMode.gameObjects)
                    gameObject.SetActive(true);
            }
        }
    }
}
