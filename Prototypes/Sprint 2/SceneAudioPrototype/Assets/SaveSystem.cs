using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public static class SaveSystem
{
 
 //Theoretical w the string being passed
   public static void Save(int saveFileNumber, string dataToSave){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData" + saveFileNumber + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, dataToSave);
        stream.Close();
   }

//Currently loads string, Should eventually save status of game,

//Also you should be able to delete a save file if needed
   public static string LoadSave(int saveFileNumber){
    string path = Application.persistentDataPath + "/SaveData" + saveFileNumber + ".fun";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string saveString = formatter.Deserialize(stream) as string;
            stream.Close();
            return saveString;

        } else {
            Debug.Log("Save file not find in " + path);
            return null;
        }
   }
   public static void DeleteSave(int saveFileNumber) {
     string path = Application.persistentDataPath + "/SaveData" + saveFileNumber + ".fun";
        if (File.Exists(path)){
            File.Delete(path);
        }
   }
}
