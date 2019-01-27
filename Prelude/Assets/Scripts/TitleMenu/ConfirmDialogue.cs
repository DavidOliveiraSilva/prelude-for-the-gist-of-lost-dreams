using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfirmDialogue : MonoBehaviour {

	public delegate void choiceAction();
	//delegate executada quando se clica em sim
	public choiceAction yesCallback;
	//delegate executada quando se clilca em não
	public choiceAction noCallback;
	public choiceAction postCallback;

	public GameObject yesBtn;

	private GameObject lastSelected = null;
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

	public void confirm() {
		if (yesCallback != null)
			yesCallback();
		hideDialogue();
			
		
		
	}

	public void cancel() {
		if (noCallback != null)
			noCallback();
		hideDialogue();
		
	}

	public void showDialogue() {
		previousActive = false;
		gameObject.SetActive(true);

		EventSystem.current.SetSelectedGameObject(yesBtn);
	}

	public void showDialogue(GameObject returnObject) {
		lastSelected = returnObject;
		showDialogue();
	}

	public void hideDialogue() {
		EventSystem.current.SetSelectedGameObject(lastSelected);
		yesCallback = null;
		noCallback = null;
		if(postCallback != null)
			postCallback();
		postCallback = null;
		gameObject.SetActive(false);
		
	}
}
