using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {
    
    private Fade fade;
	// Use this for initialization
	void Start () {
        
        fade = GetComponent<Fade>();
	}
	
	// Update is called once per frame
	void Update () {
        if (fade.done) {
            SceneManager.LoadScene("SampleScene");
        }
	}
}
