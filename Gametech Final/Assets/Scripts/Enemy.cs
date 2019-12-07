using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform child;
    private bool chasing;
    private Transform player;
    public float speed = 5.0f;
    public float range = 2.0f;
    public float attackrate = 4.0f;
    public int attackpower = 2;
    public float health = 20.0f;
    private float attackcooldown = 0.0f;
    Rigidbody rb;
    bool grounded;

    // Use this for initialization
	void Start () {
        child = transform.Find("EnemyRange");
        rb = GetComponent<Rigidbody>();
        chasing = false;
        GetComponent<Renderer>().enabled = false;
        grounded = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (grounded) {
            GetComponent<Renderer>().enabled = true;
        }
        if (chasing) {
            chasePlayer();
        }
        else {
            //idle
        }
	}

    public void colliderEntered(GameObject p) {
        Debug.Log("player!!");
        chasing = true;
        player = p.transform;
    }

    public void colliderExited( )
    {
        chasing = false;
        player = null;
    }

    private void chasePlayer() {
        transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
        if (Vector3.Distance(transform.position, player.position) > range)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, speed * Time.deltaTime));
        else {
            //stop moving, attack
            //transform.Translate(new Vector3(0, 0, 0));
            if (Time.time >= attackcooldown) {
                Debug.Log("attack!!");
                transform.Translate(new Vector3(0, 10.0f * Time.deltaTime, 0));
                player.GetComponent<Player>().healthMod(attackpower * -1);
                attackcooldown = Time.time + attackrate;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    public void isAttacked(float damage) {
        Debug.Log("enemy attacked!");
        float kbforce = Random.Range(5.0f, 10.0f);
        GetComponent<Rigidbody>().AddForce(new Vector3(-transform.forward.x * kbforce, 5, -transform.forward.z * kbforce), ForceMode.Impulse);
        health = health - damage;
        Debug.Log("health = " + health);
        if (health <= 0.0f) {
            die();
        }
    }

    private void die() {
        Destroy(gameObject);
    }
}
