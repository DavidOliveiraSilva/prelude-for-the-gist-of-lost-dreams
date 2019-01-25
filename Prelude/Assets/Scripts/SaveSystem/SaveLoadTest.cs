using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadTest : MonoBehaviour {


	// Use this for initialization
	public GameObject slots;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewSave() {
		string[] paths = SaveSystem.GetFilePaths();
		string nome = "Save_" + paths.Length;
		SaveData save = new SaveData();
		save.saveName = nome;
		save.id = paths.Length;
		save.lvCount = 2;
		SaveSystem.SaveGame(save);
	}

	public ArrayList Load() {
		ArrayList saves = SaveSystem.LoadAll();
		if (saves == null) {
			print("Não há saves\n");
			return null;
		}
		return saves;
	}

	public void refresh() {
		Transform trans = slots.GetComponent<RectTransform>();
		ArrayList list = Load();
		if (list == null)
			return;

		foreach (SaveData s in list)
		{
			trans.GetChild(s.id).GetComponentInChildren<Text>().text = s.saveName;
		}
	}
}

