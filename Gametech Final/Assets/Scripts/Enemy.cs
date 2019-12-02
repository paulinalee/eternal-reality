using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform child;
    private bool chasing;
    private Transform player;
    public float speed;

    // Use this for initialization
	void Start () {
        child = transform.Find("EnemyRange");
        chasing = false;
        speed = 5.0f;
    }
	
	// Update is called once per frame
	void Update () {
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
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, speed * Time.deltaTime));
    }
}
