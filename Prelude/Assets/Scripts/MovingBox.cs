using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour {
    public Sentido sentido;
    public int distance;
    public int MoveDuration;
    public float StopDuration;
    private float moveCount;
    private float stopCount;
    private int position;
    private bool moving;
    public Vector3 position0;
    public Vector3 position1;
	// Use this for initialization
	void Start () {
        stopCount = StopDuration;
        moveCount = MoveDuration;
        position0 = transform.position;
        position1 = new Vector3(sentido == Sentido.Horizontal ? position0.x + distance : position0.x,
            sentido == Sentido.Vertical ? position0.y + distance : position0.y, position0.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (!moving) {
            stopCount -= Time.deltaTime;
            if (stopCount <= 0) {
                moving = true;
                stopCount = StopDuration;
            }
        } else {
            moveCount -= Time.deltaTime;
            if (moveCount <= 0) {
                moving = false;
                moveCount = MoveDuration;
                if (position == 0) {
                    transform.position = position1;
                    position = 1;
                } else {
                    transform.position = position0;
                    position = 0;
                }
            }
            
        }
        
	}
    private void FixedUpdate() {
        if (moving) {
            Move(Time.fixedDeltaTime);
        }
    }
    void Move(float dt) {
        if (position == 0) {
            if (sentido == Sentido.Horizontal) {
                transform.Translate(distance*dt*(1/MoveDuration), 0, 0);
            } else {
                transform.Translate(0, distance*dt * (1 / MoveDuration), 0);
            }
        } else {
            if (sentido == Sentido.Horizontal) {
                transform.Translate(-distance*dt * (1 / MoveDuration), 0, 0);
            } else {
                transform.Translate(0, -distance*dt * (1 / MoveDuration), 0);
            }
        }
    }
}
