using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
Classe para receber callbacks das animações */
public class MenuReady : MonoBehaviour {

	public MenuTitle title;

	//Chamada na animação de fade in da logo quando ela termina.
	public void menuReady() {
		title.menuReady();
	}

	//Chamada pela animação de fade out de quando se inicia o jogo, indicando que pode começar o loading.
	public void endFade(string action) {
		if (action.Equals("start")) {
			title.iniciarJogoFade();
		}
	}
}
