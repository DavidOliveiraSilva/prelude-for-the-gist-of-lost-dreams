using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    private GameObject start;
    private GameObject end;
    private bool active;
    public float ActiveDuration;
    public float DeactivatedDuration;
    public float counter;
    private ParticleSystem ps;
    public float monitor;
    private RaycastHit2D rc;
    private RaycastHit2D[] trackedObject;
    // Use this for initialization
    void Start () {
        counter = 0;
        start = transform.Find("Start").gameObject;
        end = transform.Find("End").gameObject;
        ps = start.GetComponent<ParticleSystem>();
        ps.Stop();
        float dy = end.transform.position.y - start.transform.position.y;
        float dx = end.transform.position.x - start.transform.position.x;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        start.transform.eulerAngles = new Vector3(0, 0, angle);
    }
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (active) {
            int layerMask = 1 << 10;
            rc = Physics2D.Linecast(start.transform.position, end.transform.position, layerMask);
            if (rc) {
                if (rc.transform != null && rc.transform.gameObject.tag == "Player") {
                    rc.transform.gameObject.GetComponent<Player>().startDeath();
                }
            }
            
            if (counter >= ActiveDuration) {
                counter = 0;
                active = false;
                Desativar();
            }
        } else {
            if(counter >= DeactivatedDuration) {
                counter = 0;
                active = true;
                Ativar();
            }
        }

	}
    void Ativar() {
        ps.Play();
    }
    void Desativar() {
        ps.Stop();
    }
}
