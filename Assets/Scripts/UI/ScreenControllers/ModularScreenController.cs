using System;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenMode
{
    SignUp = 0,
    Settings = 10,
    Navigating = 20,
    DestinationReached = 30,
}

public abstract class ModularScreenController : ScreenController
{
    private static ScreenMode activeScreenMode;
    //private static List<ModularScreenController> modularScreenControllers = new List<ModularScreenController>();

    [Serializable]
    public class ScreenConfiguration
    {
        public ScreenMode screenMode;
        public GameObject[] gameObjects;
    }

    public ScreenConfiguration[] screenConfigurations;

    public override void Awake()
    {
        base.Awake();
        //modularScreenControllers.Add(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UpdateMode();
    }

    public void UpdateMode()
    {
        // Firts begins by deactivating all other modes
        foreach (ScreenConfiguration screenConfiguration in screenConfigurations)
        {
            if (screenConfiguration.screenMode != activeScreenMode)
            {
                foreach (GameObject gameObject in screenConfiguration.gameObjects)
                    gameObject.SetActive(false);
            }
        }
        // Then activate the correct mode
        // activation is after deactivation to avoid conflicts in case one of the gameObjects is in multiple modes
        foreach (ScreenConfiguration screenConfiguration in screenConfigurations)
        {
            if (screenConfiguration.screenMode == activeScreenMode)
            {
                foreach (GameObject gameObject in screenConfiguration.gameObjects)
                    gameObject.SetActive(true);
                return;
            }
        }
        Debug.LogWarning($"WARNING: {this} has no screen configuration corresponding to {activeScreenMode}");
    }

    public static void SetMode(ScreenMode screenMode)
    {
        activeScreenMode = screenMode;
        // Only update the active sreen if it is a modular screen
        ActiveScreen.GetComponent<ModularScreenController>()?.UpdateMode();
    }
}
