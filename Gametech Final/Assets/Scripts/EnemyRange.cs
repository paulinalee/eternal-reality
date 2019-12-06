using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

    GameObject player;
    Enemy parent;

	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject.GetComponent<Enemy>();
        parent.colliderEntered(GameObject.FindWithTag("Player"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player") {
            parent.colliderEntered(other.gameObject);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.tag == "Player")
        {
            parent.colliderExited();
        }*/
    }
}
