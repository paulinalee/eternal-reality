using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MidgameNPC : NPC
{
    // Start is called before the first frame update
    bool showWeapons, showHeal, showUpgrades;
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
                endConversation();
            }
        } else {
            if (nextLine.StartsWith("||OPTIONS")) {
                processBranches();
            } else {
                uiManager.setSpeech(nextLine);
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
}
