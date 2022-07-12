using UnityEngine;

public class MainSettingsScreenController : ScreenController
{
    public GameObject ScanningScreen;

    public void OpenScan(QRCodeReader.Mode mode)
    {
        ScanningScreen.GetComponent<ScanningScreenController>().InitScan(gameObject, mode);
        OpenScreen(ScanningScreen);
    }
}
