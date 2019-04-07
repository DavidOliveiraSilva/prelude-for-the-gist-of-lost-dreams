using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private CheckpointInfo checkpointinfo;
    private bool check;
    // Use this for initialization
    void Start() {
        checkpointinfo = CheckpointInfo.instance;
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Player") {
            checkpointinfo.lastPoint = new Vector2(transform.position.x, transform.position.y);
            checkpointinfo.ativado = true;
            check = true;
        }
        
    }
}
