using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Weapon : MonoBehaviour {

	public string weaponName, weaponDescription, type, imagePath;
	public List<WeaponSkill> skills;
	// Use this for initialization
	void Start () {
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
}
