using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkill {

	private int skillLevel, maxLevel;
	private List<SkillLevel> levels;
	private SkillLevel currentLevel;
	public string skillName, skillDescription, imagePath;

	public WeaponSkill(string name, string description, string path, int basePower, int baseSpeed, int baseRange) {
		skillName = name;
		skillDescription = description;
		imagePath = path;
		levels = new List<SkillLevel>();
		SkillLevel level1 = new SkillLevel(basePower, baseSpeed, baseRange);
		levels.Add(level1);
		maxLevel = levels.Count;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getPower() {
		return currentLevel.getPower();
	}

	public int getSpeed() {
		return currentLevel.getSpeed();
	}

	public int getRange() {
		return currentLevel.getRange();
	}
}
