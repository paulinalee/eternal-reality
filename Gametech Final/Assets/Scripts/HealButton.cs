using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealButton : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    Button button;
    public int pointsRequired;
    public int healAmount;

    private AudioSource sfx;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        button = GetComponent<Button>();
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void refresh() {
        if (player.getPoints() < pointsRequired) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }
    public void onHealClick() {
        player.usePoints(pointsRequired);
        player.healHP(healAmount);
        NPC npc = GameObject.Find("NPC").GetComponent<NPC>();
        npc.changeSpeechFile("healFinish");
        npc.ContinueAdvancing();
        npc.branchConversation();
        sfx.Play();
    }
}
