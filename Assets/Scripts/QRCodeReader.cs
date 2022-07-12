//using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
// QR code decoder
using ZXing;

public class QRCodeReader : MonoBehaviour
{
    public ScanningScreenController ScanningScreenController;

    public GameObject CameraFeed;
    // The image coming from the front camera (I think)
    private RawImage cameraFeed;
    // TODO what even is this for ?
    private AspectRatioFitter aspectRatioFitter;

    private WebCamTexture cameraTexture;

    private Mode CurrentMode;
    private BarcodeReader barcodeReader;
    private bool isCamAvailable;

    //[SerializeField]
    public enum Mode
    {
        Position,
        Profile,
        TimeTable,
    }

    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        barcodeReader = new BarcodeReader();
        cameraFeed = CameraFeed.GetComponent<RawImage>();
        aspectRatioFitter = CameraFeed.GetComponent<AspectRatioFitter>();
        SetupCamera();
    }

    private void Update()
    {
        // Works fine on the computer, but ruins the framerate on the phone
        //Scan();
    }

    public void SetMode(Mode mode)
    {
        CurrentMode = mode;
    }

    private void SetupCamera()
    {
        isCamAvailable = false;

        // get an array of all cameras of this device
        WebCamDevice[] cameras = WebCamTexture.devices;



        foreach (WebCamDevice camera in cameras)
        {
            // TODO remove true
            if (!camera.isFrontFacing || true)
            {
                Debug.Log($"DEBUG - 22 | Suitable camera found");
                isCamAvailable = true;
                cameraTexture = new WebCamTexture(camera.name, Screen.height, Screen.width);
                // Play the video from the camera on the texture
                cameraTexture.Play();
                // TODO comments
                cameraFeed.texture = cameraTexture;
                // This line ensure that we keep the corrcet ratio, to avoid any image distorsion
                aspectRatioFitter.aspectRatio = (float)cameraTexture.width / cameraTexture.height;
                // Flip the image ? This line seems to have no effect whatsoever, so I don't really know
                cameraFeed.rectTransform.localEulerAngles = new Vector3(0, 0, -cameraTexture.videoRotationAngle);
                // return
                return;
            }
        }
        // No suitable camera found
        Debug.LogWarning($"WARNING: No suitable camera found");
        ScanningScreenController.DisplayText($"No suitable camera found");
    }

    /*private async Task<string> Decode(Color32[] rawRGB, int width, int heigth)
    {
        // analyze the camera feed to detect and decode the QR code
        Result result = barcodeReader.Decode(rawRGB, width, heigth);
        //Result result = await Task.Run(() => barcodeReader.Decode(rawRGB, width, heigth));
        // TODO remove debug
        Debug.Log($"DEBUG - 22 | Scanning result: {result}");
        return result?.ToString();
    }*/

    private string Decode(Color32[] rawRGB, int width, int heigth)
    {
        // analyze the camera feed to detect and decode the QR code
        Result result = barcodeReader.Decode(rawRGB, width, heigth);
        //Result result = await Task.Run(() => barcodeReader.Decode(rawRGB, width, heigth));
        // TODO remove debug
        Debug.Log($"DEBUG - 22 | Scanning result: {result}");
        return result?.ToString();
    }

    public async void Scan()
    {
        if (!isCamAvailable)
        {
            ScanningScreenController.DisplayText($"No suitable camera found");
            return;
        }
        string scanResult = Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
        if (scanResult != null)
        {
            ScanningScreenController.DisplayText(scanResult);
            switch (CurrentMode)
            {
                case Mode.Position:
                    break;
                case Mode.Profile:
                    break;
                case Mode.TimeTable:
                    break;
                default:
                    break;
            }
            ScanningScreenController.Back();
        }
        else
        {
            ScanningScreenController.DisplayText("No QR code detected");
        }
    }
}
