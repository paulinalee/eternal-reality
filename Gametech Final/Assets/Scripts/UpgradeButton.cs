using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    // Start is called before the first frame update
    private int pointsNeeded;
    private AudioSource sfx;
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPointsNeeded(int val) {
        pointsNeeded = val;
    }
    public void upgradeOnClick() {
        // find the parent of the button (the skill) then find the sibling index to determine what skill # to upgrade

        Player player = GameObject.Find("Player").GetComponent<Player>();
        UpgradeUI UI = GameObject.Find("UpgradeScreen").GetComponent<UpgradeUI>();
        int skillNumber = this.transform.parent.GetSiblingIndex();
        UI.upgradeSkill(skillNumber);
        player.usePoints(pointsNeeded);
        sfx.Play();
    }
}
