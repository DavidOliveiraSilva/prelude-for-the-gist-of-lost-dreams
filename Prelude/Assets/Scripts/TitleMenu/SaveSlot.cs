using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

	private Text id;
	private Text saveName;
	private Text lvCount;
	public SaveData saveData;

	public MenuTitle menu;
	public ConfirmDialogue confirmSlotDialogue;

	// Use this for initialization
	void Start () {
		saveData = new SaveData();
		saveData.lvCount = -1;

		id = transform.GetChild(0).GetComponent<Text>();
		saveName = transform.GetChild(1).GetComponent<Text>();
		lvCount = transform.GetChild(2).GetComponent<Text>();
	}

	public SaveData getSaveData(){
		return saveData;
	}

	public void setSaveData(SaveData saveData){
		
		this.saveData = saveData;
		//print(saveData.id + " " + saveData.lvCount);
		if (saveData.lvCount != -1) {
			id.text = (saveData.id+1).ToString();
			saveName.text = saveData.saveName;
			lvCount.text = saveData.lvCount.ToString();
		} else {
			id.text = (saveData.id+1).ToString();
			saveName.text = saveData.saveName;
			lvCount.text = "0";
		}
	}

	public void selectSlot() {
		//SaveData sData = SaveSystem.LoadGame(int.Parse(gameObject.name) - 1);
		if(saveData.lvCount != -1) {
			GameObject.Find("Gamestate").GetComponent<Gamestate>().saveData = saveData;
		} else {
			confirmSlotDialogue.yesCallback += createNewSave;
			confirmSlotDialogue.yesCallback += menu.refreshSaveSlots;
			confirmSlotDialogue.showDialogue();
		}
	}
	public void createNewSave() {
		SaveData novo = new SaveData();
		novo.id = int.Parse(gameObject.name) - 1;
		novo.saveName = "save_" + novo.id.ToString();
		novo.lvCount = 0;

		saveData = novo;

		SaveSystem.SaveGame(novo, novo.id);
	}
}
