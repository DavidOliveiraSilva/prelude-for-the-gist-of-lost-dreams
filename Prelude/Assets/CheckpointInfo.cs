using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInfo : MonoBehaviour {

    public static CheckpointInfo instance;
    public Vector2 startposition;
    public Vector2 lastPoint;
    public bool ativado;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance != null) {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
