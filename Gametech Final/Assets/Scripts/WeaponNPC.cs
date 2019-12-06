using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class WeaponNPC : NPC
{
    // Start is called before the first frame update
    private Canvas weaponDisplay;
    private Dictionary<string, WeaponInfo> weapons;
    private GameObject weaponContainer;
    public GameObject weaponButtonPrefab;
    private bool inSelectionMode, weaponSelected;

    public override void Start()
    {  
        base.Start();
        weaponDisplay = GameObject.Find("WeaponSelectView").GetComponent<Canvas>();
        weaponContainer = GameObject.Find("WeaponSelectContainer/Viewport/Content");
    }

    // Update is called once per frame
    public override void Update()
    {
        if (interactable) {
            if (Input.GetKeyUp(KeyCode.F) && !inConversation) {
                inSelectionMode = false;
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

    public override void advanceConversation() {
        // check if we are at a choice, if so set up the UI
        string nextLine = fileReader.ReadLine();
        if (string.IsNullOrEmpty(nextLine)) {
            if (!inSelectionMode && !weaponSelected) {
                // have yet to select a weapon
                displayWeapons();
            } else if (weaponSelected) {
                endConversation();
            }
        } else {
            if (nextLine.StartsWith("||OPTIONS")) {
                processBranches();
            } else {
                speechText.text = nextLine;
            }
        }
    }

        
    public override void resetDisplays() {
        // conversations should begin with straight dialogue, never only choice buttons or weap buttons
        base.resetDisplays();
        weaponDisplay.enabled = false;
    }

    void displayChoices() {
        Debug.Log("displaying choices!");
    }
    void displayWeapons() {
        inSelectionMode = true;
        speechDisplay.enabled = false;
        weaponDisplay.enabled = true;
        weapons = GameObject.Find("WeaponSelect").GetComponent<WeaponSelect>().GetWeapons();
        foreach(KeyValuePair<string, WeaponInfo> entry in weapons) {
            GameObject newButton = Instantiate(weaponButtonPrefab);
            newButton.transform.parent = weaponContainer.transform;
            Text skillName = newButton.transform.Find("Name").GetComponent<Text>();
            skillName.text = entry.Value.name;

            Text skillDesc = newButton.transform.Find("Description").GetComponent<Text>();
            skillDesc.text = entry.Value.description;

            for(int i = 0; i < 3; i++) {
                Text skillText = newButton.transform.Find("Tooltip").GetChild(i).GetComponent<Text>();
                SkillInfo skillInfo = entry.Value.skills[i];
                LevelInfo baseLevel = skillInfo.levels[0];
                string stats = "PWR: " + baseLevel.power + "   SPD: " + baseLevel.speed + "   RNG: " + baseLevel.range;
                string skillWithStats = string.Join(Environment.NewLine, skillInfo.name, stats);
                skillText.text = skillWithStats;
            }
        }
    }

    public void toggleWeaponSelected(bool val) {
        weaponSelected = val;
    }
    
    public void toggleSelectionMode(bool val) {
        inSelectionMode = val;
    }
    public void clickChoice() {
       // Debug.Log();
    }

}
