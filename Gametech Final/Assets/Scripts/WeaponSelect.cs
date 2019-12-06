using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponSelect : MonoBehaviour {

	private string path;
	private List<WeaponInfo> weaponsList;
	private Dictionary<string, WeaponInfo> weapons;
	private Dictionary<string, int[]> weaponUpgrades;
	// Use this for initialization
	void Start () {
		path = Application.streamingAssetsPath + "/Weapons/";
		weapons = new Dictionary<string, WeaponInfo>();
		weaponsList = new List<WeaponInfo>();
		weaponUpgrades = new Dictionary<string, int[]>();
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

	public Dictionary<string, int[]> GetUpgrades() {
		return weaponUpgrades;
	}

	public void saveUpgradeState(string weaponName, int skillNumber, int level) {

		if (!weaponUpgrades.ContainsKey(weaponName)) {
			// no prev state saved, save new state
			weaponUpgrades[weaponName] = new int[3];
		}
		
		weaponUpgrades[weaponName][skillNumber] = level;
		Debug.Log("upgrade saved for " + weaponName + " skillNumber: " + skillNumber.ToString() + " level: " + level.ToString());
	}

	public bool alreadyUpgraded(string name) {
		return weaponUpgrades.ContainsKey(name);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
