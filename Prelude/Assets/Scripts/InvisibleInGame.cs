using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleInGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
