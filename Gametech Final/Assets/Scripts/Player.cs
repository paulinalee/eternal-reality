using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	CharacterController characterController;
	public float speed = 5.0f;
	public float gravity = 9.8f;
	private Vector3 direction = Vector3.zero;
    public float mouseSens = 2.0f;
    public Weapon weapon;
    private Dictionary<string, WeaponInfo> weapons;
    private bool acceptInput;
    public int health = 100;
    private int maxHealth;
    private int points = 300;
    private UI uiMananger;
    private float s1CD, s2CD, s3CD;
    private bool s1Used, s2Used, s3Used;
	// Use this for initialization
	void Start () {
        uiMananger = GameObject.Find("UI").GetComponent<UI>();
        maxHealth = health;
        s1CD = 0;
        s2CD = 0;
        s3CD = 0;
		characterController = GetComponent<CharacterController>();
        acceptInput = true;
        // force the resolution
        Screen.SetResolution (1920 , 1080, true, 60);
	}
	
	// Update is called once per frame
	void Update () {
//       Debug.Log("position X: " + transform.position.x + " || position y: " + transform.position.y + " || position z: " + transform.position.z);
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
                if (Input.GetButton ("Jump")) {
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
        } else if (Input.GetKeyUp(KeyCode.E) && !s2Used) {
            weapon.Attack(2);
            s2CD = weapon.getSkills()[1].getCurrent().getSpeed();
            s2Used = true;
            uiMananger.updateSkillBar(1, true, s2CD);
        } else if (Input.GetKeyUp(KeyCode.R) && !s3Used) {
            weapon.Attack(3);
            s3CD = weapon.getSkills()[2].getCurrent().getSpeed();
            s3Used = true;
            uiMananger.updateSkillBar(2, true, s3CD);
        }
    }

    public void setMovable(bool val) {
        acceptInput = val;
    }

    public void healthMod(int val) {
        health += val;
        Debug.Log(health);
        uiMananger.updatePlayerHP(health);
    }
    
    public void changeWeapon(string weaponName) {
        weapons = GameObject.Find("WeaponSelect").GetComponent<WeaponSelect>().GetWeapons();
        weapon.updateWeapon(weapons[weaponName]);
    }

    public void addPoints(int newPoints) {
        points += newPoints;
    }

    public int getPoints() {
        return points;
    }

    public void usePoints(int pointsUsed) {
        points -= pointsUsed;
    }

    public int getHealth() {
        return health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }
}