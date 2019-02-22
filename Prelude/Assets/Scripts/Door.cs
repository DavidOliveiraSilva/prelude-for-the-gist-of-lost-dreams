using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator animator;
    public ParticleSystem[] particles;

	void Start () {
        animator = GetComponent<Animator>();
        particlesEmission(false);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void Open() {
        animator.SetBool("open", true);
        particlesEmission(true);
    }

    public void particlesEmission(bool emision) {
        foreach (var p in particles)
        {
            p.enableEmission = emision;
        }
    }

    public void particlesStop() {
        foreach (var p in particles)
        {
            p.enableEmission = false;
        }
    }
}
