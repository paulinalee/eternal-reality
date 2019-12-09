using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponInfoUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Canvas weaponInfoDisplay;
    public Text wName, wDesc, s1Name, s2Name, s3Name, s1Stats, s2Stats, s3Stats;
    private bool show;
    private Player player;
    void Start()
    {
        weaponInfoDisplay = GameObject.Find("WeaponInfoView").GetComponent<Canvas>();
        player = GameObject.Find("Player").GetComponent<Player>();
        show = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (show && Input.GetKeyUp(KeyCode.T)) {
            if (!weaponInfoDisplay.enabled) {
                refresh();
            }
            weaponInfoDisplay.enabled = !weaponInfoDisplay.enabled;
        }
    }

    public void toggleInteractable(bool val) {
        show = val;
    }
    
    public void refresh() {
        wName.text = player.weapon.getName();
        wDesc.text = player.weapon.getDescription(); 
        List<WeaponSkill> skills = player.weapon.getSkills();
        s1Name.text = skills[0].getName() + " LV. " + skills[0].getLevel().ToString();
        s2Name.text = skills[1].getName() + " LV. " + skills[1].getLevel().ToString();
        s3Name.text = skills[2].getName() + " LV. " + skills[2].getLevel().ToString();

        s1Stats.text = skills[0].getDescription() + " <PWR " + skills[0].getCurrent().getPower() + 
            ", SPD " + skills[0].getCurrent().getSpeed() + ", RNG " + skills[0].getCurrent().getRange() + ">";
        
        s2Stats.text = skills[1].getDescription() + " <PWR " + skills[1].getCurrent().getPower() + 
            ", SPD " + skills[1].getCurrent().getSpeed() + ", RNG " + skills[1].getCurrent().getRange() + ">";

        s3Stats.text = skills[2].getDescription() + " <PWR " + skills[2].getCurrent().getPower() + 
            ", SPD " + skills[2].getCurrent().getSpeed() + ", RNG " + skills[2].getCurrent().getRange() + ">";
    }

}
