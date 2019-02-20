using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFlower : MonoBehaviour {
    public GameObject purplePetal;
    public float GenerateFreq;
    private float lastGen;
    public float PetalSpeed;
	// Use this for initialization
	void Start () {
        lastGen = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastGen > GenerateFreq) {
            GameObject pp = Instantiate(purplePetal);
            pp.transform.position = transform.position;
            GameObject tuane = GameObject.Find("Tuane");
            float dx = tuane.transform.position.x - pp.transform.position.x;
            float dy = tuane.transform.position.y - pp.transform.position.y;
            float angle = Mathf.Atan2(dy, dx);
            pp.GetComponent<Rigidbody2D>().velocity = new Vector2(PetalSpeed*Time.deltaTime*Mathf.Cos(angle), PetalSpeed * Time.deltaTime * Mathf.Sin(angle));
            lastGen = Time.time;
        }
	}
}
    