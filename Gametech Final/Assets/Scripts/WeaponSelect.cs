using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponSelect : MonoBehaviour {

	private string path;
	private List<WeaponInfo> weapons;
	// Use this for initialization
	void Start () {
		path = Application.dataPath + "/Weapons/";
		weapons = new List<WeaponInfo>();
		ReadFromFolder();
	}
	
	void ReadFromFolder() {
		DirectoryInfo dir = new DirectoryInfo(path);
		FileInfo[] info = dir.GetFiles("*.json");
		foreach (FileInfo f in info) {
			string contents = File.ReadAllText(f.FullName);
			WeaponInfo weapon = JsonUtility.FromJson<WeaponInfo>(contents);
			weapons.Add(weapon);
		}
	}

	public List<WeaponInfo> GetWeapons() {
		return weapons;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
