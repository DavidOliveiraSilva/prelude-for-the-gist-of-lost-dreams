using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {
    public float value;
    public GameObject release;
    private ParticleSystem ps;
    private bool finished;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void autoDestroy() {
        finished = true;
        GameObject r = Instantiate(release);
        r.transform.position = transform.position;
        ps.Stop();

        Destroy(gameObject, ps.main.startLifetime.constantMax);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            collision.GetComponent<Player>().AddLifeTime(value);
            autoDestroy();
        }
    }
}
