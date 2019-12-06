using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    public Weapon weapon;
    public GameObject weaponName, description;
    private List<GameObject> skills;
    private List<WeaponSkill> weaponSkills;
    void Start()
    {
        weaponName = GameObject.Find("WeaponName");
        description = GameObject.Find("WeaponDescription");
        skills = new List<GameObject>();
        GameObject skill1 = GameObject.Find("Skill1");
        GameObject skill2 = GameObject.Find("Skill2");
        GameObject skill3 = GameObject.Find("Skill3");
        skills.Add(skill1);
        skills.Add(skill2);
        skills.Add(skill3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T)) {
            Refresh();
            canvas.enabled = !canvas.enabled;
        }
    }

    void Refresh() {
        weaponName.GetComponent<Text>().text = weapon.getName();
        description.GetComponent<Text>().text = weapon.getDescription();
        weaponSkills = weapon.getSkills();
        for(int i = 0; i < 3; i++) {
            updateSkill(skills[i], weaponSkills[i]);
        }
    }

    void updateSkill(GameObject skillDisplay, WeaponSkill weaponSkill) {
        Text name = skillDisplay.transform.Find("Name").GetComponent<Text>();
        name.text = weaponSkill.getName();

        Text description = skillDisplay.transform.Find("Description").GetComponent<Text>();
        description.text = weaponSkill.getDescription();

        // Current stats
        updateStats(skillDisplay, "Current", weaponSkill.getCurrent());

        if (weaponSkill.isMaxed()) {
            skillDisplay.transform.Find("Next").GetComponent<Canvas>().enabled = false;
            skillDisplay.transform.Find("Max").GetComponent<Canvas>().enabled = true;
            skillDisplay.transform.Find("Button").GetComponent<Button>().interactable = false;
        } else {
            // make sure everything is displaying even if you change weapons
            skillDisplay.transform.Find("Next").GetComponent<Canvas>().enabled = true;
            skillDisplay.transform.Find("Max").GetComponent<Canvas>().enabled = false;
            skillDisplay.transform.Find("Button").GetComponent<Button>().interactable = true;
            // update the next skill values
            updateStats(skillDisplay, "Next", weaponSkill.getNext());
        }
    }

    void updateStats(GameObject skillDisplay, string section, SkillLevel level) {
        Text power = skillDisplay.transform.Find(section + "/Power/PowerVal").GetComponent<Text>();
        power.text = level.getPower().ToString();

        Text speed = skillDisplay.transform.Find(section + "/Speed/SpeedVal").GetComponent<Text>();
        speed.text = level.getSpeed().ToString();

        Text range = skillDisplay.transform.Find(section + "/Range/RangeVal").GetComponent<Text>();
        range.text = level.getRange().ToString();
    }

    public void upgradeSkill(int skillNumber) {
        weaponSkills[skillNumber].upgrade();
        Debug.Log("upgraded skill " + skillNumber.ToString());
        Refresh();
    }
}
