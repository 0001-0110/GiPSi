public class MainMenuController : ScreenController
{
    public ScanningScreenController ScanningScreenController;

    public void InitScan()
    {
        ScanningScreenController.InitScan(gameObject, QRCodeReader.Mode.Position);
    }
}
