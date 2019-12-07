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
    private bool roundEnded, pointSpendingDone;
    public float timeBetweenRounds = 5f;
    private float preRoundTimer, teleportTimer; // teleport = timer before player gets teleported to npc, preround = timer before new round starts
    public MidgameNPC npc;
    private UI uiManager;
    void Start()
    {
        uiManager = GameObject.Find("UI").GetComponent<UI>();
        preRoundTimer = timeBetweenRounds;
        enemiesperwave = 2; // remove this when youre done debugging!!!!!
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pointSpendingDone) {
            preRoundTimer -= Time.deltaTime;
            if (preRoundTimer <= 0.0f) {
                startNextRound();
            }
        }
        if (currentenemies >= enemiesperwave) {
            //Debug.Log("WAVE OVER");
            if (transform.Find("Enemies").childCount == 0) {
                currentwave += 1;
                currentenemies = 0;
                if (currentwave > 1) { // swap this back to 3 when done debugging
                    Debug.Log("=====================ROUND OVER");
                    endRound();
                }
            }
        }
    }
    void endRound() {
        roundEnded = true;
        // deactivate spawn points
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        uiManager.toggleRoundStatus(true);
        uiManager.showRoundEndMsg();
    }

    void resetPlayer() {
        // change player location to be near NPC
        showNPC();
    }

    void startNextRound() {
        currentround += 1;
        currentwave = 1;
        enemiesperwave += 5;
        roundEnded = false;
        // reactivate spawn points
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void resetpreRoundTimer() {
        preRoundTimer = timeBetweenRounds;
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

    void showNPC() {
        npc.gameObject.SetActive(true);
        npc.changeSpeechFile("roundEndLowHP");
    }
}
