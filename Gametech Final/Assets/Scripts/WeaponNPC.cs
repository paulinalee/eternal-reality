using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class WeaponNPC : NPC
{
    // Start is called before the first frame update
    private Dictionary<string, WeaponInfo> weapons;
    private bool inSelectionMode, weaponSelected;

    public override void Start()
    {  
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
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

    public override void advanceConversation() {
        string nextLine = fileReader.ReadLine();
        if (string.IsNullOrEmpty(nextLine)) {
            if (!stopAdvancing && !weaponSelected) {
                // have yet to select a weapon
                uiManager.displayWeapons();
            } else if (weaponSelected) {
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

    void displayChoices() {
        Debug.Log("displaying choices!");
    }

    public void toggleWeaponSelected(bool val) {
        if (!val) {
            Debug.Log("allowing reselect (weapNPC)");
        }
        weaponSelected = val;
    }
    
    public void toggleSelectionMode(bool val) {
        inSelectionMode = val;
    }
    public void clickChoice() {
       // Debug.Log();
    }

    public void delete() {
        Destroy(gameObject);
    }
}
