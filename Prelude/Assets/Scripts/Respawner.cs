using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
    public GameObject objeto;
    public float intervalo;
    public bool bloqueado;
    public int max;
    public bool random;
    private float last;
    private float width;
    private float height;
    private GameObject instanciado;
    private float counter;
    private int childCount;
	// Use this for initialization
	void Start () {
        last = Time.time;
        width = transform.localScale.x;
        height = transform.localScale.y;
        counter = 0;
        childCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        childCount = 0;
        foreach (var item in transform) {
            if (item != null) {
                childCount++;
            }
        }
        if (!bloqueado) {
            if (Time.time - last > intervalo) {
                GameObject obj = Instantiate(objeto);
                if (random) {
                    obj.transform.position = new Vector3(transform.position.x + Random.Range(-width / 2, width / 2),
                        transform.position.y + Random.Range(-height / 2, height / 2), transform.position.z);
                } else {
                    obj.transform.position = transform.position;
                }
                last = Time.time;
            }
        } else {
            //if (instanciado == null) {
                //counter += Time.deltaTime;
            //}
            if(childCount < max) {
                counter += Time.deltaTime;
            }
            if (counter > intervalo) {
                instanciado = Instantiate(objeto);
                instanciado.transform.SetParent(transform);
                instanciado.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                instanciado.GetComponent<CircleCollider2D>().radius = 0.5f / Mathf.Max(width, height);
                if (random) {
                    instanciado.transform.position = new Vector3(transform.position.x + Random.Range(-width / 2, width / 2),
                        transform.position.y + Random.Range(-height / 2, height / 2), transform.position.z);
                } else {
                    instanciado.transform.position = transform.position;
                }
                counter = 0;
            }
            
        }
        


	}
}
