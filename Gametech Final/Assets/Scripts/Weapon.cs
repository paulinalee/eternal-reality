using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// using UnityEditor;
public class Weapon : MonoBehaviour {

	public string weaponName, weaponDescription, type, imagePath;
	public WeaponSelect weaponManager;
	public List<WeaponSkill> skills = new List<WeaponSkill>();
    Renderer render;
    public Shader shader;
	// Use this for initialization
	void Start () {
		skills = new List<WeaponSkill>();
        render = transform.GetComponent<Renderer>();
		// weaponName = "Wood Stick";
		// weaponDescription = "A fragile stick from an old tree.";
		// WeaponSkill skill1 = new WeaponSkill("Whack", "Quick hit with a stick", "abcd.efg", 10, 5, 1);
		// WeaponSkill skill2 = new WeaponSkill("Swing", "Powerful hit", "abcd.efg", 13, 10, 1);
		// WeaponSkill skill3 = new WeaponSkill("Bonk", "Chaotic energy", "abcd.efg", 15, 3, 1);
		// skills = new List<WeaponSkill>();
		// skills.Add(skill1);
		// skills.Add(skill2);
		// skills.Add(skill3);
		// Debug.Log(skills.Count);
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
		Dictionary<string, int[]> prevWeaponLevels = weaponManager.GetUpgrades();
		
		weaponName = newWeapon.name;
		bool alreadyUpgraded = weaponManager.alreadyUpgraded(weaponName);
		weaponDescription = newWeapon.description;
		imagePath = newWeapon.imgpath;
        updateImage();
		skills = new List<WeaponSkill>();

		// construct the weapon object
		for(int i = 0; i < 3; i++) {
			SkillInfo skillInfo = newWeapon.skills[i];
			List<SkillLevel> levels = new List<SkillLevel>();
			int[] savedLevels = new int[3];
			if (alreadyUpgraded) {
				savedLevels = prevWeaponLevels[weaponName];
			}

			foreach(LevelInfo levelInfo in skillInfo.levels) {
				levels.Add(new SkillLevel(levelInfo.power, levelInfo.speed, levelInfo.range));
			}

			WeaponSkill newSkill = new WeaponSkill(skillInfo.name, skillInfo.description, skillInfo.imgpath, levels);
			if (alreadyUpgraded) {
				Debug.Log("setting skill " + i + "'s level to " + savedLevels[i]);
				newSkill.setLevel(savedLevels[i]);
			}
			skills.Add(newSkill);
		}
	}

    public void Attack(int skillnum)
    {
		skillnum = skillnum - 1; // for indexing
        WeaponSkill skill = skills[skillnum];
        Collider[] colliders = Physics.OverlapSphere(transform.position, skill.getCurrent().getRange() * 5);
        foreach (Collider c in colliders)
        {
            Debug.Log(c.gameObject.name);
            if (c.gameObject.tag == "Enemy")
            {
                Vector3 enemypos = c.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, enemypos) > 0)
                {
                    //c.gameObject.SetActive(false);
                    Enemy e = c.GetComponent<Enemy>();
                    Debug.Log("power is " + skill.getCurrent().getPower());
                    e.isAttacked(skill.getCurrent().getPower());
                }
            }
        }
	}

    private void updateImage() {
        render.material = new Material(shader);
		string relativePath = imagePath;
		relativePath = imagePath.Replace("Assets/Resources/", "");
		relativePath = relativePath.Replace(".png", "");
        render.material.mainTexture = Resources.Load<Texture>(relativePath);
    }

	public void playSFX(int skillNumber) {
		transform.GetChild(skillNumber).GetComponent<AudioSource>().Play();
	}
}
