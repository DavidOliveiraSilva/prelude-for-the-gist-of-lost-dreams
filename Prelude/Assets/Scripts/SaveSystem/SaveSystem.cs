using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour {

	public SaveGameData saveGameData;
    const string folderName = "SavesData";
    const string fileExtension = ".dat";

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.S))
        {
            string folderPath = Path.Combine(Application.persistentDataPath, folderName);
            if (!Directory.Exists (folderPath))
                Directory.CreateDirectory (folderPath);            

            string dataPath = Path.Combine(folderPath, saveGameData.saveName + fileExtension);       
            SaveGame (saveGameData, dataPath);
        }

        if (Input.GetKeyDown (KeyCode.L))
        {
            string[] filePaths = GetFilePaths ();
            
            if(filePaths.Length > 0)
                saveGameData = LoadGame (filePaths[0]);
        }
    }

	public static void SaveGame(SaveGameData data) {
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath))
			Directory.CreateDirectory (folderPath);            

		string dataPath = Path.Combine(folderPath, data.saveName + fileExtension);       
		SaveGame (data, dataPath);
	}

    public static void SaveGame (SaveGameData data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, data);
        }
    }

	public static SaveGameData LoadGame (int id) {
		string[] filePaths = GetFilePaths ();
        SaveGameData ret = null;
		if(filePaths.Length > 0) {
			//maneira temporária
			for (int i = 0; i < filePaths.Length; i++) {
				ret = LoadGame (filePaths[0]);
				if (ret.id == id)
					break;	
			}
			
		}
			
		return ret;
	}
    public static SaveGameData LoadGame (string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.Open))
        {
            return (SaveGameData)binaryFormatter.Deserialize (fileStream);
        }
    }

    public static string[] GetFilePaths ()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        return Directory.GetFiles (folderPath, fileExtension);
    }
}
