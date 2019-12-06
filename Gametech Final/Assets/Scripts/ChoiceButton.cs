using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    // Start is called before the first frame update
    private string choiceFile;
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
        npc.branchConversation();
    }

    public void setChoiceFile(string file) {
        choiceFile = file;
    }
}
