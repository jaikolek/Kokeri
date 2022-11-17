using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(SaveLoad data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/kokeri.kr";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveLoad saveData = new SaveLoad();

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveLoad LoadData()
    {
        string path = Application.persistentDataPath + "/kokeri.kr";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveLoad data = formatter.Deserialize(stream) as SaveLoad;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save data not found in " + path);
            return null;
        }
    }
}
