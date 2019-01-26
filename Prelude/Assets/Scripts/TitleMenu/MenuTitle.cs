using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuTitle : MonoBehaviour {

	public GameObject logo;
	public GameObject options;
	public GameObject saves;
	public GameObject anyKey;
	public GameObject fadeOut;
	public string startScene = "SampleScene";

	private ArrayList menus = new ArrayList();
	private bool activeMenu = false;
	private bool animStarted = false;
	private EventSystem m_EventSystem;

	[SerializeField]
	public enum MenuType {Options=0, Saves=1};
	public MenuType t;

	void OnEnable() {
		m_EventSystem = EventSystem.current;
	}
	void Start () {
		menus.Add(options);
		menus.Add(saves);
		logo.SetActive(false);
		options.SetActive(false);
		saves.SetActive(false);

		DontDestroyOnLoad(GameObject.Find("Gamestate"));
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
		changeActiveMenu((int)MenuType.Options);
	
	}
	//função que inicia o jogo de fato. Depois do fade out terminar
	public void iniciarJogoFade() {
		SceneManager.LoadScene(startScene);
	}
	public void iniciarJogo() {
		if(!activeMenu)
			return;
			m_EventSystem.enabled = false;
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

	public void changeActiveMenu(int menu) {
		switch ((MenuType)menu)
		{
			case (MenuType.Options):
				closeAllMenus();
				options.SetActive(true);
				m_EventSystem.SetSelectedGameObject(options.transform.GetChild(0).gameObject);
				break;
			case (MenuType.Saves):
				closeAllMenus();
				//preencher os slots com os saves no disco
				refreshSaveSlots();
				saves.SetActive(true);
				m_EventSystem.SetSelectedGameObject(saves.transform.GetChild(1).gameObject);
				break;
			default:
				break;
		}
	}

	public void refreshSaveSlots() {
		ArrayList savesData = SaveSystem.LoadAll();
		for (int i = 0; i < savesData.ToArray().Length; i++) {
			SaveData tData = (SaveData)savesData[i];
			//print(tData.id + " " + tData.lvCount);
			saves.transform.GetChild(0).GetChild(tData.id).GetComponent<SaveSlot>().setSaveData(tData);
		}
		
	}

	public void closeAllMenus() {
		foreach (GameObject menu in menus) {
			menu.SetActive(false);
		}
	}
}
