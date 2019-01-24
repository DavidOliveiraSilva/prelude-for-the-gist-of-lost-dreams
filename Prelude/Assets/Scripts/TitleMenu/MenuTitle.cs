using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour {

	public GameObject logo;
	public GameObject options;
	public GameObject anyKey;
	public GameObject fadeOut;
	public string startScene = "SampleScene";

	private bool activeMenu = false;
	private bool animStarted = false;

	// Use this for initialization
	void Start () {
		logo.SetActive(false);
		options.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!animStarted && Input.anyKeyDown) {
			activateMenu();
		}
		
	}

	private void activateMenu() {
		animStarted = true;
		anyKey.SetActive(false);
		logo.SetActive(true);
		logo.GetComponent<Animation>().Play("LogoShowAnim");
		
	}

	/**
	Função chamada pela animação pra indicar que a animação terminou e o menu já pode ser acessado */
	public void menuReady() {
		activeMenu = true;
		animStarted = true;
		options.SetActive(true);
	
	}
	public void iniciarJogoFade() {
		SceneManager.LoadScene(startScene);
	}
	public void iniciarJogo() {
		if(!activeMenu)
			return;
		fadeOut.GetComponent<Animation>().Play("StartFadeOut");
	}

	public void sair() {
		if(!activeMenu)
			return;
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
