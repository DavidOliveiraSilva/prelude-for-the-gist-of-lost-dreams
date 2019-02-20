using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObject : MonoBehaviour {
    public float duration;
    public GameObject release;
    public bool dead;
	// Use this for initialization
	void Start () {
		
	}

    void Death() {
        dead = true;
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            duration -= Time.deltaTime;
            if (duration <= 0) {
                GameObject rl = Instantiate(release);
                rl.transform.position = transform.position;
                Death();
            }
        }
	}
}
