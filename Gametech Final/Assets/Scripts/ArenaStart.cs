using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStart : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Debug.Log("Player entered arena!");
            boxCollider.enabled = false;
            WeaponNPC oldNPC = GameObject.Find("NPC").GetComponent<WeaponNPC>();
            oldNPC.delete();
            BoxCollider arenaCollider = GameObject.Find("ArenaWall").GetComponent<BoxCollider>();
            arenaCollider.enabled = true;
            GameObject.Find("UI").GetComponent<UI>().showPoints();
        }
    }
}
