using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        
    }

    public virtual void OnEnable()
    {
        SetLanguage();
    }

    public virtual void Update()
    {
        
    }

    public virtual void SetLanguage()
    {

    }

    /*public virtual void SetMode(string mode)
    {

    }*/

    /*[System.Obsolete("This overload is deprecated, please use the one taking a gameObject instead")]
    public virtual void OpenScreen(int screenIndex)
    {

        if (screenIndex < 0 || screenIndex >= screens.Length)
            // An invalid screen index will cause the app to display only the background
            Debug.LogWarning($"WARNING: invalid screen index {screenIndex}");
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(i == screenIndex);
        }
        gameObject.SetActive(false);
    }*/

    public virtual void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);
        gameObject.SetActive(false);
    }

    // Doesn't work, because buttons can only accept one argument
    /*public virtual void OpenSreen(GameObject screen, string mode)
    {
        screen.GetComponent<ScreenController>().SetMode(mode);
        OpenScreen(screen);
    }*/

    public void SetMode(int screenModeIndex)
    {
        ModularScreenController.SetMode((ScreenMode)screenModeIndex);
    }
}
