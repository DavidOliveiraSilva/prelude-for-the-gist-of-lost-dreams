using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float speed;
    public float dashDuration;
    public float dashSpeed;
    public int dashLoad;
    public float lifeTime;
    public float maxLifeTime;
    private bool dead;
    private float dashing;
    private float dashAngle;
    private bool hasControl;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        dashing = 0;
        hasControl = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        float angle = 0;
        if (hasControl) {
            if (Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
                angle = Mathf.Atan2(ver, hor);
                rb.velocity = new Vector2(speed * Time.deltaTime * Mathf.Cos(angle) * Mathf.Abs(hor), speed * Time.deltaTime * Mathf.Sin(angle) * Mathf.Abs(ver));
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if(hor > 0) {
            sr.flipX = true;
        }
        if(hor < 0) {
            sr.flipX = false;
        }

        if (hasControl) {
            if (dashLoad > 0 && Input.GetButtonDown("Dash")) {
                dashLoad--;
                Dash();
            }
        }

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            startDeath();
        }

        if(dashing > 0) {
            dashing -= Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed*Time.deltaTime*speed*Mathf.Cos(dashAngle), dashSpeed * Time.deltaTime * speed * Mathf.Sin(dashAngle));
            if (dashing <= 0) {
                dashing = 0;
                hasControl = true;
                rb.velocity = new Vector2(0, 0);
            }
        }

        
        
    }

    public void FixedUpdate() {
        //CHECANDO COLISÃO COM CAIXA E PAREDE (QUANDO A TUANE É IMPRENSADA PELA CAIXA E PAREDE)
        int layermask = 1 << 10;
        layermask = ~layermask;
        RaycastHit2D rcLeft = Physics2D.Linecast(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y, 0), layermask);
        RaycastHit2D rcRight = Physics2D.Linecast(transform.position, new Vector3(transform.position.x + 0.5f, transform.position.y, 0), layermask);
        RaycastHit2D rcUp = Physics2D.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), layermask);
        RaycastHit2D rcDown = Physics2D.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y - 0.5f, 0), layermask);

        if (rcLeft && rcRight) {
            if (rcLeft.transform != null && rcRight.transform != null) {
                if ((rcLeft.transform.gameObject.tag == "Moving Box" && rcRight.transform.gameObject.tag == "Wall") ||
                        (rcLeft.transform.gameObject.tag == "Wall" && rcRight.transform.gameObject.tag == "Moving Box") ||
                            (rcLeft.transform.gameObject.tag == "Moving Box" && rcRight.transform.gameObject.tag == "Moving Box")) {
                    startDeath();
                }
            }
        }
        if (rcUp && rcDown) {
            if (rcUp.transform != null && rcDown.transform != null) {
                if ((rcUp.transform.gameObject.tag == "Moving Box" && rcDown.transform.gameObject.tag == "Wall") ||
                        (rcUp.transform.gameObject.tag == "Wall" && rcDown.transform.gameObject.tag == "Moving Box") ||
                            (rcUp.transform.gameObject.tag == "Moving Box" && rcDown.transform.gameObject.tag == "Moving Box")) {
                    startDeath();
                }
            }
        }
        Debug.DrawLine(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y, 0));
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + 0.5f, transform.position.y, 0));
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, 0));
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 0.5f, 0));
    }
    void CheckRC(RaycastHit2D rc) {
        if(rc.transform != null) {
            print(rc.transform.tag);
        }
    }
    public void AddLifeTime(float value) {
        lifeTime += value;
        if(lifeTime > maxLifeTime) {
            lifeTime = maxLifeTime;
        }
    }
    void Dash() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        dashAngle = Mathf.Atan2(ver, hor);
        if (Mathf.Abs(hor) > 0 || Mathf.Abs(ver) > 0) {
            dashAngle = Mathf.Atan2(ver, hor);
        } else {
            if (sr.flipX) {
                dashAngle = 0;
            } else {
                dashAngle = Mathf.PI;
            }
        }
        dashing = dashDuration;
        hasControl = false;
    }
    

    public void startDeath() {
        dead = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Thorn")) {
            startDeath();
        }
        if (col.gameObject.tag.Equals("Mortal Cell")) {
            startDeath();
        }
        if (col.gameObject.tag.Equals("Purple Flower")) {
            startDeath();
        }
    }
}
