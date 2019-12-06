using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tooltipPanel, player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWeapon() {
        Text buttonText = this.transform.GetChild(0).GetComponent<Text>();
        player.GetComponent<Player>().changeWeapon(buttonText.text);
        WeaponNPC npc = GameObject.Find("NPC").GetComponent<WeaponNPC>();
        Canvas WeaponDisplay = GameObject.Find("WeaponSelectView").GetComponent<Canvas>();
        WeaponDisplay.enabled = false;
        npc.toggleWeaponSelected(true);
        npc.toggleSelectionMode(false);
        npc.changeSpeechFile("ready");
        npc.branchConversation();
    }

    public void showTooltip() {
        tooltipPanel = this.transform.Find("Tooltip").gameObject;
        tooltipPanel.GetComponent<Canvas>().enabled = true;
    }

    public void hideTooltip() {
        tooltipPanel = this.transform.Find("Tooltip").gameObject;
        tooltipPanel.GetComponent<Canvas>().enabled = false;
    }
}
