using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuReady : MonoBehaviour {

	public MenuTitle title;
	public void menuReady() {
		title.menuReady();
	}

	public void endFade(string action) {
		if (action.Equals("start")) {
			title.iniciarJogoFade();
		}
	}
}
