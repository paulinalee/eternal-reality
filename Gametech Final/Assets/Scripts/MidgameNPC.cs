using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MidgameNPC : NPC
{
    // Start is called before the first frame update
    bool showWeapons, showHeal, showUpgrades, startNextRound;
    string originalFileName;
    public override void Start()
    {
        base.Start();
        originalFileName = fileName;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void beginConversation() {
        changeSpeechFile(originalFileName);
        base.beginConversation();
    }

    public override void advanceConversation() {
        if (!stopAdvancing) {
            string nextLine = fileReader.ReadLine();
            if (string.IsNullOrEmpty(nextLine)) {
                if (showWeapons) {
                    stopAdvancing = true;
                    uiManager.displayWeapons();
                    showWeapons = false;
                } else if (showHeal) {
                    stopAdvancing = true;
                    uiManager.displayHeals();
                    showHeal = false;
                } else if (showUpgrades) {
                    stopAdvancing = true;
                    uiManager.displayUpgrades();
                    showUpgrades = false;
                } else {
                    if (startNextRound) {
                        stopAdvancing = false;
                        uiManager.showReadyMsg();
                        SpawnController spawnController = GameObject.Find("SpawnZone").GetComponent<SpawnController>();
                        spawnController.prepareNextRound();
                    }
                    endConversation(); // this lets players do multiple things between rounds by talking to npc again

                }
            } else {
                if (nextLine.StartsWith("||OPTIONS")) {
                    processBranches();
                } else {
                    uiManager.setSpeech(nextLine);
                }
            }
        }
     
    }

    public override void resetVariables() {
        showWeapons = false;
        showHeal = false;
    }

    public void toggleWeaponScreenOn() {
        showWeapons = true;
    }

    public void toggleHealScreenOn() {
        showHeal = true;
    }

    public void toggleUpgradeScreenOn() {
        showUpgrades = true;
    }

    public void toggleStartRound() {
        startNextRound = true;
    }
}
