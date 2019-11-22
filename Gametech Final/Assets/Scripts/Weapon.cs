using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Weapon : MonoBehaviour {

	public string weaponName, weaponDescription, type, imagePath;
	public List<WeaponSkill> skills;
	// Use this for initialization
	void Start () {
		skills = new List<WeaponSkill>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateFromFile(string filepath) {
		
	}
}
