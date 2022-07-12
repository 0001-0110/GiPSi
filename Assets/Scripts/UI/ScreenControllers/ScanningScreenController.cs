using UnityEngine;
using UnityEngine.UI;

public class ScanningScreenController : ScreenController
{
    private QRCodeReader QRCodeReader;

    public GameObject TextObject;
    private Text text;

    private GameObject previousScreen;

    public override void Awake()
    {
        base.Awake();
        QRCodeReader = GetComponent<QRCodeReader>();
        text = TextObject.GetComponent<Text>();
    }

    public void InitScan(GameObject previousScreen, QRCodeReader.Mode mode)
    {
        this.previousScreen = previousScreen;
        QRCodeReader.SetMode(mode);
    }

    public void DisplayText(string text, float duration = -1)
    {
        // Cristal clear
        this.text.text = text;
        TextObject.SetActive(text != null);
    }

    /// <summary>
    /// Open the screen we were coming from
    /// </summary>
    public void Back()
    {
        OpenScreen(previousScreen);
    }
}
