using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

	private Text id;
	private Text saveName;
	private Text lvCount;
	private SaveData saveData;

	// Use this for initialization
	void Start () {
		saveData = null;
		id = transform.GetChild(0).GetComponent<Text>();
		saveName = transform.GetChild(1).GetComponent<Text>();
		lvCount = transform.GetChild(2).GetComponent<Text>();
	}

	public SaveData getSaveData(){
		return saveData;
	}

	public void setSaveData(SaveData saveData){
		this.saveData = saveData;
		if (saveData.lvCount != -1) {
			saveName.text = saveData.saveName;
			lvCount.text = saveData.lvCount.ToString();
		} else {
			saveName.text = "Vazio";
			lvCount.text = "0";
		}
	}

	
	public void createNewSave() {
		SaveData novo = new SaveData();
		novo.id = int.Parse(gameObject.name) - 1;
		novo.saveName = "save_" + novo.id.ToString();
		novo.lvCount = 2;

		SaveSystem.SaveGame(novo, novo.id);
	}
}
