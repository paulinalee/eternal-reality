using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool interactable, inRange, inConversation, stopAdvancing;
    protected Player player;

    protected string speechFile;

    protected StreamReader fileReader;

    public UI uiManager;
    public string fileName;
    
    public virtual void Start()
    {  
        interactable = false;
        inConversation = false;
        player = GameObject.Find("Player").GetComponent<Player>();
        if (string.IsNullOrEmpty(speechFile)) {
            speechFile = Application.streamingAssetsPath + "/NPC/" + fileName + ".txt";
        }
    }

    public void changeSpeechFile(string newFile) {
        speechFile = Application.streamingAssetsPath + "/NPC/" + newFile + ".txt";
        fileReader = new StreamReader(speechFile);
    }

    public virtual void advanceConversation() {
        if (!stopAdvancing) {
            string nextLine = fileReader.ReadLine();
            if (string.IsNullOrEmpty(nextLine)) {
                endConversation();
            } else {
                if (nextLine.StartsWith("||OPTIONS")) {
                    processBranches();
                } else {
                    uiManager.setSpeech(nextLine);
                }
            }
        }
        
    }

    public virtual void beginConversation() {
        player.setMovable(false);
        stopAdvancing = false;
        fileReader = new StreamReader(speechFile);
        uiManager.resetDisplays();
        inConversation = true;
        advanceConversation();
    }

    public void endConversation() {
        player.setMovable(true);
        uiManager.hideDialogueBox();
        inConversation = false;
        resetVariables();
    }

    public virtual void resetVariables() {

    }
   public void processBranches() {
       // TODO: error handling when you heck up
       stopAdvancing = true;
       string nextLine = fileReader.ReadLine();
        List<string> choices = new List<string>();
        while (!nextLine.StartsWith("||BRANCHFILES")) {
            choices.Add(nextLine);
            nextLine = fileReader.ReadLine();
        }
        nextLine = fileReader.ReadLine(); // read the "||BRANCHFILES" line

        // reading all the branchfiles
        List<string> branchFiles = new List<string>();
        while (!nextLine.StartsWith("||METADATA")) {
            branchFiles.Add(nextLine);
            nextLine = fileReader.ReadLine();
        }
        int min = Math.Min(choices.Count, branchFiles.Count);
        Dictionary<string, string> choiceToFileMap = new Dictionary<string, string>();
        for (int i = 0; i < min; i++) {
            choiceToFileMap[choices[i]] = branchFiles[i];
        }

        // reading from metadata to end of file and mapping
        // metadata is used only when buttons are supposed to lead to more than just dialogue (i.e. transition the ui screens)
        nextLine = fileReader.ReadLine(); // read the "||METADATA" line
        List<string> metadata = new List<string>();
        while (!nextLine.StartsWith("||ENDCHOICE")) {
            metadata.Add(nextLine);
            nextLine = fileReader.ReadLine();
        }
        Dictionary<string, string> choiceMetadata = new Dictionary<string, string>();
        for (int i = 0; i < metadata.Count; i++) {
            choiceMetadata[choices[i]] = metadata[i];
        }
        uiManager.displayChoices(choiceToFileMap, choiceMetadata);
    }

    public void branchConversation() {
        uiManager.toggleBranchingDialogue();
        advanceConversation();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            endConversation();
        } else {
            if (interactable) {
                if (Input.GetKeyUp(KeyCode.F) && !inConversation) {
                    beginConversation();
                } else if (inConversation) {
                    if (Input.GetKeyUp(KeyCode.F)) {
                        advanceConversation();
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {  
        if (other.GetComponent<Player>() != null) {
            interactable = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null) {
            interactable = false;
        } 
    }

    public void ContinueAdvancing()
    {
        stopAdvancing = false;
    }
}
