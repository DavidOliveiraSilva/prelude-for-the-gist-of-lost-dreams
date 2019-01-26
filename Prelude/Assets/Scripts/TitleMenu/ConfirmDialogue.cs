﻿using System.Collections;
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

	public GameObject yesBtn;

	private GameObject lastSelected = null;
	private EventSystem m_EventSystem;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void confirm() {
		hideDialogue();
		yesCallback();
		
	}

	public void cancel() {
		hideDialogue();
		noCallback();
	}

	public void showDialogue() {
		gameObject.SetActive(true);
		lastSelected = m_EventSystem.lastSelectedGameObject;
		m_EventSystem.SetSelectedGameObject(yesBtn);
	}

	public void hideDialogue() {
		gameObject.SetActive(false);
		m_EventSystem.SetSelectedGameObject(lastSelected);
	}
}