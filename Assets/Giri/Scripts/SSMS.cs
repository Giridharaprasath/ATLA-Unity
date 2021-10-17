using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Save System Manager Script
public static class SSMS
{
    //public static string path = Application.persistentDataPath + "/singleplayer.gg"; 

    public static string path = "D:/singleplayer.gg"; 

    public static void SavePlayer(SPLSS player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SPGD data = new SPGD(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SPGD LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SPGD data = formatter.Deserialize(stream) as SPGD;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("SAVE FILE NOT FOUND IN " + path);
            return null;
        }
    }
}
