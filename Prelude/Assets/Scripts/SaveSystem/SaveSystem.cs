﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 



public class SaveSystem : MonoBehaviour {

	public SaveData saveGameData;
    const string folderName = "SavesData";
    const string fileExtension = ".dat";

	public class dataComparator : IComparer {
        public int Compare(object x, object y)
        {
            return ((SaveData)x).id.CompareTo(((SaveData)y).id);
        }

    } 

    void Update ()
    {
		/* 
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
		*/
    }

	public static void SaveGame(SaveData data) {
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath))
			Directory.CreateDirectory (folderPath);            

		string dataPath = Path.Combine(folderPath, data.saveName + fileExtension);       
		SaveGame (data, dataPath);
	}

    public static void SaveGame (SaveData data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, data);
        }
    }

	public static ArrayList LoadAll() {
		ArrayList retu = null;
		string[] filePaths = GetFilePaths ();
        
		if(filePaths.Length > 0) {
			//maneira temporária
			retu = new ArrayList();
			for (int i = 0; i < filePaths.Length; i++) {
				retu.Add(LoadGame (filePaths[i]));	
			}
			IComparer comp = new dataComparator();
			retu.Sort(comp);
		}
		
		
		return retu;
	}

	public static SaveData LoadGame (int id) {
		string[] filePaths = GetFilePaths ();
        SaveData ret = null;
		if(filePaths.Length > 0) {
			//maneira temporária
			for (int i = 0; i < filePaths.Length; i++) {
				ret = LoadGame (filePaths[i]);
				if (ret.id == id)
					break;	
			}
			
		}
			
		return ret;
	}
    public static SaveData LoadGame (string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.Open))
        {
            return (SaveData)binaryFormatter.Deserialize (fileStream);
        }
    }

    public static string[] GetFilePaths ()
    {
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath))
			Directory.CreateDirectory (folderPath);

        return Directory.GetFiles (folderPath);
    }
}
