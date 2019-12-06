using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool interactable, inConversation;
    protected Canvas display, choiceDisplay;
    protected Canvas speechDisplay;
    protected Text speechText;
    protected Player player;

    protected string speechFile;

    protected StreamReader fileReader;
    public string fileName;
    public GameObject choiceButtonPrefab, choiceContainer;
    
    public virtual void Start()
    {  
        interactable = false;
        inConversation = false;
        display = GameObject.Find("NPCSpeech").GetComponent<Canvas>();
        player = GameObject.Find("Player").GetComponent<Player>();
        choiceDisplay = GameObject.Find("ChoiceView").GetComponent<Canvas>();
        choiceContainer = GameObject.Find("ChoiceView/ChoiceButtons");
        speechDisplay = GameObject.Find("SpeechView").GetComponent<Canvas>();
        speechText = GameObject.Find("SpeechView/Text").GetComponent<Text>();
        if (string.IsNullOrEmpty(speechFile)) {
            speechFile = Application.dataPath + "/NPC/" + fileName + ".txt";
        }
    }

    public void changeSpeechFile(string newFile) {
        speechFile = Application.dataPath + "/NPC/" + newFile + ".txt";
        Debug.Log("new file path: " + speechFile);
        fileReader = new StreamReader(speechFile);
    }

    public virtual void advanceConversation() {
        string nextLine = fileReader.ReadLine();
        if (string.IsNullOrEmpty(nextLine)) {
            endConversation();
        } else {
            speechText.text = nextLine;
        }
    }
    
    public virtual void resetDisplays() {
        // conversations should begin with straight dialogue, never only choice buttons or weap buttons
        display.enabled = true;
        choiceDisplay.enabled = false;
        speechDisplay.enabled = true;
    }

    public void beginConversation() {
        player.setMovable(false);
        fileReader = new StreamReader(speechFile);
        this.resetDisplays();
        inConversation = true;
        advanceConversation();
    }

    public void endConversation() {
        player.setMovable(true);
        display.enabled = false;
        inConversation = false;
    }

   public void processBranches() {
       // TODO: error handling when you heck up
       string nextLine = fileReader.ReadLine();
        List<string> choices = new List<string>();
        while (!nextLine.StartsWith("||BRANCHFILES")) {
            choices.Add(nextLine);
            Debug.Log("Added choice: " + nextLine);
            nextLine = fileReader.ReadLine();
        }
        nextLine = fileReader.ReadLine(); // read the "||BRANCHFILES" line
        List<string> branchFiles = new List<string>();
        while (!nextLine.StartsWith("||ENDCHOICE")) {
            branchFiles.Add(nextLine);
            nextLine = fileReader.ReadLine();
        }
        int min = Math.Min(choices.Count, branchFiles.Count);
        Dictionary<string, string> choiceToFileMap = new Dictionary<string, string>();
        for (int i = 0; i < min; i++) {
            choiceToFileMap[choices[i]] = branchFiles[i];
        }
        generateChoices(choiceToFileMap);
    }

    void generateChoices(Dictionary<string, string> choiceToFileMap) {
        choiceDisplay.enabled = true;
        foreach(KeyValuePair<string, string> entry in choiceToFileMap) {
            GameObject newButton = Instantiate(choiceButtonPrefab);
            newButton.transform.parent = choiceContainer.transform;
            Text choiceText = newButton.transform.Find("ChoiceText").GetComponent<Text>();
            newButton.GetComponent<ChoiceButton>().setChoiceFile(entry.Value);
            choiceText.text = entry.Key;
            Debug.Log("choice text is: " + choiceText.text);
        }
    }

    public void branchConversation() {
        choiceDisplay.enabled = false;
        speechDisplay.enabled = true;
        advanceConversation();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (interactable) {
            if (Input.GetKeyUp(KeyCode.F) && !inConversation) {
                beginConversation();
            } else if (inConversation) {
                if (Input.GetKeyUp(KeyCode.F)) {
                    advanceConversation();
                } else if (Input.GetKeyUp(KeyCode.Escape)) {
                    endConversation();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {  
        if (other.GetComponent<Player>() != null) {
            Debug.Log("player entered"); 
            interactable = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null) {
            Debug.Log("player exit"); 
            interactable = false;
        } 
    }
}
