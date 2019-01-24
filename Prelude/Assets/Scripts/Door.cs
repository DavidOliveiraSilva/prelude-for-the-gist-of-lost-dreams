using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private bool opening;
    private Rigidbody2D door1rb;
    private Rigidbody2D door2rb;

    public float openingSpeed;
    private float time;
	// Use this for initialization
	void Start () {
        opening = false;
        time = 0;
        door1rb = transform.Find("Door 1").gameObject.GetComponent<Rigidbody2D>();
        door2rb = transform.Find("Door 2").gameObject.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (opening) {
            time += Time.deltaTime;
            door1rb.velocity = new Vector2(0, openingSpeed);
            door2rb.velocity = new Vector2(0, -openingSpeed);
            if(time >= (1 / openingSpeed)) {
                opening = false;
                door1rb.transform.localPosition = new Vector3(0, 2, 0);
                door2rb.transform.localPosition = new Vector3(0, -2, 0);
                Destroy(door1rb);
                Destroy(door2rb);
            }
        }
	}
    public void Open() {
        door1rb.bodyType = RigidbodyType2D.Dynamic;
        door2rb.bodyType = RigidbodyType2D.Dynamic;
        opening = true;
    }
}
