using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponSelect : MonoBehaviour {

	private string path;
	private List<WeaponInfo> weaponsList;
	private Dictionary<string, WeaponInfo> weapons;
	// Use this for initialization
	void Start () {
		path = Application.dataPath + "/Weapons/";
		weapons = new Dictionary<string, WeaponInfo>();
		weaponsList = new List<WeaponInfo>();
		ReadFromFolder();
		mapWeapons();
	}
	
	void ReadFromFolder() {
		DirectoryInfo dir = new DirectoryInfo(path);
		FileInfo[] info = dir.GetFiles("*.txt");
		foreach (FileInfo f in info) {
			string contents = File.ReadAllText(f.FullName);
			WeaponInfo weapon = JsonUtility.FromJson<WeaponInfo>(contents);
			weaponsList.Add(weapon);
			Debug.Log(weapon.name);
		}
	}
	
	void mapWeapons() {
		foreach (WeaponInfo weapon in weaponsList) {
			weapons[weapon.name] = weapon;
		}
	}
	public List<WeaponInfo> GetWeaponsList() {
		return weaponsList;
	}

	public Dictionary<string, WeaponInfo> GetWeapons() {
		return weapons;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
