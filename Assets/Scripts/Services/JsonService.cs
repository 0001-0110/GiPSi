using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Services
{
    internal static class JsonService
    {
        public static void Serialize<T>(string filePath, T obj)
        {
            // Create a new file. if it already exist, override its content
            using (TextWriter textWritter = File.CreateText(filePath))
                //streamWriter.Write(TimeTable.ToJson());
                new JsonSerializer().Serialize(textWritter, obj);

            Debug.Log($"DEBUG: DATA ACCESS - Saved timeTable in the file {filePath}");
        }


        /// <summary>
        /// Deserialize a json file into its original object
        /// </summary>
        /// <typeparam name="T">The type of the object returned</typeparam>
        /// <param name="filePath">The path of the file to deserialize</param>
        /// <returns>
        /// <para>If the file is valid, return the object stored in it</para>
        /// <para>If not (file missing or invalid) return default value (null for classes)</para>
        /// </returns>
        public static T Deserialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // If the file doesn't exist
                Debug.Log("DEBUG: DATA ACCESS - File not found while loading json");
                return default;
            }

            // The file exists
            using (JsonTextReader jsonTextReader = new JsonTextReader(File.OpenText(filePath)))
            {
                T obj = new JsonSerializer().Deserialize<T>(jsonTextReader);
                Debug.Log($"DEBUG: DATA ACCESS - Loaded timeTable from the file {filePath}");
                return obj;
            }
        }
    }
}
