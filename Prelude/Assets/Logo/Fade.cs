using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    private SpriteRenderer sr;
    public Color initialColor;
    public Gradient fade;
    public float start; //3
    public float duration; //5
    private float timer;
    public bool done;
    public bool loop;
    // Use this for initialization
	void Start () {
        timer = 0;
        done = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = initialColor;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > start && timer - start < duration) {
            sr.color = fade.Evaluate((timer - start)/duration);
        }
        if(timer - start >= duration && !loop) {
            done = true;
        }
        if (timer - start >= duration && loop) {
            timer = start;
        }
	}
}
