using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    internal static class FileService
    {
        /// <summary>
        /// Dark magic, do not touch
        /// (I have no idea why, but it works)
        /// </summary>
        /// <param name="fileName">the string contaning the path to the file from the StreamingAssets folder (must contain the extension too)</param>
        /// <returns></returns>
        /// <remarks>Ignore the warning, and keep the async, this is necessary when building the apk</remarks>
        public async static Task<XmlDocument> LoadXml(string fileName)
        {
            // The path is not the same when in editor and when in the apk
            // Hence the use of Application.streamingAssetsPath
            string path = Path.Combine(Application.streamingAssetsPath, fileName);
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                // Try to load the localization file
#if UNITY_EDITOR
                xmlDocument.Load(path);
                // To avoid the warning
                await Task.Yield();
                return xmlDocument;
#else
                UnityWebRequest request = UnityWebRequest.Get(path);
                var operation = request.SendWebRequest();
                while (!operation.isDone)
                    // I am just as confused as you are
                    await Task.Yield();
                if (!(request.result == UnityWebRequest.Result.Success))
                    throw new Exception($"ERROR: Something went REALLY wrong. Good luck :)");
                else
                {
                    xmlDocument.LoadXml(request.downloadHandler.text);
                    return xmlDocument;
                }
#endif
            }
            catch (FileNotFoundException)
            {
                Debug.LogWarning($"ERROR: The file {fileName} does not exist");
                // There is no file at the designated path
                // TODO How do we handle that case ?
                return null;
            }
        }
    }
}
