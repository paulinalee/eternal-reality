using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    int currentwave = 1;
    int currentround = 1;
    public int enemiesperwave = 10;
    int currentenemies = 0;
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentenemies >= enemiesperwave) {
            Debug.Log("WAVE OVER");
            if (transform.Find("Enemies").childCount == 0) {
                Debug.Log("ALL ENEMIES KILLED");
                currentwave += 1;
                currentenemies = 0;
                if (currentwave > 3) {
                    Debug.Log("ROUND OVER");
                    //do round end stuff
                    currentround += 1;
                    currentwave = 1;
                    enemiesperwave += 5;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ENTERED!!!");
            //game start! begin spawning
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public int getCurrentEnemies() {
        return currentenemies;
    }

    public void addEnemies() {
        currentenemies += 1;
    }
}
