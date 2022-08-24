using System;
using UnityEngine;

public class ModularScreenController : ScreenController
{
    private string currentMode;

    [Serializable]
    public class ScreenMode
    {
        public string mode;
        public GameObject[] gameObjects;
    }

    public ScreenMode[] screenModes;

    public void SetMode(string mode)
    {
        currentMode = mode;
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
        // activation is after deactivation to avoid conflicts in case one of the gameObjects is in multiple modes
        foreach (ScreenMode screenMode in screenModes)
        {
            if (screenMode.mode == mode)
            {
                foreach (GameObject gameObject in screenMode.gameObjects)
                    gameObject.SetActive(true);
                return;
            }
        }
        Debug.LogWarning($"WARNING: {this} has no mode corresponding to {mode}");
    }
}
