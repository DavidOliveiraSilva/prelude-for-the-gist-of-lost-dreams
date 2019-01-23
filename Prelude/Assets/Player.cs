using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        float angle = 0;
        if(Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
            angle = Mathf.Atan2(ver, hor);
            rb.velocity = new Vector2(speed * Time.deltaTime * Mathf.Cos(angle), speed * Time.deltaTime * Mathf.Sin(angle));
        } else {
            rb.velocity = new Vector2(0, 0);
        }
	}
}
