using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Weapon : MonoBehaviour {

	public string weaponName, weaponDescription, type, imagePath;
	private List<WeaponSkill> skills;
	// Use this for initialization
	void Start () {
		weaponName = "Wood Stick";
		weaponDescription = "A fragile stick from an old tree.";
		WeaponSkill skill1 = new WeaponSkill("Whack", "Quick hit with a stick", "abcd.efg", 10, 5, 1);
		WeaponSkill skill2 = new WeaponSkill("Swing", "Powerful hit", "abcd.efg", 13, 10, 1);
		WeaponSkill skill3 = new WeaponSkill("Bonk", "Chaotic energy", "abcd.efg", 15, 3, 1);
		skills = new List<WeaponSkill>();
		skills.Add(skill1);
		skills.Add(skill2);
		skills.Add(skill3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string getName() {
		return weaponName;
	}

	public string getDescription() {
		return weaponDescription;
	}

	public List<WeaponSkill> getSkills() {
		return skills;
	}

	public void updateWeapon(WeaponInfo newWeapon) {
		weaponName = newWeapon.name;
		weaponDescription = newWeapon.description;
		imagePath = newWeapon.imgpath;
		skills = new List<WeaponSkill>();
		foreach (SkillInfo skillInfo in newWeapon.skills) {
			List<SkillLevel> levels = new List<SkillLevel>();
			foreach(LevelInfo levelInfo in skillInfo.levels) {
				levels.Add(new SkillLevel(levelInfo.power, levelInfo.range, levelInfo.speed));
			}
			skills.Add(new WeaponSkill(skillInfo.name, skillInfo.description, skillInfo.imgpath, levels));
		}
		Debug.Log("weapon swapped");
	}
}
