using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    int currentwave = 1;
    int currentround = 1;
    public int enemiesperwave = 10;
    int currentenemies = 0;
    private bool roundEnded, pointSpendingDone;
    public double timeBetweenRounds = 5f;
    private double preRoundTimer, teleportTimer; // teleport = timer before player gets teleported to npc, preround = timer before new round starts
    public MidgameNPC npc;
    private UI uiManager;
    void Start()
    {
        uiManager = GameObject.Find("UI").GetComponent<UI>();
        teleportTimer = 5f;
        preRoundTimer = timeBetweenRounds;
        enemiesperwave = 10; // remove this when youre done debugging!!!!!
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roundEnded) {
            teleportTimer -= Time.deltaTime;
            uiManager.updateTimer(Convert.ToInt32(Math.Ceiling(teleportTimer)));
            if (teleportTimer <= 0.0f) {
                resetPlayer();
            }
        }
        if (pointSpendingDone) {
            preRoundTimer -= Time.deltaTime;
            uiManager.updateTimer(Convert.ToInt32(Math.Ceiling(preRoundTimer)));
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
        teleportTimer = 5f;
    }

    void resetPlayer() {
        // change player location to be near NPC
        roundEnded = false;
        showNPC();
        GameObject.Find("Player").transform.SetPositionAndRotation(new Vector3(-1.15f, .8f, -55.8f), new Quaternion()); // fix this to not be literally on the npc later
        uiManager.toggleRoundStatus(false);
    }

    void startNextRound() {
        currentround += 1;
        pointSpendingDone = false;
        currentwave = 1;
        enemiesperwave += 5;
        roundEnded = false;
        // reactivate spawn points
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        uiManager.toggleRoundStatus(false);
    }

    public void prepareNextRound() {
        pointSpendingDone = true;
        preRoundTimer = timeBetweenRounds;
        npc.gameObject.SetActive(false);
        uiManager.toggleRoundStatus(true);
        uiManager.showReadyMsg();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
        Player player = GameObject.Find("Player").GetComponent<Player>();
        int playerHealth, maxHealth;
        playerHealth = player.getHealth();
        maxHealth = player.getMaxHealth();
        if (playerHealth >= (maxHealth * 3/4)) {
            Debug.Log("ended round with high HP");
            npc.changeSpeechFile("roundEndHighHP");
        } else if (playerHealth >= (maxHealth * 1/4)) {
            Debug.Log("ended round with ok HP");
            npc.changeSpeechFile("roundEndMidHP");
        } else {
            Debug.Log("ended round with low hp");
            npc.changeSpeechFile("roundEndLowHP");
        }
    }
}
