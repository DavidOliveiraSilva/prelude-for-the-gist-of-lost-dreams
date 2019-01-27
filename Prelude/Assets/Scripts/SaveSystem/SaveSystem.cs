using System.IO;
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

	//Salvar o jogo pelo ID
	public static void SaveGame(SaveData data, int id) {
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath))
			Directory.CreateDirectory (folderPath);

		//Máximo de 3 slots
		if (id < 0 && id >= 3)
			return;

		string dataPath = Path.Combine(folderPath, id.ToString() + fileExtension);       
		SaveGame (data, dataPath);
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
		//SaveData padrão pra se não houver save no slot
		retu = new ArrayList();
		retu.Add(new SaveData(0, "Vazio", -1));
		retu.Add(new SaveData(1, "Vazio", -1));
		retu.Add(new SaveData(2, "Vazio", -1));

		if(filePaths.Length > 0) {
			for (int i = 0; i < filePaths.Length; i++) {
				SaveData t = LoadGame (filePaths[i]);
				retu[t.id] = t;	
			}
			IComparer comp = new dataComparator();
			retu.Sort(comp);
		}
		
		
		return retu;
	}

	public static SaveData LoadGame (int id) {
		string[] filePaths = GetFilePaths ();
        SaveData ret = null;
		if(filePaths.Length > 0 && id < filePaths.Length) {
			//maneira temporária
			ret = LoadGame (filePaths[id]);
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

	public static void DeleteGame(int id) {
		string[] filePaths = GetFilePaths ();
		if (filePaths.Length <= 0) {
			Debug.Log("Não foi possível deletar save de ID: " + id + ". Não existe nenhum slot salvo");
			return;
		}
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath)) {
			Debug.Log("Não foi possível deletar save de ID: " + id + ". Diretório não existe.");
			return;
		}
		string dataPath = Path.Combine(folderPath, id.ToString() + fileExtension);    
		if (!File.Exists(dataPath)) {
			Debug.Log("Não foi possível deletar save de ID: " + id + ". Save não existe.");
			return;
		}

		File.Delete(dataPath);
		
	}

    public static string[] GetFilePaths ()
    {
		string folderPath = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists (folderPath))
			Directory.CreateDirectory (folderPath);

        return Directory.GetFiles (folderPath);
    }
}
