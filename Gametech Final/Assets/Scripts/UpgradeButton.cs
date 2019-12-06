using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void upgradeOnClick() {
        // find the parent of the button (the skill) then find the sibling index to determine what skill # to upgrade

        UpgradeUI UI = GameObject.Find("UpgradeScreen").GetComponent<UpgradeUI>();
        int skillNumber = this.transform.parent.GetSiblingIndex();
        UI.upgradeSkill(skillNumber);
    }
}
