using System;
using System.IO;
using Newtonsoft.Json;

public class Utils
{

    public static string SerializeObject(System.Object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T DeserializeObject<T>(string jsonString)
    {
        T gameSave = JsonConvert.DeserializeObject<T>(jsonString);

        return gameSave;
    }

    public static void WriteTextToFile(string fileName, string data)
    {
        string filePath = UnityEngine.Application.persistentDataPath + fileName;
        System.IO.File.WriteAllText(filePath, data);
    }

    public static string ReadTextFromFile(string fileName)
    {
        string filePath = UnityEngine.Application.persistentDataPath + fileName;

        string savedData = null;
        if (File.Exists(filePath))
        {
            savedData = File.ReadAllText(filePath);
        }

        return savedData;
    }

}
