using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Player : MonoBehaviour {

	CharacterController characterController;
	public float speed = 5.0f;
	public float gravity = 9.8f;
	private Vector3 direction = Vector3.zero;
    public float mouseSens = 2.0f;
    public Weapon weapon;
    private Dictionary<string, WeaponInfo> weapons;
    private bool acceptInput;
    public int health;
    private int maxHealth;
    private int points = 300;
    public UI uiMananger;
    private float s1CD, s2CD, s3CD;
    private bool s1Used, s2Used, s3Used;
    public WeaponSelect weaponManager;
    private AudioSource sfx;

    private bool weapEquipped;
	// Use this for initialization
	void Start () {
        maxHealth = health;
        s1CD = 0;
        s2CD = 0;
        s3CD = 0;
		characterController = GetComponent<CharacterController>();
        sfx = GetComponent<AudioSource>();
        acceptInput = true;
        // force the resolution
        Screen.SetResolution (1920 , 1080, true, 60);
	}

	// Update is called once per frame
	void Update () {
        // Debug.Log("position X: " + transform.position.x + " || position y: " + transform.position.y + " || position z: " + transform.position.z);
        if (!weapEquipped){
            weapEquipped = true;
            changeWeapon("Wood Stick");
        }
        if (s1Used) {
                s1CD -= Time.deltaTime;
                if (s1CD <= 0) {
                    s1Used = false;
                    uiMananger.updateSkillBar(0, false);
                }
            }
        if (s2Used) {
            s2CD -= Time.deltaTime;
            if (s2CD <= 0) {
                s2Used = false;
                uiMananger.updateSkillBar(1, false);
            }
        }
        if (s3Used) {
            s3CD -= Time.deltaTime;
            if (s3CD <= 0) {
                s3Used = false;
                uiMananger.updateSkillBar(2, false);
            }
        }
        if (acceptInput) {
            checkAttack();
            if (characterController.isGrounded){
                direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                direction *= speed;
                if (Input.GetKeyUp(KeyCode.Space)) {
                    direction.y = speed;
                }
            } else {
                direction = new Vector3(Input.GetAxis("Horizontal"), direction.y,
                Input.GetAxis("Vertical"));
                direction.x *= speed;
                direction.z *= speed;
            }

            direction.y -= gravity * Time.deltaTime;
            direction = transform.TransformDirection(direction);
            if(Input.GetAxis("Mouse X") < 0)
                transform.Rotate(Vector3.up * -mouseSens);
            if(Input.GetAxis("Mouse X") > 0)
                transform.Rotate(Vector3.up * mouseSens);
            characterController.Move(direction * Time.deltaTime);
        }
	}

    void checkAttack() {
        if (Input.GetKeyUp(KeyCode.Q) && !s1Used) {
            weapon.Attack(1);
            s1CD = weapon.getSkills()[0].getCurrent().getSpeed();
            s1Used = true;
            uiMananger.updateSkillBar(0, true, s1CD);
            weapon.playSFX(0);
        } else if (Input.GetKeyUp(KeyCode.E) && !s2Used) {
            weapon.Attack(2);
            s2CD = weapon.getSkills()[1].getCurrent().getSpeed();
            s2Used = true;
            uiMananger.updateSkillBar(1, true, s2CD);
            weapon.playSFX(1);
        } else if (Input.GetKeyUp(KeyCode.R) && !s3Used) {
            weapon.Attack(3);
            s3CD = weapon.getSkills()[2].getCurrent().getSpeed();
            s3Used = true;
            uiMananger.updateSkillBar(2, true, s3CD);
            weapon.playSFX(2);
        }
    }

    public void setMovable(bool val) {
        acceptInput = val;
    }

    public void healthMod(int val) {
        health += val;
        if (health <= 0) {
            SceneManager.LoadScene("GameOver");
        }
        sfx.Play();
        uiMananger.updatePlayerHP(health, maxHealth);
    }
    
    public void changeWeapon(string weaponName) {
        weapons = weaponManager.GetWeapons();
        weapon.updateWeapon(weapons[weaponName]);
        weaponChanged();
        GameObject.Find("WeaponInfoView").GetComponent<WeaponInfoUI>().refresh();
    }

    void weaponChanged() {
        List<WeaponSkill> skills = weapon.getSkills();
        string[] paths = {skills[0].getPath(), skills[1].getPath(), skills[2].getPath()};
        uiMananger.updateSkillIcons(paths);
    }

    public void addPoints(int newPoints) {
        points += newPoints;
        uiMananger.updatePoints(points);
    }

    public int getPoints() {
        return points;
    }

    public void usePoints(int pointsUsed) {
        points -= pointsUsed;
        uiMananger.updatePoints(points);
    }

    public int getHealth() {
        return health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public void healHP(int amount) {
        switch (amount) {
            case 25:
                health = Math.Min(maxHealth, health + (maxHealth / 4));
                break;
            case 50:
                health = Math.Min(maxHealth, health + (maxHealth / 1));
                break;
            case 100:
                health = maxHealth;
                break;
        }
        uiMananger.updatePlayerHP(health, maxHealth);
    }
}