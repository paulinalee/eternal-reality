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
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        button = GetComponent<Button>();
        if (player.getPoints() < pointsRequired) {
            button.interactable = false;
            // todo later: make text less opaque when button is disabled
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHealClick() {
        player.usePoints(pointsRequired);
        NPC npc = GameObject.Find("NPC").GetComponent<NPC>();
        npc.changeSpeechFile("healFinish");
        npc.ContinueAdvancing();
        npc.branchConversation();
    }
}
