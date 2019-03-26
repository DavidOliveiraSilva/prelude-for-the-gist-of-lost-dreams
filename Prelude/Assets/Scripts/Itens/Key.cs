using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    private SpringJoint2D sj;
    private bool connected;
    private ParticleSystem ps;
    private SpriteRenderer sr;
    private bool dead;
    private float alpha;
    private float dyingConstant;
    [Range(0.0f, 1.0f)]
    public float springDamping;
    public float springDistance;
	// Use this for initialization
	void Start () {
        connected = false;
        dead = false;
        alpha = 1;
        
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        dyingConstant = ps.main.startLifetime.constantMax;
    }
	
	// Update is called once per frame
	void Update () {
        if (dead) {
            alpha -= Time.deltaTime/ dyingConstant;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }
	}

    void AutoDestroy() {
        dead = true;
        ps.Stop();

        Destroy(gameObject, dyingConstant);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            connected = true;
            gameObject.AddComponent<SpringJoint2D>();
            sj = gameObject.GetComponent<SpringJoint2D>();
            sj.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            sj.dampingRatio = springDamping;
            sj.distance = springDamping;
        }
        if(collision.tag == "Door") {
            if (connected) {
                sj.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                sj.distance = 0.5f;
                collision.gameObject.GetComponent<Door>().Open();
                AutoDestroy();
            }
        }
    }

}
