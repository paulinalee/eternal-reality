using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    // Start is called before the first frame update
    private string choiceFile;
    private bool toWeaponScreen, toHealScreen, toUpgradeScreen, skipToNextRound;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        NPC npc = GameObject.Find("NPC").GetComponent<NPC>();
        npc.changeSpeechFile(choiceFile);
        evaluateMetadata();
        npc.ContinueAdvancing();
        npc.branchConversation();
    }

    public void setChoiceFile(string file) {
        choiceFile = file;
    }

    public void midgameShowWeapons() {
        toWeaponScreen = true;
    }

    public void midgameShowHeal() {
        toHealScreen = true;
    }

    public void midgameShowUpgrade() {
        toUpgradeScreen = true;
    }

    public void midgameStartNewRound() {
        skipToNextRound = true;
    }
    
    void evaluateMetadata() {
        MidgameNPC npc = GameObject.Find("NPC").GetComponent<MidgameNPC>();
        if (toWeaponScreen) {
            npc.toggleWeaponScreenOn();
        }

        if (toHealScreen) {
            npc.toggleHealScreenOn();
        }

        if (toUpgradeScreen) {
            npc.toggleUpgradeScreenOn();
        }

        if (skipToNextRound) {
            npc.toggleStartRound();
        }
    }
}
