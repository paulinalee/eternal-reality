using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkill {

	private int skillLevel, maxLevel;
	private List<SkillLevel> levels;
	private SkillLevel activeSkill;
	public string skillName, skillDescription, imagePath;
	private int currentIndex, nextIndex;

	public WeaponSkill(string name, string description, string path, int basePower, int baseSpeed, int baseRange) {
		skillName = name;
		skillDescription = description;
		imagePath = path;
		levels = new List<SkillLevel>();
		SkillLevel level1 = new SkillLevel(basePower, baseSpeed, baseRange);
		activeSkill = level1;
		currentIndex = 0;
		nextIndex = currentIndex + 1;
		levels.Add(level1);

		// for testing, comment in/out to test max/nonmax skill UI
		// SkillLevel level2 = new SkillLevel(basePower + 5, baseSpeed + 5, baseRange + 5);
		// levels.Add(level2);
		maxLevel = levels.Count;
	}

	public WeaponSkill(string name, string description, string path, List<SkillLevel> lvls) {
		skillName = name;
		skillDescription = description;
		imagePath = path;
		levels = lvls;
		Debug.Log("weap levels: " + levels.Count);
		maxLevel = levels.Count;
		currentIndex = 0;
		nextIndex = currentIndex + 1;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string getName() {
		return skillName;
	}

	public string getDescription() {
		return skillDescription;
	}

	public string getPath() {
		return imagePath;
	}

	public void upgrade() {
		if (!isMaxed()) {
			currentIndex = nextIndex;
			nextIndex++;
			activeSkill = levels[currentIndex];
		}
	}

	public bool isMaxed() {
		return nextIndex >= levels.Count;
	}

	public SkillLevel getNext() {
		return levels[nextIndex];
	}

	public SkillLevel getCurrent() {
		return levels[currentIndex];
	}
}
