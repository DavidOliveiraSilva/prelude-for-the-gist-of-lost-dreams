using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotSelection : MonoBehaviour {

	public GameObject continueBtn;
	public MenuTitle menu;
	

	public Text idTxt;
	public Text nameTxt;
	public Text lvTxt;

	public ConfirmDialogue confirmDialogue;

	public SaveSlot selectedSlot;
	private EventSystem m_EventSystem;

	private bool previousActive = true;
	// Use this for initialization
	void Start () {
		if(previousActive)
			gameObject.SetActive(false);
		m_EventSystem = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showScreen() {
		idTxt.text = selectedSlot.saveData.id.ToString();
		nameTxt.text = selectedSlot.saveData.saveName;
		lvTxt.text = selectedSlot.saveData.lvCount.ToString();

		previousActive = false;
		gameObject.SetActive(true);
		m_EventSystem.SetSelectedGameObject(continueBtn);
	}

	public void hideScreen() {
		menu.refreshSaveSlots();
		gameObject.SetActive(false);
	}

	public void conitnueBtnSelection() {
		GameObject.Find("Gamestate").GetComponent<Gamestate>().saveData = selectedSlot.saveData;
		menu.iniciarJogo();
	}

	public void exitBtnSelection() {
		hideScreen();
	}

	public void deleteBtnSelection() {
		confirmDialogue.yesCallback += deleteTrue;
		confirmDialogue.showDialogue();
	}

	public void deleteTrue() {
		SaveSystem.DeleteGame(selectedSlot.saveData.id);
		selectedSlot.initializeSaveData();
		hideScreen();
	}
}
