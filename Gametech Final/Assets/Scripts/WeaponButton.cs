using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tooltipPanel, player;
    private AudioSource sfx;
    void Start()
    {
        player = GameObject.Find("Player");
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWeapon() {
        Text buttonText = this.transform.GetChild(0).GetComponent<Text>();
        player.GetComponent<Player>().changeWeapon(buttonText.text);
        GameObject npcObject = GameObject.Find("NPC");
        Canvas WeaponDisplay = GameObject.Find("WeaponSelectView").GetComponent<Canvas>();
        WeaponDisplay.enabled = false;
        if (npcObject.GetComponent<WeaponNPC>()) {
            npcObject.GetComponent<WeaponNPC>().toggleWeaponSelected(true);
        }

        NPC npc = npcObject.GetComponent<NPC>();
        npc.changeSpeechFile("ready");
        npc.ContinueAdvancing();
        npc.branchConversation();
        sfx.Play();
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
